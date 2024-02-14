using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Enums;
using System.Collections;

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

                var i = ints.Select(x=>x.Value);
                return new ValuesDto()
                {
                    Ints = ints.Select(x => x.Value).ToArray(),
                    Floats = floats.Select(x => x.Value).ToArray(),
                    Bools = bools.Select(x => x.Value).ToArray(),
                };
            }
        }

        public Endpoint(HttpMethodEnum http, string url, ValueDto[] vals)
        {
            Info = new EndPointDto()
            {
                HTTP = http,
                URL = url,
                Vals = vals
            };

            foreach (var val in vals)
            {
                //var wtf = (val as ValueDo);
                //Values.Add(wtf.Value); 
            }
        }
    }
}
