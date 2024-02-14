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

            foreach (var val in ep.Info.Vals)
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
                val_val.Text = ValueToString(val);
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
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            var panel = (sender as Button).Parent;

            foreach(var control in panel.Controls)
            {
                if(control is TextBox tb) 
                {
                    var val = (ValueDto)tb.Tag;
                    if (val is ValueDo<int> childInt)
                    {
                        childInt.Value=Convert.ToInt32(tb.Text);
                    }
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
            _endpoints.Add(new Endpoint(HttpMethodEnum.GET, "/getValuesG", vals));
        }

        //protože generický potomek negenerického rodièe to komplikuje
        string ValueToString(ValueDto val)
        {
            if (val is ValueDo<int> childInt)
            {
                return childInt.Value.ToString();
            }
            else if (val is ValueDo<float> childFloat)
            {
                return childFloat.Value.ToString();
            }
            else if (val is ValueDo<bool> childBool)
            {
                return childBool.Value.ToString();
            }

            return "n/a";
        }
    }
}
