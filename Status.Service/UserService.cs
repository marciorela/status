using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using MR.String;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Status.Domain.ViewModels;

namespace Status.Service
{
    public class UserService
    {

        public async Task<ReturnIdVM> CheckAuthenticationAsync(string email, string password)
        {
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
        }
    }
}
