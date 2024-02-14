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
        public T Value { get; set; }
    }
}
