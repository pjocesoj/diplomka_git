using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Enums;

namespace NodeEmulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Endpoint> _endpoints = new List<Endpoint>();
        private void Form1_Load(object sender, EventArgs e)
        {
            var vals = new ValueDto[]
           {
                   new ValueDto() { Name = "a", Type = ValType.INT },
                   new ValueDto() { Name = "b", Type = ValType.INT }
           };
            _endpoints.Add(new Endpoint(HttpMethodEnum.GET, "/getValues", vals));

            var list = _endpoints.Select(x => x.Info);
            new HttpEndpoint().Start(8080, "/getInfo", list);
            foreach (var endpoint in _endpoints)
            {
                new HttpEndpoint().Start(8080, endpoint.Info.URL, endpoint.Values);
            }

        }
    }
}
