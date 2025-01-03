﻿using MainNode.Logic.Compile;

namespace MainNode.Logic.Test.Compile.Multitype.Flow;

[TestClass]
public class LoopCompilerMultiFlowTests
{
    protected LoopCompiler _loopCompiler;
    protected FlowRepository _flowRepo;

    [TestInitialize]
    public void Initialize()
    {
        _flowRepo = new FlowRepository();
        var nodeRepo = new NodeRepository();
        var funcRepo = new FuncRepo();
        _loopCompiler = new LoopCompiler(_flowRepo, nodeRepo, funcRepo);

        _loopCompiler.Compile("int i=1+2");
        _loopCompiler.Compile("float f=1.5+0.5");
        _loopCompiler.Compile("bool b=true+true");
    }
}
