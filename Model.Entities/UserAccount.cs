namespace Model.Entities
{
    public class UserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string GoogleApisServerKey { get; set; }
        public string GoogleApisBrowserKey { get; set; }
        public string FilePath { get; set; }
    }
}