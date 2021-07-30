namespace Web.Controllers
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public static class Auth
        { 
            public const string Init = Root + "/auth/User/Login";
        }

        public  static class Properties
        {
            public const string CreatePropertyBuilding = Root + "/Properties/Create-Property-Building/";
            public const string CreateProperty = Root + "/Properties/Create-Property/";
            public const string CreatePropertyWithOwner = Root + "/Properties/Create-Property-With-Owner/";
            public const string AddImageProperty = Root + "/Properties/Add-Image-Property/";
            public const string ChangePriceProperty = Root + "/Properties/Change-Price-Property/";
            public const string FindPropertyByFilters = Root + "/Properties/Find-Property-By-Filters/";
            
        }
    }
}
