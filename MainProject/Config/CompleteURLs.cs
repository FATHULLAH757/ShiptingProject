namespace MainProject.Config
{
    public class CompleteURLs
    {
        public string URL { get; set; }
        public string AccessToken { get; set; }
        public CompleteURLs(string URL, string AccessToken)
        {
            this.URL = URL;
            this.AccessToken = AccessToken;
        }
        public static CompleteURLs testMethod =>  new CompleteURLs("dddd", "dfdfdfd");
    }
}
