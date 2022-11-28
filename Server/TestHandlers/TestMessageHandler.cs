using Riptide;
using WebApplication1.GlobalUtils;

namespace WebApplication1.TestHandlers;

public class TestMessageHandler
{
    [MessageHandler(2)]
    private static void HandleSomeMessageFromServer2(ushort playerId, Message message)
    {
        var strTest = message.GetString();
        var intTest = message.GetInt();
        var floatTest = message.GetFloat();
        
        Debug.LogColor($"{strTest} | {intTest} | {floatTest} | player id: {playerId}", ConsoleColor.Blue);
    }
}