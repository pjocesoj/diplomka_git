using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Interfaces;
using System.Text.Json;
using System;

namespace HlavniUzel.Komunikace
{
    public class HttpNodeCommunication : INodeCommunication
    {
        private readonly HttpClient _httpClient;
        public HttpNodeCommunication()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = new TimeSpan(0, 0, 1);
        }

        private async Task<T?> getAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                string json = await response.Content.ReadAsStringAsync();
                var ret = JsonSerializer.Deserialize<T>(json);
                return ret;
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception("timeout", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("JSON", ex);
            }
            catch (Exception ex)
            {
                var t = ex.GetType();
                throw new Exception("unexpected error", ex);
            }
        }

        public async Task<EndPointDto[]> GetEndPoints()
        {
            string url = "http://192.168.1.233/getInfo";
            try
            {
                return await getAsync<EndPointDto[]>(url);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ValuesDto?> GetValues(string endpoint)
        {
            string url = $"http://192.168.1.233/{endpoint}";
            try
            {
                var t= await getAsync<Dto.temp.Values_temp?>(url);
                return t;
                //return await getAsync<ValuesDto>(url);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
