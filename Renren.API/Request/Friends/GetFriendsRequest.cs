/***********************
 * 
 * http://wiki.dev.renren.com/wiki/Friends.getFriends
 * 
 ***********************/

namespace Renren.API.Request.Friends
{
    // 其返回值对应着List<FriendInfoEntity>
    public class GetFriendsRequest : RenrenRequestBase
    {
        private const string SESSION_KEY_PARAM_NAME = "session_key";

        private const string PAGE_PARAM_NAME = "page";
        private const string COUNT_PARAM_NAME = "count";

        protected override string METHOD_PARAM_VALUE
        {
            get { return "friends.getFriends"; }
        }

        public GetFriendsRequest(string sessionKey, int page = 0, int count = 500)
        {
            AddParameter(SESSION_KEY_PARAM_NAME, sessionKey);

            //optional
            AddParameter(PAGE_PARAM_NAME, page);
            AddParameter(COUNT_PARAM_NAME, count);
        }
    }
}