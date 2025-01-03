using MainNode.Communication.Interfaces;
using MainNode.Logic.Do;

namespace MainNode.Logic.Test
{
    public class MockNode
    {
        public Node Node { get; private set; }

        #region Get values
        public ValueDo<int> Get_a { get; private set; }= new ValueDo<int>("a", 0);
        public ValueDo<float> Get_b { get; private set; }= new ValueDo<float>("b", 0);
        public ValueDo<bool> Get_c { get; private set; }= new ValueDo<bool>("c", false);
        #endregion

        #region Set values
        public ValueDo<int> Set_a { get; private set; }= new ValueDo<int>("a", 0);
        public ValueDo<float> Set_b { get; private set; }= new ValueDo<float>("b", 0);
        public ValueDo<bool> Set_c { get; private set; }= new ValueDo<bool>("c", false);
        #endregion
        
        public ValueDo<int> Slow_a { get; private set; }= new ValueDo<int>("a", 0);
        public MockNode(INodeCommunication comm)
        {
            var get = endpointGet();
            var set = endpointSet();
            var slow = endpointSlow();

            //var eps = getEndPoints();
            var eps = new EndPointDo[] { get, set, slow };

            Node = new Node(comm);
            Node.Name = "mock";
            Node.Address = "localhost:8080";
            Node.EndPoints = eps;
        }

        private EndPointDo endpointGet()
        {
            var get = new EndPointDo
            {
                Path = new Communication.Dto.HttpEndPointPath() { Path = "/getValuesG" },
                Type = Communication.Enums.EndpointType.GET,
                Values = new ValuesDo(),
                Arguments = new ValuesDo()
            };
            get.Values.Ints.Add(Get_a);
            get.Values.Floats.Add(Get_b);
            get.Values.Bools.Add(Get_c);

            return get;
        }
        private EndPointDo endpointSet()
        {
            var set = new EndPointDo
            {
                Path = new Communication.Dto.HttpEndPointPath() { Path = "/setValues", HttpMethod = Communication.Enums.HttpMethodEnum.POST },
                Type = Communication.Enums.EndpointType.SET,
                Values = new ValuesDo(),
                Arguments = new ValuesDo()
            };
            var a = new ValueDo<int>("a", 1);
            var b = new ValueDo<float>("b", 0);
            var c = new ValueDo<bool>("c", false);

            set.Values.Ints.Add(Set_a);
            set.Values.Floats.Add(Set_b);
            set.Values.Bools.Add(Set_c);

            set.Arguments.Ints.Add(Set_a);
            set.Arguments.Floats.Add(Set_b);
            set.Arguments.Bools.Add(Set_c);

            //set.Arguments.Ints.Add(new ValueDo<int>("a", 1));
            //set.Arguments.Floats.Add(new ValueDo<float>("b", 0));
            //set.Arguments.Bools.Add(new ValueDo<bool>("c", false));

            return set;
        }
        private EndPointDo endpointSlow()
        {
            var slow = new EndPointDo
            {
                Path = new Communication.Dto.HttpEndPointPath() { Path = "/slow" },
                Type = Communication.Enums.EndpointType.GET,
                Values = new ValuesDo(),
                Arguments = new ValuesDo(),
                Delay = 1100
            };
            slow.Values.Ints.Add(Slow_a);

            return slow;
        }

    }
}
