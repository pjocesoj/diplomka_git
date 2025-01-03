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
            _ = Task.Run(async() => { await Listen(serialize,null); });
        }
        public void Start(int port, string endpoint, Func<string> serializer)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}{endpoint}/");
            _listener.Start();
            Console.WriteLine("Listening...");
            _ = Task.Run(async () => { await Listen(serializer,null); });
        }
        public void Start(int port, string endpoint, Func<string> serializer,Action<string> deserializer)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}{endpoint}/");
            _listener.Start();
            Console.WriteLine("Listening...");
            _ = Task.Run(async () => { await Listen(serializer,deserializer); });
        }
        public void Start(int port, string endpoint, object obj, Action<string> deserializer)
        {
            _response = obj;

            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}{endpoint}/");
            _listener.Start();
            Console.WriteLine("Listening...");
            _ = Task.Run(async () => { await Listen(serialize, deserializer); });
        }
        string serialize()
        {
            return JsonSerializer.Serialize(_response);
        }

        public async Task Listen(Func<string> serializer,Action<string> deserializer)
        {
            while (true)
            {
                HttpListenerContext context = _listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                if(deserializer is not null) 
                {
                    var json=getBody(request);
                    deserializer(json); 
                }
                var responseString = serializer();

                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "application/json";
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    
        private string getBody(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (Stream body = request.InputStream)
            {
                using (var reader = new StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
