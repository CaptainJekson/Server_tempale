using Morpeh;
using NetCoreProfiler;
using WebApplication1.GlobalUtils;

namespace WebApplication1;

public class SystemsExecutor
{
    private World _world;
    
    public SystemsExecutor(World world)
    {
        _world = world;
    }
    
    public void Execute()
    {
        using (new NetCoreProfilerScope("MorpehFixedUpdate"))
        {
            _world.FixedUpdate(Time.deltaTime);
        }

        using (new NetCoreProfilerScope("MorpehUpdate"))
        {
            _world.Update(Time.deltaTime);
        }
                    
        using (new NetCoreProfilerScope("MorpehLateUpdate"))
        {
            _world.LateUpdate(Time.deltaTime);
        }
    }
}