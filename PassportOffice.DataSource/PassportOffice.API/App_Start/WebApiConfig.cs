namespace PassportOffice.API
{
    using System.Net.Http.Formatting;
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable crossdamain requests.
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Setting of formatters
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
        }
    }
}
