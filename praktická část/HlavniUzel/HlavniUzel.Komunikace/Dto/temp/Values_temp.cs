namespace HlavniUzel.Komunikace.Dto.temp
{
    /// <summary>
    /// dočasné DTO než převedu ESP8266 PoC na realnou
    /// </summary>
    internal class Values_temp
    {
        public ValueDto_temp<int>[] Ints { get; set; }
        public ValueDto_temp<float>[] Floats { get; set; }
        public ValueDto_temp<bool>[] Bools { get; set; }

        public static implicit operator ValuesDto(Values_temp temp)
        {
            var i = temp.Ints.Select(x => x.Value).ToArray();
            var f = temp.Floats.Select(x => x.Value).ToArray();
            var b = temp.Bools.Select(x => x.Value).ToArray();

            return new ValuesDto() { Ints = i, Floats = f, Bools=b};
        }
    }

    internal class ValueDto_temp<T>
    {
        public T Value { get; set; }
    }
}
