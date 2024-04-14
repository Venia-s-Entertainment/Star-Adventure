using UnityEngine;
using UnityEngine.UI;
using Settings;

public class Console : MonoBehaviour
{
    public Text consoleText;
    [SerializeField] InputField input;
    [SerializeField] byte tresholdLine;

    private byte currentLine;
    public void Debug(object message, string color)
    {
        if (currentLine >= tresholdLine)
        {
            consoleText.text = null;
            currentLine = 0;
        }
        consoleText.text += $"\n<color={color}>[{System.DateTime.Now}]\t{message}</color>";
        currentLine++;
    }
    public void ExecuteCommand()
    {
        string[] cmdParts = input.text.Split(' ');
        string firstPart = cmdParts[0];
        string secondPart = cmdParts.Length > 1 ? cmdParts[1] : null;

        for (int i = 0; i < UserCommands.commands.Length; i++)
        {
            if (firstPart == UserCommands.commands[i])
            {
                CommandHandler.Invoke(i, secondPart);
                return;
            }
        }
        for (int i = 0; i < AdminCommands.commands.Length; i++)
        {
            if (firstPart == AdminCommands.commands[i])
            {
                if (!GameSettings.adminIsAavailable)
                {
                    Debug($"You don't have permission for this command: '{firstPart}'!", "red");
                    return;
                }

                CommandHandler.Invoke(i, secondPart, true);
                return;
            }
        }
        if (firstPart == "ow" && CommandHandler.currentScene == 0)
        {
            OtherCommands.StartOuterWilds();
            return;
        }

        Debug($"Invalid command: {firstPart}, use 'help' to show all available commands", "red");
    }
}
