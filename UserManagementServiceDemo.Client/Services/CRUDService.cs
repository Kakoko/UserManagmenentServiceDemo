using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementServiceDemo.Client.Services
{
    public class CRUDService : IIntegrationService
    {

        private static HttpClient _httpClient = new HttpClient();


        public CRUDService()
        {
            //set up HTTPClient instance
            _httpClient.BaseAddress = new Uri("https://localhost:44386");
            _httpClient.Timeout = new TimeSpan(0, 0, 30); 

        }
        public async Task Run()
        {
            await GetResource();
        }
        

        public async Task GetResource()
        {
            var response = await _httpClient.GetAsync("api/Report/users");
            response.EnsureSuccessStatusCode();
        }

    }
}
