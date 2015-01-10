using DarkMultiPlayerCommon;
using MessageStream2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkMultiPlayerServer.Messages
{
    public static class ResourceHandler
    {
        public static void HandleFundsChanged(ClientObject client, IEnumerable<ClientObject> clients, byte[] messageData)
        {
            foreach (var c in clients)
            {
                if (c != client)
                {
                    ServerMessage newMessage = new ServerMessage();
                    newMessage.type = ServerMessageType.FUNDS_CHANGED;
                    newMessage.data = messageData;
                    ClientHandler.SendToClient(c, newMessage, true);
                    DarkLog.Debug("Sending Funds changed to " + c.playerName);
                }
            }
        }
    }
}
