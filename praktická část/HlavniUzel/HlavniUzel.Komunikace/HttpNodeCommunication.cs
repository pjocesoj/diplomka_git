using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Interfaces;
using System.Text.Json;
using System;

namespace HlavniUzel.Komunikace
{
    public class HttpNodeCommunication:INodeCommunication
    {
        private readonly HttpClient _httpClient;
        public HttpNodeCommunication()
        {
            _httpClient = new HttpClient();
        }

        public EndPointDto[] GetEndPoints()
        {
            string url = "http://192.168.1.233/getInfo";
            using (HttpResponseMessage response = new HttpClient().GetAsync(url).Result)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                var ret = JsonSerializer.Deserialize<EndPointDto[]>(json);
                return ret!;
            }
        }
    }
}
