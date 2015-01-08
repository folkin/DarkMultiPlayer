using System;
using System.IO;
using System.Reflection;

namespace DarkMultiPlayerServer
{
    public class BackwardsCompatibility
    {
        public static void RemoveOldPlayerTokens()
        {
            string playerDirectory = Path.Combine(Server.universeDirectory, "Players");
            if (!Directory.Exists(playerDirectory))
            {
                return;
            }
            string[] playerFiles = Directory.GetFiles(playerDirectory, "*", SearchOption.TopDirectoryOnly);
            Guid testGuid;
            foreach (string playerFile in playerFiles)
            {
                try
                {
                    string playerText = File.ReadAllLines(playerFile)[0];
                    if (Guid.TryParse(playerText, out testGuid))
                    {
                        //Player token detected, remove it
                        DarkLog.Debug("Removing old player token for " + Path.GetFileNameWithoutExtension(playerFile));
                        File.Delete(playerFile);
                    }
                }
                catch
                {
                    DarkLog.Debug("Removing damaged player token for " + Path.GetFileNameWithoutExtension(playerFile));
                    File.Delete(playerFile);
                }
            }
        }

        public static void FixKerbals()
        {
            string kerbalPath = Path.Combine(Server.universeDirectory, "Kerbals");
            int kerbalCount = 0;

            while (File.Exists(Path.Combine(kerbalPath, kerbalCount + ".txt")))
            {
                string oldKerbalFile = Path.Combine(kerbalPath, kerbalCount + ".txt");
                string kerbalName = null;

                using (StreamReader sr = new StreamReader(oldKerbalFile))
                {
                    string fileLine;
                    while ((fileLine = sr.ReadLine()) != null)
                    {
                        if (fileLine.StartsWith("name = "))
                        {
                            kerbalName = fileLine.Substring(fileLine.IndexOf("name = ") + 7);
                            break;
                        }
                    }
                }

                if (!String.IsNullOrEmpty(kerbalName))
                {
                    DarkLog.Debug("Renaming kerbal " + kerbalCount + " to " + kerbalName);
                    File.Move(oldKerbalFile, Path.Combine(kerbalPath, kerbalName + ".txt"));
                }
                kerbalCount++;
            }

            if (kerbalCount != 0)
            {
                DarkLog.Normal("Kerbal database upgraded to 0.24 format");
            }
        }

        public static void ConvertSettings()
        {
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DMPServerSettings.txt")))
            {
                DarkLog.Debug("Skipping settings conversion");
                return;
            }

            FieldInfo[] settingFields = typeof(SettingsStore).GetFields();

            DarkLog.Debug("Converting settings...");
            using (FileStream fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DMPServerSettings.txt"), FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string currentLine = sr.ReadLine();
                        if (currentLine == null)
                        {
                            break;
                        }

                        string trimmedLine = currentLine.Trim();
                        if (String.IsNullOrEmpty(trimmedLine))
                        {
                            continue;
                        }

                        if (!trimmedLine.Contains(",") || trimmedLine.StartsWith("#"))
                        {
                            continue;
                        }

                        string currentKey = trimmedLine.Substring(0, trimmedLine.IndexOf(","));
                        string currentValue = trimmedLine.Substring(trimmedLine.IndexOf(",") + 1);

                        foreach (FieldInfo settingField in settingFields)
                        {
                            if (settingField.Name.ToLower() == currentKey)
                            {
                                if (settingField.FieldType == typeof(string))
                                {
                                    settingField.SetValue(Settings.settingsStore.Settings, currentValue);
                                }
                                if (settingField.FieldType == typeof(bool)) // We do not allow invalid values
                                {
                                    if (currentValue == "1" || currentValue.ToLower() == bool.TrueString.ToLower())
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, true);
                                    }
                                    else if (currentValue == "0" || currentValue.ToLower() == bool.FalseString.ToLower())
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, false);
                                    }
                                }
                                if (settingField.FieldType == typeof(double))
                                {
                                    double doubleValue;
                                    if (double.TryParse(currentValue, out doubleValue))
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, doubleValue);
                                    }
                                }
                                if (settingField.FieldType == typeof(float))
                                {
                                    float floatValue;
                                    if (float.TryParse(currentValue, out floatValue))
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, floatValue);
                                    }
                                }
                                if (settingField.FieldType == typeof(decimal))
                                {
                                    decimal decimalValue;
                                    if (decimal.TryParse(currentValue, out decimalValue))
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, decimalValue);
                                    }
                                }
                                if (settingField.FieldType == typeof(short))
                                {
                                    short shortValue;
                                    if (short.TryParse(currentValue, out shortValue))
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, shortValue);
                                    }
                                }
                                if (settingField.FieldType == typeof(int))
                                {
                                    int intValue;
                                    if (int.TryParse(currentValue, out intValue))
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, intValue);
                                    }
                                }
                                if (settingField.FieldType == typeof(long))
                                {
                                    long longValue;
                                    if (long.TryParse(currentValue, out longValue))
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, longValue);
                                    }
                                }
                                if (settingField.FieldType == typeof(uint))
                                {
                                    uint uintValue;
                                    if (uint.TryParse(currentValue, out uintValue))
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, uintValue);
                                    }
                                }
                                if (settingField.FieldType == typeof(ulong))
                                {
                                    ulong ulongValue;
                                    if (ulong.TryParse(currentValue, out ulongValue))
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, ulongValue);
                                    }
                                }
                                if (settingField.FieldType == typeof(ushort))
                                {
                                    ushort ushortValue;
                                    if (ushort.TryParse(currentValue, out ushortValue))
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, ushortValue);
                                    }
                                }
                                if (settingField.FieldType.IsEnum)
                                {
                                    int intValue = Int32.Parse(currentValue);
                                    Array enumValues = settingField.FieldType.GetEnumValues();
                                    if (intValue <= enumValues.Length)
                                    {
                                        settingField.SetValue(Settings.settingsStore.Settings, enumValues.GetValue(intValue));
                                    }
                                }
                            }
                        }
                    }
                }
                Settings.settingsStore.SaveSettings();
                File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DMPServerSettings.txt"));
            }
        }
    }
}

