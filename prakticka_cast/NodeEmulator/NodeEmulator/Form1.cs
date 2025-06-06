using MainNode.Communication.Dto;
using MainNode.Communication.Enums;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace NodeEmulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("NodeEmulator.favicon.ico"))
            {
                if (stream != null)
                    this.Icon = new Icon(stream);
            }
        }

        List<Endpoint> _endpoints = new List<Endpoint>();
        private void Form1_Load(object sender, EventArgs e)
        {
            createGet();

            var list = _endpoints.Select(x => x.Info);
            new HttpEndpoint().Start(8080, "/getInfo", list);
            foreach (var endpoint in _endpoints)
            {
                new HttpEndpoint().Start(8080, endpoint.Info.URL, endpoint.SerializeValues);
                createControls(endpoint);
            }

            createSet();
            createMultiply();
            createSlow();
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

            foreach (ValueDo val in ep.Info.Vals)
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

                #region val_val
                TextBox val_val = new TextBox();
                val_val.Text = val.ValueToString();
                val_val.Tag = val;
                val_val.Height = 25;
                val_val.Left = 250;
                val_val.Top = top;
                panel.Controls.Add(val_val);
                #endregion

                top = val_val.Bottom + 5;

            }

            Control last = panel.Controls[panel.Controls.Count - 1];
            panel.Height = last.Bottom + 5;

            Button btn = new Button();
            btn.Text = "update";
            btn.Click += Btn_Click;
            btn.BackColor = Color.White;
            btn.Height = last.Bottom - (name.Bottom + 5);
            btn.Width = 100;
            btn.Left = 400;
            btn.Top = name.Bottom + 5;
            panel.Controls.Add(btn);

            ep.View = panel;
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            var panel = (sender as Button).Parent;

            foreach (var control in panel.Controls)
            {
                if (control is TextBox tb)
                {
                    var val = (ValueDo)tb.Tag;
                    val.ValueFromString(tb.Text);
                }
            }
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
            var args = new ValueArgDto[0];
            _endpoints.Add(new Endpoint(HttpMethodEnum.GET, EndPointType.EP_TYPE_GET, "/getValuesG", vals, args));
        }
        void createSet()
        {
            var vals = new ValueDto[]
            {
                   new ValueDo<int>() { Name = "a", Type = ValType.INT,Value=1 },
                   new ValueDo<float>() { Name = "b", Type = ValType.FLOAT,Value=1 },
                   new ValueDo<bool>() { Name = "c", Type = ValType.BOOL,Value=true },
            };
            var args = new ValueArgDto[]
            {
                new ValueArgDto() { Name = "a", Type = ValType.INT,Default="1" },
                new ValueArgDto() { Name = "b", Type = ValType.FLOAT,Default="1" },
                new ValueArgDto() { Name = "C", Type = ValType.BOOL,Default="true" },
            };
            var endpoint = new Endpoint(HttpMethodEnum.POST, EndPointType.EP_TYPE_SET, "/setValues", vals, args);
            _endpoints.Add(endpoint);

            new HttpEndpoint().Start(8080, endpoint.Info.URL, endpoint.SerializeValues, endpoint.Deserialize);
            createControls(endpoint);
        }

        void createMultiply()
        {
            var vals = new ValueDto[]
            {
                   new ValueDo<int>() { Name = "a", Type = ValType.INT,Value=1 },
                   new ValueDo<int>() { Name = "b", Type = ValType.INT,Value=10 },
                   new ValueDo<int>() { Name = "c", Type = ValType.INT,Value=20 }
            };
            var args = new ValueArgDto[]
            {
                new ValueArgDto() { Name = "x", Type = ValType.INT,Min="",Max="100",Default="1" },
            };
            var endpoint = new Endpoint(HttpMethodEnum.POST, EndPointType.EP_TYPE_GET, "/multiply", vals, args);
            _endpoints.Add(endpoint);

            Action<string> des = ((json) =>
            {
                var vals = JsonSerializer.Deserialize<ValuesDto>(json);
                int x = vals.Ints.First();
                var ints = endpoint.Info.Vals.Where(x => x.Type == ValType.INT).Select(x => (ValueDo<int>)x);
                foreach (var i in ints)
                {
                    i.Value = i.Value * x;
                }
                endpoint.UpdateView();
            });

            new HttpEndpoint().Start(8080, endpoint.Info.URL, endpoint.SerializeValues, des);
            createControls(endpoint);
        }

        void createSlow()
        {
            var vals = new ValueDto[]
            {
                   new ValueDo<int>() { Name = "a", Type = ValType.INT,Value=1 },
            };
            var args = new ValueArgDto[0];

            var endpoint = new Endpoint(HttpMethodEnum.GET, EndPointType.EP_TYPE_GET, "/slow", vals, args,1000);
            _endpoints.Add(endpoint);

            Action<string> des = ((json) =>
            {
                var ints = endpoint.Info.Vals.Where(x => x.Type == ValType.INT).Select(x => (ValueDo<int>)x);
                foreach (var i in ints)
                {
                    i.Value = i.Value+1;
                }
                Thread.Sleep(1000);
                endpoint.UpdateView();
            });

            new HttpEndpoint().Start(8080, endpoint.Info.URL, endpoint.SerializeValues, des);
            createControls(endpoint);
        }
    }
}
