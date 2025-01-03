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
            ret.Path = new HttpEndPointPath()
            {
                HttpMethod = dto.HTTP,
                Path = dto.URL
            };
            ret.Type = dto.Type;
            ret.Values = Map(dto.Vals);
            ret.Arguments = Map(dto.Args);
            ret.Delay = dto.Delay;

            return ret;
        }

        public static ValuesDto Map(ValuesDo Do)
        {
            var i = Do.Ints.Select(x => x.Value).ToArray();
            var f = Do.Floats.Select(x => x.Value).ToArray();
            var b = Do.Bools.Select(x => x.Value).ToArray();

            return new ValuesDto() { Ints = i, Floats = f, Bools = b };
        }

        public static ValuesDo Map(ValueDto[] dto)
        {
            var ret = new ValuesDo();
            foreach (var val in dto)
            {
                switch (val.Type)
                {
                    case ValType.INT: ret.Ints.Add(new ValueDo<int>(val.Name, 0)); break;
                    case ValType.FLOAT: ret.Floats.Add(new ValueDo<float>(val.Name, 0)); break;
                    case ValType.BOOL: ret.Bools.Add(new ValueDo<bool>(val.Name, true)); break;
                }
            }
            return ret;
        }
        public static ValuesDo Map(ValueArgDto[] dto)
        {
            var ret = new ValuesDo();
            foreach (var val in dto)
            {
                switch (val.Type)
                {
                    case ValType.INT: ret.Ints.Add(Map<int>(val)); break;
                    case ValType.FLOAT: ret.Floats.Add(Map<float>(val)); break;
                    case ValType.BOOL: ret.Bools.Add(Map<bool>(val)); break;
                }
            }
            return ret;
        }
        #region string to value
        private static T defaultValue<T>(string str)
        {
            if (string.IsNullOrEmpty(str)) { return default(T); }
            return (T)Convert.ChangeType(str, typeof(T));
        }
        private static T minValue<T>(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (typeof(T) == typeof(int)) { return (T)(object)int.MinValue; }
                if (typeof(T) == typeof(float)) { return (T)(object)float.MinValue; }
                if (typeof(T) == typeof(bool)) { return (T)(object)false; }
            }
            return (T)Convert.ChangeType(str, typeof(T));
        }
        private static T maxValue<T>(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (typeof(T) == typeof(int)) { return (T)(object)int.MaxValue; }
                if (typeof(T) == typeof(float)) { return (T)(object)float.MaxValue; }
                if (typeof(T) == typeof(bool)) { return (T)(object)true; }
            }
            return (T)Convert.ChangeType(str, typeof(T));
        }
        #endregion
        public static ValueArgDo<T> Map<T>(ValueArgDto dto)
        {
            T min = minValue<T>(dto.Min);
            T max = maxValue<T>(dto.Max);
            T def = defaultValue<T>(dto.Default);

            return new ValueArgDo<T>(dto.Name, def, min, max, def);
        }
    }
}
