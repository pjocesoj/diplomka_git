using MainNode.Communication.Dto;
using MainNode.Communication.Enums;
using MainNode.Logic.Do;

namespace MainNode.Logic
{
    public static class Mapper
    {
        public static EndPointDo Map(EndPointDto dto)
        {
            var ret = new EndPointDo();
            ret.Path=new HttpEndPointPath()
            {
                HttpMethod = dto.HTTP,
                Path = dto.URL
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

        public static ValuesDto Map(ValuesDo Do)
        {
            var i = Do.Ints.Select(x => x.Value).ToArray();
            var f = Do.Floats.Select(x => x.Value).ToArray();
            var b = Do.Bools.Select(x => x.Value).ToArray();

            return new ValuesDto() { Ints = i, Floats = f, Bools = b };
        }
    }
}
