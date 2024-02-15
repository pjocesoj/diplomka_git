using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Enums;
using System.Collections;
using System.Text.Json;
using System.Windows.Forms;

namespace NodeEmulator
{
    public class Endpoint
    {
        public EndPointDto Info { get; set; }

        public ValuesDto Values
        {
            get
            {
                var ints = Info.Vals.Where(x => x.Type == ValType.INT).Select(x => (ValueDo<int>)x);
                var floats = Info.Vals.Where(x => x.Type == ValType.FLOAT).Select(x => (ValueDo<float>)x);
                var bools = Info.Vals.Where(x => x.Type == ValType.BOOL).Select(x => (ValueDo<bool>)x);

                var i = ints.Select(x => x.Value);
                return new ValuesDto()
                {
                    Ints = ints.Select(x => x.Value).ToArray(),
                    Floats = floats.Select(x => x.Value).ToArray(),
                    Bools = bools.Select(x => x.Value).ToArray(),
                };
            }
        }

        public Control View { get; set; }

        public Endpoint(HttpMethodEnum http, string url, ValueDto[] vals)
        {
            Info = new EndPointDto()
            {
                HTTP = http,
                URL = url,
                Vals = vals
            };
            UpdateViewEvent += Endpoint_UpdateViewEvent;
        }

        #region UpdateView
        public void UpdateView()
        {
            //pokud běží ve špatném vlákně zavolá znovu ve správném
            if(this.View.InvokeRequired) {this.View.Invoke(new Action(UpdateView));}           
            UpdateViewEvent?.Invoke(View);
        }
        // Define a delegate for the event
        public delegate void UpdateViewHandler(Control view);

        // Define the event using the delegate
        public event UpdateViewHandler UpdateViewEvent;

        private void Endpoint_UpdateViewEvent(Control view)
        {
            foreach (var control in view.Controls)
            {
                if (control is TextBox tb)
                {
                    var val = (ValueDo)tb.Tag;
                    try
                    {
                        tb.Text = val.ValueToString();
                    }
                    catch (Exception ex)
                    { int i = 0; }
                }
            }
        }
        #endregion

        #region serialize
        public string SerializeInfo()
        {
            return JsonSerializer.Serialize(this);
        }
        public string SerializeValues()
        {
            return JsonSerializer.Serialize(Values);
        }
        #endregion
    }
}
