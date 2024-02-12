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

        private void Form1_Load(object sender, EventArgs e)
        {
            var vals = new ValueDto[]
           {
                   new ValueDto() { Name = "a", Type = HlavniUzel.Komunikace.Enums.ValType.INT },
                   new ValueDto() { Name = "a", Type = HlavniUzel.Komunikace.Enums.ValType.INT }
           };
            var ep1 = new Endpoint(HttpMethodEnum.GET, "getInfo",vals);

            var list = new EndPointDto[] { ep1.Info };
            HttpEndpoint server = new HttpEndpoint();
            server.Start(8080, "getInfo", list);
        }
    }
}
