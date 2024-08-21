using MainNode.Communication.Dto;
using MainNode.Communication.Interfaces;
using System.Text.Json;
using System;
using System.Net.Http.Json;
using MainNode.Exceptions;
using System.Text;

namespace MainNode.Communication
{
    public class HttpNodeCommunication : INodeCommunication
    {
        private HttpClient _httpClient;
        private TimeSpan _defaultTimeout = new TimeSpan(0, 0, 1);
        public string Address { get; private set; } = "";
        public string AddressType { get; set; } = "http";
        public void Init(string address)
        {
            _httpClient = new HttpClient
            {
                Timeout = new TimeSpan(10, 0, 0)//potřeba zajistit aby byl větší než nejpomalejší endpoint
            };

            Address = address;
        }

        private async Task<T?> getAsync<T>(string url,int? delay=null)
        {
            if (string.IsNullOrEmpty(Address)) { throw new ApplicationException("call Init to set address"); }

            try
            {
                var tokenSource = new CancellationTokenSource(delay==null?_defaultTimeout:TimeSpan.FromSeconds(5));
                HttpResponseMessage response = await _httpClient.GetAsync(url,tokenSource.Token);

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
                string body = JsonSerializer.Serialize(data);
                HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(body));

                string json = await response.Content.ReadAsStringAsync();
                if (json == "ok") { return (T)(object)true; }//bool nemůže být přímo převeden
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
        private string ArgsToString(ValuesDto args)
        {
            if (args == null) { return string.Empty; }

            StringBuilder sb = new StringBuilder("?");
            char name = 'a';//pro snížení množství dat použit 1 písmený název (node zná pořadí)
            foreach (var v in args.Ints) { sb.Append($"{name++}={v}"); }
            foreach (var v in args.Floats) { sb.Append($"{name++}={v}"); }
            foreach (var v in args.Bools) { sb.Append($"{name++}={v}"); }
            return sb.ToString();
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
        public async Task<ValuesDto?> GetValues(EndPointPath path, int? delay, ValuesDto args = null)
        {
            string url = $"http://{Address}{path.Path}";
            try
            {
                if ((path as HttpEndPointPath)!.HttpMethod == Enums.HttpMethodEnum.GET)
                {
                    string arg=ArgsToString(args);
                    return await getAsync<ValuesDto>($"{url}{arg}",delay);
                }
                return await postAsync<ValuesDto>(url, args);

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
                var ret = await postAsync<ValuesDto>(url, vals);
                return true;
                //return ret;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
