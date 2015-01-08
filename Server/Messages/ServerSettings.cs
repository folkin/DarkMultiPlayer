using System;
using System.IO;
using DarkMultiPlayerCommon;
using MessageStream2;

namespace DarkMultiPlayerServer.Messages
{
    public class ServerSettings
    {
        public static void SendServerSettings(ClientObject client)
        {
            int numberOfKerbals = Directory.GetFiles(Path.Combine(Server.universeDirectory, "Kerbals")).Length;
            int numberOfVessels = Directory.GetFiles(Path.Combine(Server.universeDirectory, "Vessels")).Length;
            int numberOfScenarioModules = Directory.GetFiles(Path.Combine(Server.universeDirectory, "Scenarios", client.playerName)).Length;
            ServerMessage newMessage = new ServerMessage();
            newMessage.type = ServerMessageType.SERVER_SETTINGS;
            using (MessageWriter mw = new MessageWriter())
            {
                mw.Write<int>((int)Server.serverSettings.Settings.warpMode);
                mw.Write<int>((int)Server.serverSettings.Settings.gameMode);
                mw.Write<bool>(Server.serverSettings.Settings.cheats);
                //Tack the amount of kerbals, vessels and scenario modules onto this message
                mw.Write<int>(numberOfKerbals);
                mw.Write<int>(numberOfVessels);
                //mw.Write<int>(numberOfScenarioModules);
                mw.Write<int>(Server.serverSettings.Settings.screenshotHeight);
                mw.Write<int>(Server.serverSettings.Settings.numberOfAsteroids);
                mw.Write<string>(Server.serverSettings.Settings.consoleIdentifier);
                mw.Write<int>((int)Server.serverSettings.Settings.gameDifficulty);
                if (Server.serverSettings.Settings.gameDifficulty == GameDifficulty.CUSTOM)
                {
                    mw.Write<bool>(Server.gameplaySettings.Settings.allowStockVessels);
                    mw.Write<bool>(Server.gameplaySettings.Settings.autoHireCrews);
                    mw.Write<bool>(Server.gameplaySettings.Settings.bypassEntryPurchaseAfterResearch);
                    mw.Write<bool>(Server.gameplaySettings.Settings.indestructibleFacilities);
                    mw.Write<bool>(Server.gameplaySettings.Settings.missingCrewsRespawn);
                    mw.Write<float>(Server.gameplaySettings.Settings.fundsGainMultiplier);
                    mw.Write<float>(Server.gameplaySettings.Settings.fundsLossMultiplier);
                    mw.Write<float>(Server.gameplaySettings.Settings.repGainMultiplier);
                    mw.Write<float>(Server.gameplaySettings.Settings.repLossMultiplier);
                    mw.Write<float>(Server.gameplaySettings.Settings.scienceGainMultiplier);
                    mw.Write<float>(Server.gameplaySettings.Settings.startingFunds);
                    mw.Write<float>(Server.gameplaySettings.Settings.startingReputation);
                    mw.Write<float>(Server.gameplaySettings.Settings.startingScience);
                }
                newMessage.data = mw.GetMessageBytes();
            }
            ClientHandler.SendToClient(client, newMessage, true);
        }
    }
}

