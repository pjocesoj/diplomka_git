using MainNode.Communication.Dto;
using MainNode.Communication.Enums;
using MainNode.Communication.Interfaces;
using MainNode.Logic.Do;
using MainNode.Logic.Extentions;

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
            ValuesDto data = await _comm.GetValues(EP.Path,Mapper.Map(EP.Arguments));

            EP.Values.Ints.CopyValues(data.Ints);
            EP.Values.Floats.CopyValues(data.Floats);
            EP.Values.Bools.CopyValues(data.Bools);
        }
        public async Task GetAllValues()
        {
            //var gets = Array.FindAll(EndPoints, (x => (x.Path as HttpEndPointPath).HttpMethod == HttpMethodEnum.GET));
            var gets = Array.FindAll(EndPoints, (x => x.Type == EndpointType.GET));
            foreach (var get in gets)
            {
                await GetValues(get);
            }
        }

        public async Task SetValues(EndPointDo EP)
        {
            bool ok = await _comm.SetValues(EP.Path, Mapper.Map(EP.Arguments));
        }
    }
}
