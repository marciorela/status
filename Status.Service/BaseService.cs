using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Status.Service
{
    public class BaseService
    {
        protected async Task<T> PostAsync<T>(string url, object value)
        {
            using var client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(value);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            //response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(data);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

            }

            return obj;
        }

        protected async Task<T> GetAsync<T>(string url, string query)
        {
            using var client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var uri = new Uri(client.BaseAddress, url);
            var uriBuilder = new UriBuilder(uri);

            if (query != "")
            {
                uriBuilder.Query = query;
            }

            var response = await client.GetAsync(uriBuilder.Uri, HttpCompletionOption.ResponseHeadersRead);

            //response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(data);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

            }

            return obj;
        }
    }
}
