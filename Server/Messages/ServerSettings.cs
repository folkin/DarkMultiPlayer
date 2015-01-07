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
                mw.Write<int>((int)Settings.settingsStore.Settings.warpMode);
                mw.Write<int>((int)Settings.settingsStore.Settings.gameMode);
                mw.Write<bool>(Settings.settingsStore.Settings.cheats);
                //Tack the amount of kerbals, vessels and scenario modules onto this message
                mw.Write<int>(numberOfKerbals);
                mw.Write<int>(numberOfVessels);
                //mw.Write<int>(numberOfScenarioModules);
                mw.Write<int>(Settings.settingsStore.Settings.screenshotHeight);
                mw.Write<int>(Settings.settingsStore.Settings.numberOfAsteroids);
                mw.Write<string>(Settings.settingsStore.Settings.consoleIdentifier);
                mw.Write<int>((int)Settings.settingsStore.Settings.gameDifficulty);
                if (Settings.settingsStore.Settings.gameDifficulty == GameDifficulty.CUSTOM)
                {
                    mw.Write<bool>(GameplaySettings.settingsStore.Settings.allowStockVessels);
                    mw.Write<bool>(GameplaySettings.settingsStore.Settings.autoHireCrews);
                    mw.Write<bool>(GameplaySettings.settingsStore.Settings.bypassEntryPurchaseAfterResearch);
                    mw.Write<bool>(GameplaySettings.settingsStore.Settings.indestructibleFacilities);
                    mw.Write<bool>(GameplaySettings.settingsStore.Settings.missingCrewsRespawn);
                    mw.Write<float>(GameplaySettings.settingsStore.Settings.fundsGainMultiplier);
                    mw.Write<float>(GameplaySettings.settingsStore.Settings.fundsLossMultiplier);
                    mw.Write<float>(GameplaySettings.settingsStore.Settings.repGainMultiplier);
                    mw.Write<float>(GameplaySettings.settingsStore.Settings.repLossMultiplier);
                    mw.Write<float>(GameplaySettings.settingsStore.Settings.scienceGainMultiplier);
                    mw.Write<float>(GameplaySettings.settingsStore.Settings.startingFunds);
                    mw.Write<float>(GameplaySettings.settingsStore.Settings.startingReputation);
                    mw.Write<float>(GameplaySettings.settingsStore.Settings.startingScience);
                }
                newMessage.data = mw.GetMessageBytes();
            }
            ClientHandler.SendToClient(client, newMessage, true);
        }
    }
}

