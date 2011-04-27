using System;
using RestSharp;

namespace Renren.API.Request
{
    public abstract class RenrenRequestBase : RestRequest
    {
        private const string API_VERSION_PARAM_NAME = "v";
        private const string API_VERSION_PARAM_VALUE = "1.0";

        private const string CALL_ID_PARAM_NAME = "call_id";
        // ReSharper disable InconsistentNaming
        private static string CALL_ID_PARAM_VALUE { get { return DateTime.Now.Millisecond.ToString(); } }
        // ReSharper restore InconsistentNaming

        private const string METHOD_PARAM_NAME = "method";
        // ReSharper disable InconsistentNaming
        protected abstract string METHOD_PARAM_VALUE { get; }
        // ReSharper restore InconsistentNaming

        protected RenrenRequestBase()
            : base(Method.POST)
        {
            AddParameter(API_VERSION_PARAM_NAME, API_VERSION_PARAM_VALUE);
            AddParameter(CALL_ID_PARAM_NAME, CALL_ID_PARAM_VALUE);
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            AddParameter(METHOD_PARAM_NAME, METHOD_PARAM_VALUE);
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }
    }
}