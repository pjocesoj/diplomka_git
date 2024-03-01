using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainNode.Logic;
using HlavniUzel.Windows;
using System.Net;
using System.Windows;
using MainNode.Logic.Evaluation;

namespace HlavniUzel.ViewModels
{
    public partial class NodeViewModel : ObservableObject
    {
        private Node _node;
        private readonly NodeRepository _repo;

        public NodeViewModel(Node node, NodeRepository repo)
        {
            _node = node;
            _repo = repo;
            Name = _node.Name;
            Address = _node.Address;
        }
        [ObservableProperty]
        private string _name = "";

        [ObservableProperty]
        private string _address = "";

        public List<EndPointViewModel> EndPoints 
        {
            get
            {
                var ret=new List<EndPointViewModel>();
                foreach(var ep in _node.EndPoints) { ret.Add(new EndPointViewModel(ep)); }
                return ret;
            }
        }

        [RelayCommand]
        public async Task ButtonClick()
        {
            _node.Name = Name;
            _node.Address = Address;

            try
            {
                await _repo.AddNode(_node);
                App.Current.ShowWindow<NodeInfoWindow>(this);
            }
            catch(ApplicationException ex) { throw new ApplicationException(ex.Message,ex); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var ep = _node.EndPoints[2];
            List<Operation<int>> operations = new List<Operation<int>>()
            {
            new Operation<int>(ep.Values.Ints[0],FuncIntInt.Plus),
            new Operation<int>(2,FuncIntInt.Multiply),
            };
            var instr = new Flow<int>("", operations);
            var i=instr.Evaluate();
            instr.Output = ep.Arguments.Ints[0];
            instr.Run();

            var re = new FlowRepository();
            re.AddFlow(instr);

            List<Operation<float>> oper2=new List<Operation<float>>()
            {
                new Operation<float>(2.5f,FuncFloatFloat.Plus),
            new SubflowOperation<float,int>(instr,FuncFloatInt.devide)
            };
            var sub = new Flow<float>("", oper2);
            var f = sub.Evaluate();
            re.AddFlow(sub);

            var ep0 = _node.EndPoints[0];
            List<Operation<bool>> oper3 = new List<Operation<bool>>()
            {
                new RefConstOperation<bool,float>(ep0.Values.Floats[0],2,FuncBoolFloat.Greater),
            new Operation<bool>(true,FuncBoolBool.And)
            };
            var rc = new Flow<bool>("", oper3);
            var b = rc.Evaluate();
            re.AddFlow(rc);

            re.Run();
        }
    }
}
