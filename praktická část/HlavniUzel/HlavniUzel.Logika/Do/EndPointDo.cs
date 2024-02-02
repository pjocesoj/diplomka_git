using HlavniUzel.Komunikace.Enums;

namespace HlavniUzel.Logika.Do
{
    public class EndPointDo
    {
        public HttpMethodEnum HTTP { get; set; }
        public string URL { get; set; } = "";

        public List<ValueDo<int>> Ints { get; } = new List<ValueDo<int>>();
        public List<ValueDo<float>> Flots { get; } = new List<ValueDo<float>>();
        public List<ValueDo<bool>> Bools { get; } = new List<ValueDo<bool>>();
    }
}
