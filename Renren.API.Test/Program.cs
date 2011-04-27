using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Renren.API.Entity;
using Renren.API.Request;
using RestSharp;

namespace Renren.API.Test
{
    class Program
    {
        private const string CONSUMER_KEY = "your api key";
        private const string CONSUMER_SECRET = "you api secret 注意不要提交到代码库中！";

        static void Main()
        {
            var accessToken = GetAccessToken();

            var client = new RenrenClient(CONSUMER_KEY, CONSUMER_SECRET, accessToken);
            var gfr = new GetFriendsRequest();
            var friends = client.Query<List<FriendInfoEntity>>(gfr);

            foreach (var f in friends)
            {
                Console.WriteLine(f.Name);
            }
        }

        private static string GetAccessToken()
        {
            string accessToken;

            var loginClient = new RestClient("http://passport.renren.com/RL.do?p3p=1");
            var req = new RestRequest(Method.POST);
            req.AddParameter("email", "用户名(Email)");
            req.AddParameter("password", "密码");
            req.AddParameter("origURL",
                             "http://graph.renren.com/oauth/authorize?client_id=aeaac569ad9a4a8298293669520ada65&redirect_uri=http://graph.renren.com/oauth/login_success.html&response_type=token&display=popup&pp=1&https=https://graph.renren.com");

            var res = loginClient.Execute(req);
            switch (res.ResponseUri.AbsolutePath)
            {
                // 没有允许该API接入
                case "/user_approve":
                    {
                        var doc = new HtmlDocument();
                        doc.LoadHtml(res.Content);
                        var form = doc.DocumentNode.SelectSingleNode("//form");
                        var other = doc.DocumentNode.SelectNodes("//input[@type=\"hidden\"]");

                        var approveClient = new RestClient(form.Attributes["action"].Value);
                        var reqq = new RestRequest(Method.POST);
                        foreach (var item in other)
                            reqq.AddParameter(item.Attributes["name"].Value, item.Attributes["value"].Value);
                        var ress = approveClient.Execute(reqq);
                        Console.WriteLine(ress.ResponseUri.PathAndQuery);
                        throw new NotImplementedException("还没琢磨明白呢！");
                    }
                    break;

                // 以前允许过，因此直接登录就行了，不用授权
                case "/oauth/login_success.html":
                    var fragment = res.ResponseUri.Fragment.Substring(1);
                    var ss = fragment.Split('&');
                    accessToken = ss[0].Split('=')[1].Replace("%7C", "|");
                    break;

                // 可能人人网更改了接口了
                default:
                    throw new NotSupportedException();
            }
            return accessToken;
        }
    }
}
