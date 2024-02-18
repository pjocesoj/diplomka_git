using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Interfaces;
using System.Text.Json;
using System;
using System.Net.Http.Json;
using MainNode.Exceptions;

namespace HlavniUzel.Komunikace
{
    public class HttpNodeCommunication : INodeCommunication
    {
        private HttpClient _httpClient;

        public string Address { get; private set; } = "";

        public void Init(string address)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = new TimeSpan(0, 0, 1);

            Address=address;
        }

        private async Task<T?> getAsync<T>(string url)
        {
            if (string.IsNullOrEmpty(Address)) { throw new ApplicationException("call Init to set address"); }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                string json = await response.Content.ReadAsStringAsync();
                var ret = JsonSerializer.Deserialize<T>(json);
                return ret;
            }
            catch (TaskCanceledException ex)
            {
                throw new CommunicationException("timeout", ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("JSON", ex);
            }
            catch (Exception ex)
            {
                var t = ex.GetType();
                throw new Exception("unexpected error", ex);
            }
        }
        private async Task<T?> postAsync<T>(string url, object data)
        {
            if (string.IsNullOrEmpty(Address)) { throw new ApplicationException("call Init to set address"); }

            try
            {
                string body=JsonSerializer.Serialize(data);
                HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(body));

                string json = await response.Content.ReadAsStringAsync();
                if (json == "ok") { return (T)(object)true;  }//bool nemůže být přímo převeden
                var ret = JsonSerializer.Deserialize<T>(json);
                return ret;
            }
            catch (TaskCanceledException ex)
            {
                throw new CommunicationException("timeout", ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("JSON", ex);
            }
            catch (Exception ex)
            {
                var t = ex.GetType();
                throw new Exception("unexpected error", ex);
            }
        }


        public async Task<EndPointDto[]> GetEndPoints()
        {
            string url = $"http://{Address}/getInfo";
            try
            {
                return await getAsync<EndPointDto[]>(url);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ValuesDto?> GetValues(EndPointPath path)
        {
            string url = $"http://{Address}{path.Path}";
            try
            {
                //return await getAsync<Dto.temp.Values_temp?>(url);
                return await getAsync<ValuesDto>(url);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> SetValues(EndPointPath path, ValuesDto vals)
        {
            string url = $"http://{Address}{path.Path}";
            try
            {
                var ret=await postAsync<bool>(url,vals);
                return ret;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
