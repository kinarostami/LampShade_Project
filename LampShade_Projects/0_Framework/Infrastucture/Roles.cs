namespace _0_Framework.Infrastucture
{
    public static class Roles
    {
        public const string Administration = "3";
        public const string ContentUploader = "6";
        public const string SystemUser = "4";
        public const string ColleagueUser = "5";

        public static string GetRoleBy(long id)
        {
            switch (id)
            {
                case 3:
                    return "مدیر سیستم";
                case 6:
                    return "محتوا گذار";
                default:
                    return "";
            }
        }
    }
}
