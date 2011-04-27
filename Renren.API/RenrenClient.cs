using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Renren.API.Entity;
using Renren.API.Request;
using RestSharp;

namespace Renren.API
{
    public class RenrenClient
    {
        private const string RENREN_SESSION_KEY_URI = "http://graph.renren.com/renren_api/session_key";
        private const string RENREN_API_SERVER = "http://api.renren.com/restserver.do";

        private readonly string apiKey;
        private readonly string apiSecret;

        public readonly string SessionKey;
        private readonly RestClient client;

        public RenrenClient(string apiKey, string apiSecret, string accessToken)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;

            SessionKey = GetSessionKey(accessToken);
            client = new RestClient(RENREN_API_SERVER);
        }

        private static string GetSessionKey(string accessToken)
        {
            var sessionClient = new RestClient(RENREN_SESSION_KEY_URI);
            var sessionRequest = new RestRequest();
            sessionRequest.AddParameter("oauth_token", accessToken);
            var rep = sessionClient.Execute(sessionRequest);
            // RESTSharp需要运行时环境中存在Newtonsoft.Json.Net35！！
            // 否则执行结果的Data属性为null
            // 目前似乎这两个库的搭配有些问题
            // var ee = sessionClient.Execute<SessionKeyResponseEntity>(sessionRequest);
            var entity = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionKeyResponseEntity>(rep.Content);
            return entity.RenrenToken.SessionKey;
        }

        private const string API_KEY_PARAM_NAME = "api_key";
        private const string SESSION_KEY_PARAM_NAME = "session_key";

        private const string FORMAT_PARAM_NAME = "format";
        private const string FORMAT_PARAM_JSON_VALUE = "JSON";
        private const string FORMAT_PARAM_XML_VALUE = "XML";

        public string QueryJson(RenrenRequestBase request)
        {
            return QueryHelper(request, FORMAT_PARAM_JSON_VALUE);
        }

        public string QueryXml(RenrenRequestBase request)
        {
            return QueryHelper(request, FORMAT_PARAM_XML_VALUE);
        }

        public T Query<T>(RenrenRequestBase request)
        {
            var result = QueryJson(request);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
        }

        // 发送请求，文档见
        // http://wiki.dev.renren.com/wiki/API_Invocation#.E5.8F.91.E9.80.81.E8.AF.B7.E6.B1.82
        private string QueryHelper(RestRequest request, string format)
        {
            request.AddParameter(API_KEY_PARAM_NAME, apiKey);
            request.AddParameter(SESSION_KEY_PARAM_NAME, SessionKey);
            request.AddParameter(FORMAT_PARAM_NAME, format);

            // 附加验证签名，详情见
            // http://wiki.dev.renren.com/wiki/API_Invocation#.E8.AE.A1.E7.AE.97.E7.AD.BE.E5.90.8D
            var sig = MakeSignature(request.Parameters);
            request.AddParameter("sig", sig);

            return client.Execute(request).Content;
        }

        // http://wiki.dev.renren.com/wiki/API_Invocation#.E8.AE.A1.E7.AE.97.E7.AD.BE.E5.90.8D
        private string MakeSignature(List<Parameter> @params)
        {
            var sb = new StringBuilder();
            // 将Parameters以parameter name按照字典序排列
            @params.Sort(new Comparison<Parameter>((p1, p2) => string.Compare(p1.Name, p2.Name)));
            // 将Parameters以key1=value1key2=value2的方式拼接起来
            // 详情请参见：
            // http://wiki.dev.renren.com/wiki/API_Invocation#.E8.AE.A1.E7.AE.97.E7.AD.BE.E5.90.8D
            foreach (var p in @params) sb.AppendFormat("{0}={1}", p.Name, p.Value);
            sb.Append(apiSecret);

            // 用md5算法计算
            MD5 md5 = new MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));

            // 将计算结果转换为字符串
            var hashedString = new StringBuilder();
            foreach (var b in hashedBytes)
                hashedString.Append(b.ToString("x2"));
            return hashedString.ToString();
        }
    }
}
