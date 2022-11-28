using Morpeh;
using WebApplication1.TestComponents;

namespace WebApplication1.TestSystems;

public class TestSystem : ISystem
{
    private Filter _filter;
    
    public World World { get; set; }
    
    public void OnAwake()
    {
        _filter = World.Filter.With<TestComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            ref var test = ref entity.GetComponent<TestComponent>();
            var random = new Random();
            test.TestInt = random.Next(10, 100);
            Console.WriteLine("Привет из сестемы TestSystem 1!!!!!!!!!!!!");
            entity.AddComponent<TestComponent2>();
        }
    }
    
    public void Dispose()
    {
        _filter = null;
    }
}