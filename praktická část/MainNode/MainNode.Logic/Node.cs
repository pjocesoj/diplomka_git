using MainNode.Communication.Dto;
using MainNode.Communication.Enums;
using MainNode.Communication.Helpers;
using MainNode.Communication.Interfaces;
using MainNode.Logic.Do;
using MainNode.Logic.Extentions;

namespace MainNode.Logic
{
    public class Node
    {
        private INodeCommunication _comm;

        private string _addressType;
        public string AddressType
        {
            get { return _addressType; }
            set
            {
                _addressType = value;
                _comm = CommunicationTypeResolver.GetCommunicationType(AddressType!);
            }
        }

        public string Name { get; set; } = $"node{NodeRepository.Count}";

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
            AddressType = comm.AddressType;
        }
        public Node()
        {
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
            ValuesDto? data = await _comm.GetValues(EP.Path, null, Mapper.Map(EP.Arguments));

            EP.Values.Ints.CopyValues(data.Ints);
            EP.Values.Floats.CopyValues(data.Floats);
            EP.Values.Bools.CopyValues(data.Bools);
        }
        public async Task GetAllValues()
        {
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
