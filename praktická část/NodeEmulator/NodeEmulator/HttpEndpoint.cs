using System.Net;
using System.Text;
using System.Text.Json;

namespace NodeEmulator
{
    public class HttpEndpoint
    {
        HttpListener _listener;
        public void Start(int port,string endpoint)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}/{endpoint}/");
            _listener.Start();
            Console.WriteLine("Listening...");
            _ = Task.Run(async() => { await Listen(); });
        }

        public async Task Listen()
        {
            while (true)
            {
                HttpListenerContext context = _listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                var myObject = new { Name = "John", Age = 30, City = "New York" };
                var responseString = JsonSerializer.Serialize(myObject);

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
