using System;
using System.IO;
using System.Net;
using DarkMultiPlayerCommon;
using System.Reflection;
using System.Collections.Generic;
using SettingsParser;

namespace DarkMultiPlayerServer
{
    public class GameplaySettings
    {
        private const string SETTINGS_FILE_NAME = "gameplaysettings.txt";
        private static string settingsFile = Path.Combine(Server.configDirectory, SETTINGS_FILE_NAME);

        public static ConfigParser<GameplaySettingsStore> settingsStore = new ConfigParser<GameplaySettingsStore>(new GameplaySettingsStore(), settingsFile);
    }

    public class GameplaySettingsStore
    {
        [Description("Allow Stock Vessels")]
        public bool allowStockVessels = false;
        [Description("Auto-Hire Crewmemebers before Flight")]
        public bool autoHireCrews = true;
        [Description("No Entry Purchase Required on Research")]
        public bool bypassEntryPurchaseAfterResearch = true;
        [Description("Indestructible Facilities")]
        public bool indestructibleFacilities = false;
        [Description("Missing Crews Respawn")]
        public bool missingCrewsRespawn = true;
        // Career Settings
        [Description("Funds Rewards")]
        public float fundsGainMultiplier = 1.0f;
        [Description("Funds Penalties")]
        public float fundsLossMultiplier = 1.0f;
        [Description("Reputation Rewards")]
        public float repGainMultiplier = 1.0f;
        [Description("Reputation Penalties")]
        public float repLossMultiplier = 1.0f;
        [Description("Science Rewards")]
        public float scienceGainMultiplier = 1.0f;
        [Description("Starting Funds")]
        public float startingFunds = 25000.0f;
        [Description("Starting Reputation")]
        public float startingReputation = 0.0f;
        [Description("Starting Science")]
        public float startingScience = 0.0f;
    }
}
