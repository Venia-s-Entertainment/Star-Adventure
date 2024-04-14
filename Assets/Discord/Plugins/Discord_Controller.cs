using UnityEditor;
using UnityEngine;
using Discord;

public class Discord_Controller : MonoBehaviour
{
    private string Details;
    private string State;
    private Discord.Discord discord;
    private Discord.Activity dsActivity;
    private long time;
    // Start is called before the first frame update
    void Start()
    {
        Discord_Controller[] ds = FindObjectsOfType<Discord_Controller>();

        if (ds.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);


        discord = new Discord.Discord(1118835913091461153, (ushort)Discord.CreateFlags.NoRequireDiscord);
        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();
    }
    private void LateUpdate()
    {
        if (CommandHandler.currentScene == 0)
        {
            State = "Idling";
            Details = "MainMenu";
        }
        if (CommandHandler.currentScene == 1)
        {
            State = PlayerStats.isPiloting ? "Travelling" : "Walking";
            Details = PlayerStats.celestial != null ? $"On planet {PlayerStats.celestial.Name}" : "In space";
        }
        if (CommandHandler.currentScene == 2)
        {
            State = "Unknown";
            Details = "Unknown";
        }

        UpdateStatus();
    }
    private void Update()
    {
        discord.RunCallbacks();
    }
    void UpdateStatus()
    {
        dsActivity.Assets.LargeText = "Star Adventure";
        dsActivity.Assets.LargeImage = "main";
        dsActivity.Details = Details;
        dsActivity.Timestamps.Start = time;
        dsActivity.State = State;

        var activityManager = discord.GetActivityManager();
        activityManager.UpdateActivity(dsActivity,(res) =>
        {
            if (res != Result.Ok)
                Debug.LogWarning("Cannot connect to Discord");
        });
    }
}
