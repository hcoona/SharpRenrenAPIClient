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
        private const string UIDS_PARAM_NAME = "uids";
        private const string FIELDS_PARAM_NAME = "fields";
        private const string SESSION_KEY_PARAM_NAME = "session_key";

        protected override string METHOD_PARAM_VALUE
        {
            get { return "users.getInfo"; }
        }

        // Session Key is optional parameter
        // Session Key could be obtained from RenrenClient
        public GetInfoRequest(int userId, string sessionKey, FieldsFlag fields = FieldsFlag.Default)
        {
            AddParameter(UIDS_PARAM_NAME, userId);

            var fieldsParamValue = BuildFieldsParamValue(fields);
            AddParameter(FIELDS_PARAM_NAME, fieldsParamValue);

            //optional
            if (!string.IsNullOrEmpty(sessionKey))
                AddParameter(SESSION_KEY_PARAM_NAME, sessionKey);
        }

        private static string BuildFieldsParamValue(FieldsFlag fields)
        {
            var sb = new StringBuilder();
            foreach (var name in Enum.GetNames(typeof(FieldsFlag)))
            {
                if (name == "Default") continue;

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
                if (name != "Default")
                    FieldsEnumStringMapper.Add((FieldsFlag)Enum.Parse(typeof(FieldsFlag), name), name.ToLower());
            }
            FieldsEnumStringMapper[FieldsFlag.EmailHash] = "email_hash";
            FieldsEnumStringMapper[FieldsFlag.HomeTownLocation] = "hometown_location";
            FieldsEnumStringMapper[FieldsFlag.WorkHistory] = "work_history";
            FieldsEnumStringMapper[FieldsFlag.UniversityHistory] = "university_history";
            FieldsEnumStringMapper[FieldsFlag.HighSchoolHistory] = "hs_history";
            FieldsEnumStringMapper[FieldsFlag.ContactInfo] = "contact_info";
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
        WorkHistory = 4096,
        UniversityHistory = 8192,
        HighSchoolHistory = 16384,
        ContactInfo = 32768,
        Default = Uid | Name | TinyUrl | HeadUrl | ZiDou | Star
    }
}