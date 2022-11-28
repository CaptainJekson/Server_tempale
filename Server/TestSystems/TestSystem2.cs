using Morpeh;
using WebApplication1.TestComponents;

namespace WebApplication1.TestSystems;

public class TestSystem2 : ISystem
{
    private Filter _filter;
    
    public World World { get; set; }
    
    public void OnAwake()
    {
        _filter = World.Filter.With<TestComponent>().With<TestComponent2>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            ref var test = ref entity.GetComponent<TestComponent>();
            Console.WriteLine($"Привет из сестемы TestSystem 2 result = {test.TestInt}!!!!!!!!!!!!");
            entity.AddComponent<Destroy>();
        }
    }
    
    public void Dispose()
    {
        _filter = null;
    }
}