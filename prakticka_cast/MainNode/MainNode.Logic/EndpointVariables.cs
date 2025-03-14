using MainNode.Communication.Dto;
using MainNode.Logic.Do;
using MainNode.Logic.Extensions;

namespace MainNode.Logic
{
    public class EndpointVariables
    {
        public List<string> Variables { get; set; } = new List<string>();

        public Node Node { get; set; }

        public EndPointDo EndPoint { get; set; }

        public bool IsActual { get; set; }

        public bool Loading { get; private set; } = false;
        public bool Loaded { get; private set; } = false;
        private ValuesDto? _data = null;

        public async Task Load()
        {
            Loaded = false;
            Loading = true;
            IsActual = false;

            _data = await Node.ParalelCall(EndPoint);

            Loaded = true;
            Loading = false;
        }

        public async Task UpdateValues()
        {
            if (_data == null) { return; }
            EndPoint.Values.Ints.CopyValues(_data.Ints);
            EndPoint.Values.Floats.CopyValues(_data.Floats);
            EndPoint.Values.Bools.CopyValues(_data.Bools);

            IsActual = true;
        }

        public async Task GetValues()
        {
            await Node.GetValues(EndPoint);
        }
    }
}