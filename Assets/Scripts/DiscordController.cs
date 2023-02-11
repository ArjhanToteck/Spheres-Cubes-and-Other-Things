using UnityEngine;
using Discord;
using System;

public class DiscordController : MonoBehaviour
{
/*#if UNITY_STANDALONE_WIN
    // sets status on start
    void Start()
    {

        if (DiscordSettings.set == false)
        {
            // checks if discord is running
            for (var i = 0; i < System.Diagnostics.Process.GetProcesses().Length; i++)
            {
                if (System.Diagnostics.Process.GetProcesses()[i].ToString() == "System.Diagnostics.Process (Discord)")
                {
                    DiscordSettings.discordRunning = true;
                    break;
                }
            }

            if (DiscordSettings.discordRunning)
            {
                DiscordSettings.discord = new Discord.Discord(802747842061795339, (ulong)CreateFlags.Default);
                ActivityManager activityManager = DiscordSettings.discord.GetActivityManager();

                DiscordSettings.activity = new Activity
                {
                    Timestamps =
            {
                Start = ToUnixTime()
    },

                    Assets =
            {
                LargeImage = "icon"
            }
                };

                activityManager.UpdateActivity(DiscordSettings.activity, (response) =>
                {
                    if (response == Result.Ok)
                    {
                        Debug.Log("Status set.");
                    }
                    else
                    {
                        Debug.LogError("Status failed to set.");
                    }
                });
            }

            DiscordSettings.set = true;
        }
    }

    // runs callbacks forever
    void Update()
    {
        if (DiscordSettings.discordRunning)
        {
            DiscordSettings.discord.RunCallbacks();
        }
    }

    // deletes status when closed
    void OnApplicationQuit()
    {
        if (DiscordSettings.discordRunning)
        {
            DiscordSettings.discord.Dispose();
        }
    }

    long ToUnixTime()
    {
        DateTime now = DateTime.Now;
        long unixTime = ((DateTimeOffset)now).ToUnixTimeSeconds();

        return unixTime;
    }
#endif*/
}