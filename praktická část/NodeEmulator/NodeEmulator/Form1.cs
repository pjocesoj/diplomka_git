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
                   new ValueDto() { Name = "b", Type = ValType.INT },
                   new ValueDto() { Name = "c", Type = ValType.FLOAT },
                   new ValueDto() { Name = "d", Type = ValType.BOOL }
           };
            _endpoints.Add(new Endpoint(HttpMethodEnum.GET, "/getValues", vals));

            createGet();

            var list = _endpoints.Select(x => x.Info);
            new HttpEndpoint().Start(8080, "/getInfo", list);
            foreach (var endpoint in _endpoints)
            {
                new HttpEndpoint().Start(8080, endpoint.Info.URL, endpoint.Values);
                createControls(endpoint);
            }

        }

        void createControls(Endpoint ep)
        {
            #region panel
            Panel panel = new Panel();
            panel.Width = flowLayoutPanel1.Width - 5;
            panel.BackColor = Color.Red;
            flowLayoutPanel1.Controls.Add(panel);
            #endregion
            #region name
            Label name = new Label();
            name.Text = ep.Info.URL;
            name.AutoSize = true;
            name.Left = (panel.Width - name.Width) / 2;
            panel.Controls.Add(name);
            #endregion

            int top = name.Bottom + 5;
            var order = ep.Info.Vals.GroupBy(x => x.Type);
            foreach (var group in order)
            {
                int i = 0;
                var arr = ep.getCollection(group.Key);
                foreach (var val in group)
                {
                    #region val_type
                    Label val_type = new Label();
                    val_type.Text = val.Type.ToString();
                    val_type.Height = 25;
                    val_type.Left = 5;
                    val_type.Top = top;
                    panel.Controls.Add(val_type);
                    #endregion

                    #region val_name
                    Label val_name = new Label();
                    val_name.Text = val.Name;
                    val_name.Width = 100; ;
                    val_name.Height = 25;
                    val_name.Left = 100;
                    val_name.Top = top;
                    panel.Controls.Add(val_name);
                    #endregion

                    #region val_name
                    TextBox val_val = new TextBox();
                    val_val.Text = arr[i]!.ToString();
                    val_val.Height = 25;
                    val_val.Left = 250;
                    val_val.Top = top;
                    panel.Controls.Add(val_val);
                    #endregion

                    top = val_val.Bottom + 5;
                    i++;
                }
            }

            Control last = panel.Controls[panel.Controls.Count - 1];
            panel.Height = last.Bottom + 5;
        }

        void createGet()
        {
            var vals = new ValueDto[]
         {
                   new ValueDo<int>() { Name = "a", Type = ValType.INT,Value=1 },
                   new ValueDo<int>() { Name = "b", Type = ValType.INT,Value=2 },
                   new ValueDo<float>() { Name = "c", Type = ValType.FLOAT,Value=3.14f },
                   new ValueDo<bool>() { Name = "d", Type = ValType.BOOL,Value=false }
         };
            _endpoints.Add(new Endpoint(HttpMethodEnum.GET, "/getValuesG", vals));
        }
    }
}
