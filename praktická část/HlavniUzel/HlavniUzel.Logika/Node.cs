using HlavniUzel.Komunikace.Dto;
using HlavniUzel.Komunikace.Interfaces;
using HlavniUzel.Logika.Do;

namespace HlavniUzel.Logika
{
    public class Node
    {
        private readonly INodeCommunication _comm;

        public string Name { get; set; } = "node";

        /// <summary>
        /// IP, COM port, ...
        /// </summary>
        public string Address { get; set; }

        public EndPointDo[] EndPoints { get; set; }=new EndPointDo[0];
        public Node(INodeCommunication comm) 
        {
            this._comm = comm;
        }

        public async Task GetEndPoints()
        {
            var ep=await _comm.GetEndPoints();
            if (ep != null) 
            {
                var dos=ep.Select(x=>Mapper.Map(x)); 
                EndPoints = dos.ToArray();
            }
        }

        public async Task GetValues(EndPointDo EP)
        {
            ValuesDto data = await _comm.GetValues(EP.URL);

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

        public async Task SetValues(string url, ValuesDo newVals)
        {
            bool ok=await _comm.SetValues(url,Mapper.Map(newVals));
        }
    }
}
