using System.Diagnostics;
using Morpeh;
using NetCoreProfiler;
using WebApplication1;
using WebApplication1.GlobalUtils;
using WebApplication1.StartupModule;
using WebApplication1.TestHandlers;

//Reptide
ushort port = 7777;
ushort maxClients = 10;
var server = new Riptide.Server();
RiptideServer.Server = server;
server.Start(port, maxClients);
var handler = new TestMessageHandler();

//TimeCycle
var stopWatch = new Stopwatch();
var targetMilliseconds = 5d;
var frameRateTimer = 0f;
var framesPerSecond = 0;

Time.Initialize();

//Morpeh ECS
WorldExtensions.InitializationDefaultWorld();
var world = World.Default;

var systemExecutor = new SystemsExecutor(world);
StartupInstaller.Install(world);

while (true)
{
    stopWatch.Reset();
    stopWatch.Start();
    Profiler.BeginFrame();
    
    //Console.WriteLine(Time.deltaTime);
    
    framesPerSecond++;
    Time.UpdateDeltaTime();
    //Console.WriteLine($"Systems {Time.deltaTime}");
    
    server.Update();
    systemExecutor.Execute();
    
    frameRateTimer += Time.deltaTime;
    if (frameRateTimer >= 1f)
    {
        //Console.WriteLine($"Global Server FPS: {framesPerSecond}");
        framesPerSecond = 0;
        frameRateTimer = 0f;
    }

    stopWatch.Stop();

    var timeLeft = (int) (targetMilliseconds - stopWatch.Elapsed.TotalMilliseconds);
                    
    if (timeLeft > 0)
        Thread.Sleep(timeLeft);
}
