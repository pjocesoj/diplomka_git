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
            HttpEndpoint server = new HttpEndpoint();
            server.Start(8080,"test");
        }
    }
}
