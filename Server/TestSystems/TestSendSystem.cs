using Morpeh;
using Riptide;
using WebApplication1.GlobalUtils;
using WebApplication1.TestComponents;

namespace WebApplication1.TestSystems;

public class TestSendSystem : ISystem
{
    private Filter _filter;
    private Riptide.Server _server;
    
    public World World { get; set; }

    private float _rate;

    public void OnAwake()
    {
        _filter = World.Filter.With<TestMessage>();
        _server = RiptideServer.Server;
        _server.ClientConnected += OnClientConnected;
        _server.ClientDisconnected += OnClientDisconnected;
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            ref var testMessage = ref entity.GetComponent<TestMessage>();

            _rate += deltaTime;

            if (_rate >= 1)
            {
                _rate = 0;
                
                var message = Message.Create(MessageSendMode.Reliable, (ushort)MessageType.Test);
                message.AddString(testMessage.StrTest);
                message.AddInt(testMessage.IntTest);
                message.AddFloat(testMessage.FloatTest);
                _server.SendToAll(message);
            }
        }
    }
    
    private void OnClientConnected(object? sender, ServerConnectedEventArgs e)
    {
        Debug.LogColor("Client connected", ConsoleColor.Yellow);
    }
    
    private void OnClientDisconnected(object? sender, ServerDisconnectedEventArgs e)
    {
        Debug.LogColor("Client disconnected", ConsoleColor.Yellow);
    }
    
    public void Dispose()
    {
        _server.ClientConnected -= OnClientConnected;
        _server.ClientDisconnected -= OnClientDisconnected;
        _filter = null;
    }
}