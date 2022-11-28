using Morpeh;
using WebApplication1.TestComponents;

namespace WebApplication1.TestSystems;

public class TestLateSystem : ILateSystem
{
    private Filter _filter;
    
    public World World { get; set; }
    
    public void OnAwake()
    {
        _filter = World.Filter.With<Destroy>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            Console.WriteLine("Удаление сущности");
            entity.Dispose();
        }
    }
    
    public void Dispose()
    {
        _filter = null;
    }
}