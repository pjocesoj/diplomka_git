using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Enums;
using HlavniUzel.Logika.Do;

namespace HlavniUzel.Logika
{
    public static class Convertor
    {
        public static EndPointDo Map(EndPointDto dto)
        {
            var ret = new EndPointDo()
            {
                HTTP = dto.HTTP,
                URL = dto.URL
            };

            foreach (ValueDto val in dto.Vals)
            {
                switch (val.Type)
                {
                    case ValType.INT: ret.Ints.Add(new ValueDo<int>(val.Name, 0)); break;
                    case ValType.FLOAT: ret.Flots.Add(new ValueDo<float>(val.Name, 0)); break;
                    case ValType.BOOL: ret.Bools.Add(new ValueDo<bool>(val.Name, true)); break;
                }
            }

            return ret;
        }
    }
}
