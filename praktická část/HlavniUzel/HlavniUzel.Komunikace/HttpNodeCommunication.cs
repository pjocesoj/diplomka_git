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

        public async Task<EndPointDto[]> GetEndPoints()
        {
            string url = "http://192.168.1.233/getInfo";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                string json = await response.Content.ReadAsStringAsync();
                var ret = JsonSerializer.Deserialize<EndPointDto[]>(json);
                return ret!;
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
    }
}
