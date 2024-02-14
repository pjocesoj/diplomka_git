using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Enums;
using System.Collections;

namespace NodeEmulator
{
    public class Endpoint
    {
        public EndPointDto Info { get; set; }

        public ValuesDto Values { get; set; }

        public Endpoint(HttpMethodEnum http, string url, ValueDto[] vals)
        {
            Info = new EndPointDto()
            {
                HTTP = http,
                URL = url,
                Vals = vals
            };

            List<int> i = new List<int>();
            List<float> f = new List<float>();
            List<bool> b = new List<bool>();
            foreach (var v in vals)
            {
                switch (v.Type)
                {
                    case ValType.INT: i.Add(0); break;
                    case ValType.FLOAT: f.Add(0.0f); break;
                    case ValType.BOOL: b.Add(false); break;
                }
            }
            Values= new ValuesDto()
            { Ints=i.ToArray() ,Floats=f.ToArray(),Bools=b.ToArray()};
        }

        public IList getCollection(ValType valType) 
        {
            switch (valType)
            {
                case ValType.INT: return Values.Ints;
                case ValType.FLOAT: return Values.Floats;
                case ValType.BOOL: return Values.Bools;
                default:return new object[0];
            }
        }
    }
}
