using HlavniUzel.Komunikace.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NodeEmulator
{
    public class ValueDo<T>: ValueDto
    {
        [JsonIgnore]
        public required T Value { get; set; }

        public string ValueToString()
        {
            return Value.ToString();
        }

        public void ValueFromString(string src)
        {
            Value= (T)Convert.ChangeType(src, typeof(T));
        }
    }
}
