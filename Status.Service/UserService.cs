using Status.Domain.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Status.Service
{
    public class UserService : BaseService
    {
        public async Task<ReturnIdVM> CheckAuthenticationAsync(string email, string password)
        {
            return await PostAsync<ReturnIdVM>("/users/v1/authenticate/", new SignInVM { Email = email, Senha = password });

            /*
                using var client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001/users/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var login = new SignInVM { Email = email, Senha = password };

            var uri = "v1/authenticate/";
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<ReturnIdVM>(data);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

            }

            return id;
   */

        }

    }
}
