using Morpeh;
using WebApplication1.TestComponents;
using WebApplication1.TestSystems;

namespace WebApplication1.StartupModule;

public static class StartupInstaller
{
    public static void Install(World world)
    {
        //TODO потом сделать отдельны подмодули
        var systemsGroup = world.CreateSystemsGroup();
        systemsGroup.AddSystem(new TestSystem());
        systemsGroup.AddSystem(new TestSystem2());
        systemsGroup.AddSystem(new TestLateSystem());
        systemsGroup.AddSystem(new TestSendSystem());
        // world.CreateEntity().SetComponent(new TestMessage
        // {
        //     StrTest = "Lupa Pupa",
        //     IntTest = 31,
        //     FloatTest = 3.14f,
        // });
        
        world.AddSystemsGroup(0, systemsGroup);
    }
}