using MainNode.Communication.Dto;
using MainNode.Communication.Enums;
using MainNode.Communication.Helpers;
using MainNode.Communication.Interfaces;
using MainNode.Logic.Do;
using MainNode.Logic.Extentions;
using MainNode.Logic.Interfaces;
using System.Text.Json.Serialization;

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

        public string Name { get; set; } = $"node{INodeRepository.Count}";

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

        [JsonIgnore]
        public ConnectionStatus ConnectionStatus { get; set; } = new ConnectionStatus(5, 5);
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
            try
            {
                ValuesDto? data = await _comm.GetValues(EP.Path, EP.Delay, Mapper.Map(EP.Arguments));

                if (data == null) { return; }
                EP.Values.Ints.CopyValues(data.Ints);
                EP.Values.Floats.CopyValues(data.Floats);
                EP.Values.Bools.CopyValues(data.Bools);

                ConnectionStatus.Success();
            }
            catch (Exception e)
            {
                ConnectionStatus.Failure(e.Message, EP);
            }
        }
        public async Task GetAllValues()
        {
            var gets = Array.FindAll(EndPoints, (x => x.Type == EndPointType.GET));
            foreach (var get in gets)
            {
                await GetValues(get);
            }
        }

        public async Task SetValues(EndPointDo EP)
        {
            try
            {
                bool ok = await _comm.SetValues(EP.Path, Mapper.Map(EP.Arguments));
            }
            catch (Exception e)
            {
                ConnectionStatus.Failure(e.Message, EP);
            }
        }

        /// <summary>
        /// pro pomalé endpointy, které mohou běžet během iterace smyčky <br/>
        /// načte hodnoty, ale neprovede aktualizaci, aby nenarušil integritu výpočtu
        /// </summary>
        /// <param name="EP"></param>
        /// <returns></returns>
        public async Task<ValuesDto?> ParalelCall(EndPointDo EP)
        {
            try
            {
                return await _comm.GetValues(EP.Path, EP.Delay, Mapper.Map(EP.Arguments));
            }
            catch (Exception e)
            {
                ConnectionStatus.Failure(e.Message, EP);
                return null;
            }
        }
    }
}
