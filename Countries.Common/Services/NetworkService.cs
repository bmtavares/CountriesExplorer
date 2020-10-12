namespace CountriesExplorer.Common.Services
{
    using Models;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows;

    public class NetworkService : INetworkService
    {
        public Response CheckConnection()
        {
            var client = new WebClient();

            try
            {
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return new Response
                    {
                        IsSuccess = true
                    };
                }
            }
            catch
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check the Internet connection."
                };
            }
        }
    }
}
