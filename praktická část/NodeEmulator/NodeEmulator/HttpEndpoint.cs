using System.Net;
using System.Text;
using System.Text.Json;

namespace NodeEmulator
{
    public class HttpEndpoint
    {
        HttpListener _listener;
        object _response;
        public void Start(int port,string endpoint, object obj)
        {
            _response = obj;

            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}{endpoint}/");
            _listener.Start();
            Console.WriteLine("Listening...");
            _ = Task.Run(async() => { await Listen(serialize); });
        }
        public void Start(int port, string endpoint, Func<string> serializer)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}{endpoint}/");
            _listener.Start();
            Console.WriteLine("Listening...");
            _ = Task.Run(async () => { await Listen(serializer); });
        }

        string serialize()
        {
            return JsonSerializer.Serialize(_response);
        }

        public async Task Listen(Func<string> serializer)
        {
            while (true)
            {
                HttpListenerContext context = _listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                var responseString = serializer();

                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "application/json";
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}
