using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Enums;
using HlavniUzel.Komunikace.Interfaces;
using MainNode.Logic.Do;

namespace MainNode.Logic
{
    public class Node
    {
        private readonly INodeCommunication _comm;

        public string Name { get; set; } = "node";

        private string _address;
        /// <summary>
        /// IP, COM port, ...
        /// </summary>
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                _comm.Init(_address);
                //_comm.Init("192.168.1.233");
            }
        }

        public EndPointDo[] EndPoints { get; set; } = new EndPointDo[0];
        public Node(INodeCommunication comm)
        {
            this._comm = comm;
        }

        public async Task GetEndPoints()
        {
            try
            {
                var ep = await _comm.GetEndPoints();
                if (ep != null)
                {
                    var dos = ep.Select(x => Mapper.Map(x));
                    EndPoints = dos.ToArray();
                }
            }
            catch { throw; }
        }

        public async Task GetValues(EndPointDo EP)
        {
            ValuesDto data = await _comm.GetValues(EP.Path);

            for (int i = 0; i < EP.Ints.Count; i++)
            {
                EP.Ints[i].Value = data.Ints[i];
            }
            for (int i = 0; i < EP.Flots.Count; i++)
            {
                EP.Flots[i].Value = data.Floats[i];
            }
            for (int i = 0; i < EP.Bools.Count; i++)
            {
                EP.Bools[i].Value = data.Bools[i];
            }
        }
        public async Task GetAllValues()
        {
            var gets = Array.FindAll(EndPoints, (x => (x.Path as HttpEndPointPath).HttpMethod == HttpMethodEnum.GET));
            foreach (var get in gets)
            {
                await GetValues(get);
            }
        }

        public async Task SetValues(EndPointPath path, ValuesDo newVals)
        {
            bool ok = await _comm.SetValues(path, Mapper.Map(newVals));
        }
    }
}
