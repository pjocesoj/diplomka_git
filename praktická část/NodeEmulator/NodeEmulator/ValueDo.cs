using MainNode.Communication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NodeEmulator
{
    public class ValueDo<T>: ValueDo
    {
        [JsonIgnore]
        public required T Value { get; set; }

        public override string ValueToString()
        {
            return Value.ToString();
        }

        public override void ValueFromString(string src)
        {
            Value= (T)Convert.ChangeType(src, typeof(T));
        }
    }

    /// <summary>
    /// virtualní předek generiky abych se vyhnul potřebě spousty if
    /// </summary>
    public abstract class ValueDo : ValueDto 
    {
        public abstract string ValueToString();
        public abstract void ValueFromString(string src);
    }
}
