using MainNode.Communication.Dto;
using MainNode.Logic.Do;
using MainNode.Logic.Extentions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MainNode.Logic.Evaluation
{
    public class ParalelLoading
    {
        private ValuesDto? _data = null;
        private EndPointDo _ep;
        private Node _node;
        public bool Loaded { get; private set; } = true;
        public ParalelLoading(Node n, EndPointDo ep)
        {
            _node = n;
            _ep = ep;
        }

        public async Task Load()
        {
            Loaded = false;
            _data=await _node.ParalelCall(_ep);
            Loaded = true;
        }

        public void UpdateValues()
        {
            if (_data == null) { return; }
            _ep.Values.Ints.CopyValues(_data.Ints);
            _ep.Values.Floats.CopyValues(_data.Floats);
            _ep.Values.Bools.CopyValues(_data.Bools);
        }
    }
}
