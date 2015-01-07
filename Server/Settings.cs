using System;
using System.IO;
using System.Net;
using DarkMultiPlayerCommon;
using System.Reflection;
using System.Collections.Generic;
using SettingsParser;

namespace DarkMultiPlayerServer
{
    public class Settings
    {
        private const string SETTINGS_FILE_NAME = "settings.txt";
        private static string settingsFile = Path.Combine(Server.configDirectory, SETTINGS_FILE_NAME);

        public static ConfigParser<SettingsStore> settingsStore = new ConfigParser<SettingsStore>(new SettingsStore(), settingsFile);
    }

    public class SettingsStore
    {
        [Description("The address the server listens on.\r\n# WARNING: You do not need to change this unless you are running 2 servers on the same port.\r\n# Changing this setting from 0.0.0.0 will only give you trouble if you aren't running multiple servers.\r\n# Change this setting to :: to listen on IPv4 and IPv6.")]
        public string address = "0.0.0.0";
        [Description("The port the server listens on.")]
        public int port = 6702;
        [Description("Specify the warp type.")]
        public WarpMode warpMode = WarpMode.SUBSPACE;
        [Description("Specify the game type.")]
        public GameMode gameMode = GameMode.SANDBOX;
        [Description("Specify the gameplay difficulty of the server.")]
        public GameDifficulty gameDifficulty = GameDifficulty.NORMAL;
        [Description("Enable white-listing.")]
        public bool whitelisted = false;
        [Description("Enable mod control.\r\n# WARNING: Only consider turning off mod control for private servers.\r\n# The game will constantly complain about missing parts if there are missing mods.")]
        public ModControlMode modControl = ModControlMode.ENABLED_STOP_INVALID_PART_SYNC;
        [Description("Specify if the the server universe 'ticks' while nobody is connected or the server is shut down.")]
        public bool keepTickingWhileOffline = true;
        [Description("If true, sends the player to the latest subspace upon connecting. If false, sends the player to the previous subspace they were in.\r\n# NOTE: This may cause time-paradoxes, and will not work across server restarts.")]
        public bool sendPlayerToLatestSubspace = true;
        [Description("Use UTC instead of system time in the log.")]
        public bool useUTCTimeInLog = false;
        [Description("Minimum log level.")]
        public DarkLog.LogLevels logLevel = DarkLog.LogLevels.DEBUG;
        [Description("Specify maximum number of screenshots to save per player. -1 = None, 0 = Unlimited")]
        public int screenshotsPerPlayer = 20;
        [Description("Specify vertical resolution of screenshots.")]
        public int screenshotHeight = 720;
        [Description("Enable use of cheats in-game.")]
        public bool cheats = true;
        [Description("HTTP port for server status. 0 = Disabled")]
        public int httpPort = 0;
        [Description("Name of the server.")]
        public string serverName = "DMP Server";
        [Description("Maximum amount of players that can join the server.")]
        public int maxPlayers = 20;
        [Description("Specify a custom screenshot directory.\r\n#This directory must exist in order to be used. Leave blank to store it in Universe.")]
        public string screenshotDirectory = string.Empty;
        [Description("Specify in minutes how often /nukeksc automatically runs. 0 = Disabled")]
        public int autoNuke = 0;
        [Description("Specify in minutes how often /dekessler automatically runs. 0 = Disabled")]
        public int autoDekessler = 30;
        [Description("How many untracked asteroids to spawn into the universe. 0 = Disabled")]
        public int numberOfAsteroids = 30;
        [Description("Specify the name that will appear when you send a message using the server's console.")]
        public string consoleIdentifier = "Server";
        [Description("Specify the server's MOTD (message of the day).")]
        public string serverMotd = "Welcome, %name%!";
        [Description("Specify the amount of days a screenshot should be considered as expired and deleted. 0 = Disabled")]
        public double expireScreenshots = 0;
        [Description("Specify whether to enable compression. Decreases bandwidth usage but increases CPU usage. 0 = Disabled")]
        public bool compressionEnabled = true;
        [Description("Specify the amount of days a log file should be considered as expired and deleted. 0 = Disabled")]
        public double expireLogs = 0;
    }
}