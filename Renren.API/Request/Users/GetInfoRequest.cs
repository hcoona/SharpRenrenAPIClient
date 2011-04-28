/***********************
 * 
 * http://wiki.dev.renren.com/wiki/Users.getInfo
 * 
 ***********************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Renren.API.Request.Users
{
    public class GetInfoRequest : RenrenRequestBase
    {
        private const string FIELDS_PARAM_NAME = "fields";
        private const string SESSION_KEY_PARAM_NAME = "session_key";

        protected override string METHOD_PARAM_VALUE
        {
            get { return "users.getInfo"; }
        }

        public GetInfoRequest(string sessionKey, FieldsFlag fields = FieldsFlag.Default)
        {
            AddParameter(SESSION_KEY_PARAM_NAME, sessionKey);

            var fieldsParamValue = BuildFieldsParamValue(fields);
            AddParameter(FIELDS_PARAM_NAME, fieldsParamValue);
        }

        private static string BuildFieldsParamValue(FieldsFlag fields)
        {
            var sb = new StringBuilder();
            foreach (var name in Enum.GetNames(typeof(FieldsFlag)))
            {
                var f = (FieldsFlag)Enum.Parse(typeof(FieldsFlag), name);
                if ((f & fields) != 0) sb.AppendFormat("{0},", FieldsEnumStringMapper[f]);
            }
            return sb.ToString();
        }

        private static readonly Dictionary<FieldsFlag, string> FieldsEnumStringMapper = new Dictionary<FieldsFlag, string>();
        static GetInfoRequest()
        {
            foreach (var name in Enum.GetNames(typeof(FieldsFlag)))
            {
                FieldsEnumStringMapper.Add((FieldsFlag)Enum.Parse(typeof(FieldsFlag), name), name.ToLower());
            }
            FieldsEnumStringMapper[FieldsFlag.EmailHash] = "email_hash";
            FieldsEnumStringMapper[FieldsFlag.HomeTownLocation] = "hometown_location";
            FieldsEnumStringMapper[FieldsFlag.WorkInfo] = "work_info";
            FieldsEnumStringMapper[FieldsFlag.UniversityInfo] = "unifersity_info";
        }
    }


    /*****************
     * ²Î¿¼×ÊÁÏ£º
     * http://hi.baidu.com/hhayy7758/blog/item/0e0ebf24656a4b044d088d5c.html
     * 
     *****************/
    [Flags]
    public enum FieldsFlag
    {
        Uid = 1,    // 0000001b
        Name = 2,   // 0000010b
        Sex = 4,    // 0000100b
        Star = 8,
        ZiDou = 16,
        Vip = 32,
        Birthday = 64,
        EmailHash = 128,
        TinyUrl = 256,
        HeadUrl = 512,
        MainUrl = 1024,
        HomeTownLocation = 2048,
        WorkInfo = 4096,
        UniversityInfo = 8192,
        Default = Uid | Name | TinyUrl | HeadUrl | ZiDou | Star
    }
}