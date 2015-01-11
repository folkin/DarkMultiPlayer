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

        public static void HandleScienceChanged(ClientObject client, System.Collections.ObjectModel.ReadOnlyCollection<ClientObject> clients, byte[] messageData)
        {
            foreach (var c in clients)
            {
                if (c != client)
                {
                    ServerMessage newMessage = new ServerMessage();
                    newMessage.type = ServerMessageType.SCIENCE_CHANGED;
                    newMessage.data = messageData;
                    ClientHandler.SendToClient(c, newMessage, true);
                    DarkLog.Debug("Sending Science changed to " + c.playerName);
                }
            }
        }

        public static void HandleReputationChanged(ClientObject client, System.Collections.ObjectModel.ReadOnlyCollection<ClientObject> clients, byte[] messageData)
        {
            foreach (var c in clients)
            {
                if (c != client)
                {
                    ServerMessage newMessage = new ServerMessage();
                    newMessage.type = ServerMessageType.REPUTATION_CHANGED;
                    newMessage.data = messageData;
                    ClientHandler.SendToClient(c, newMessage, true);
                    DarkLog.Debug("Sending Reputation changed to " + c.playerName);
                }
            }
        }
    }
}
