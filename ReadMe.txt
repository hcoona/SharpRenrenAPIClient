代码风格使用ReSharper控制
Renren.API依赖于RestSharp，而RestSharp依赖于Json.Net

注意不要在修改代码的过程中将敏感信息提交到版本控制系统中，不然就抹不掉这些信息了……

使用方法：
首先按照http://wiki.dev.renren.com/wiki/Authentication中
《快速上手》和《应用场景》中的实例中的方法获得AccessToken

使用RenrenClient时，须将API_KEY, API_SECRET和Access_Token作为参数传递给构造函数。
之后RenrenClient会使用AccessToken来获取Session_Key

注意：获取的Access_Token和Session_Key实际上都有一个ExpiresIn的过期时间，在此库中目前都没有考虑。

目前此库仅实现了friends.getFriends接口。
（http://wiki.dev.renren.com/wiki/Friends.getFriends）
对应的Request为GetFriendsRequest类，查询结果对应着List<FriendInfoEntity>

使用时，将构造好的GetFriendsRequest传递给RenrenClient的Query方法，
将查询结果的类型传递给Query方法的泛型参数，即可完成查询。

在继续开发此库时，应该遵循此模式进行开发。