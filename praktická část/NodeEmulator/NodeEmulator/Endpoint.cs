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

            foreach (var val in vals) 
            {
                //var wtf = (val as ValueDo);
                //Values.Add(wtf.Value); 
            }
        }
    }
}
