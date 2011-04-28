/***********************
 * 
 * http://wiki.dev.renren.com/wiki/Friends.getFriends
 * 
 ***********************/

namespace Renren.API.Request.Friends
{
    public class GetFriendsRequest : RenrenRequestBase
    {
        private const string PAGE_PARAM_NAME = "page";
        private const string COUNT_PARAM_NAME = "count";

        protected override string METHOD_PARAM_VALUE
        {
            get { return "friends.getFriends"; }
        }

        public GetFriendsRequest(int page = 0, int count = 500)
        {
            AddParameter(PAGE_PARAM_NAME, page);
            AddParameter(COUNT_PARAM_NAME, count);
        }
    }
}