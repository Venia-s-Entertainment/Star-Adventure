using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Reflection;
using Settings;
using System.Collections;

public class CommandHandler
{
    public const long password = 1612000825;
    public static int currentScene
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
    public static string GetSyntax(MethodInfo m)
    {
        string msg = null;

        foreach (ParameterInfo parameter in m.GetParameters())
        {
            msg += $"<{parameter}>";
        }

        return msg;
    }
    public static void Invoke(int Index, string parametr = null,  bool isAdmin = false)
    {
        Console c = Object.FindObjectOfType<Console>();

        MethodInfo[] userMethods = typeof(UserCommands).GetMethods();
        MethodInfo[] adminMethods = typeof(AdminCommands).GetMethods();

        try
        {
            if (!isAdmin)
            {
                System.Type dataType = userMethods[Index].GetParameters().Length < 1 ? null : userMethods[Index].GetParameters()[0].ParameterType;
                var parametrs = new object[] { parametr == null ? null : System.Convert.ChangeType(parametr, dataType) };

                userMethods[Index].Invoke(typeof(UserCommands), parametrs[0] == null ? null : parametrs);
            }
            else
            {
                System.Type dataType = adminMethods[Index].GetParameters().Length < 1 ? null : adminMethods[Index].GetParameters()[0].ParameterType;
                var parametrs = new object[] { parametr == null ? null : System.Convert.ChangeType(parametr, dataType) };

                adminMethods[Index].Invoke(typeof(AdminCommands), parametrs[0] == null ? null : parametrs);
            }
        }
        catch (System.Exception)
        {
            if (!isAdmin)
                c.Debug($"Syntax error: {UserCommands.commands[Index]} {GetSyntax(userMethods[Index])}", "red");
            else
                c.Debug($"Syntax error: {AdminCommands.commands[Index]} {GetSyntax(adminMethods[Index])}", "red");
        }
    }
}
class UserCommands
{
    public static string[] commands = { "help", "clr", "exit", "ls", "admin", "hp" };
    public static string[] description =
    {
        "show all available commands",
        "clear console",
        "close game",
        "load scene by its index",
        "get admin rights",
        "show current player hp",
    };
    public static void GetHelp()
    {
        Console c = Object.FindObjectOfType<Console>();

        MethodInfo[] user = typeof(UserCommands).GetMethods();
        MethodInfo[] admin = typeof(AdminCommands).GetMethods();

        c.Debug("=====User commands=====", "green");
        for (int i = 0; i < commands.Length; i++)
        {
            if (user[i].IsStatic)
                c.Debug($"{commands[i]} {CommandHandler.GetSyntax(user[i])} - {description[i]}", "green");
        }

        if (GameSettings.adminIsAavailable)
        {
            c.Debug("=====Admin commands=====", "green");

            for (int i = 0; i < AdminCommands.commands.Length; i++)
            {
                if (admin[i].IsStatic)
                    c.Debug($"{AdminCommands.commands[i]} {CommandHandler.GetSyntax(admin[i])} - {AdminCommands.description[i]}", "green");
            }
        }
    }
    public static void ClearConsole()
    {
        Object.FindObjectOfType<Console>().consoleText.text = null;
    }
    public static void ExitApplication()
    {
        Console c = Object.FindObjectOfType<Console>();
        c.Debug("Closing application", "cyan");

        Application.Quit();
    }
    public static void LoadScene(int index)
    {
        Console c = Object.FindObjectOfType<Console>();

        try
        {
            SceneManager.GetSceneByBuildIndex(index);
        }
        catch (System.ArgumentException)
        {
            c.Debug($"Scene with index: '{index}' does not exist!", "red");
            return;
        }

        SceneManager.LoadSceneAsync(index);
        c.Debug($"Loading scene with index: {index}", "cyan");
    }
    public static void GetAdminRights(long password)
    {
        Console c = Object.FindObjectOfType<Console>();

        if (GameSettings.adminIsAavailable)
        {
            c.Debug("You already have admin rights", "green");
            return;
        }
        if (password != CommandHandler.password)
        {
            c.Debug("Invalid password!", "red");
            return;
        }

        GameSettings.adminIsAavailable = true;
        c.Debug("Admin rights successfully received!", "cyan");
    }
    public static void ShowPlayerHealth()
    {
        Console c = Object.FindObjectOfType<Console>();

        c.Debug($"Current player hp: {PlayerSettings.currentHealth}", "green");
    }
}
class AdminCommands
{
    public static string[] commands = { "pls", "jforce", "nclp", "dmg", "heal", "maxhp", "ss" };
    public static string[] description =
    {
        "change player movement speed",
        "change player jump force",
        "activate/disable noclip",
        "activate/disable damage",
        "heal player",
        "change max hp value",
        "change simulation speed"
    };

    public static void SetPlayerSpeed(float value)
    {
        PlayerSettings.characterSpeed = value;
        Object.FindObjectOfType<Console>().Debug($"Player speed changed to {value}", "green");
    }
    public static void SetPlayerJumpForce(float value)
    {
        PlayerSettings.jumpForce = value;
        Object.FindObjectOfType<Console>().Debug($"Player jump force changed to {value}", "green");
    }
    public static void ToggleNoclip(bool value)
    {
        Console c = Object.FindObjectOfType<Console>();
        FreeCamController fc = Camera.main.GetComponent<FreeCamController>();

        fc.ToggleNoclip(value);
        c.Debug($"Noclip status changed to {System.Convert.ToBoolean(value)}", "green");
    }
    public static void ToggleDamage(bool value)
    {
        Console c = Object.FindObjectOfType<Console>();

        PlayerSettings.takeDamage = System.Convert.ToBoolean(value);
        c.Debug($"Damage status changed to {System.Convert.ToBoolean(value)}", "green");
    }
    public static void HealPlayer()
    {
        PlayerSettings.currentHealth = PlayerSettings.maxHealth;

        Object.FindObjectOfType<Console>().Debug($"Player got healed", "green");
    }
    public static void SetMaxHealth(float value)
    {
        PlayerSettings.maxHealth = value;
        Object.FindObjectOfType<Console>().Debug($"Player max hp changed to {value}", "green");
    }
    public static void SetSimulationSpeed(float value)
    {
        GameSettings.SimulationSpeed = value;
        Object.FindObjectOfType<Console>().Debug($"Simulation speed changed to {value}", "green");
    }
}
class OtherCommands
{
    public static void StartOuterWilds()
    {
        AudioSource a = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<AudioSource>();

        if (a != null)
        {
            a.clip = Resources.Load<AudioClip>("Sounds/Music/OW/OuterWilds");
            a.Play();

            Object.FindObjectOfType<Console>().Debug("Outer Wilds!", "green");
        }
    }
}

