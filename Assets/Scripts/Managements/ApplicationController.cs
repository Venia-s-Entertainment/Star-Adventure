using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationController : MonoBehaviour
{
    public void LoadScene(int Index) => UserCommands.LoadScene(Index);
    public void ExitApplication() => UserCommands.ExitApplication();
    public void OpenUrl(string url) => Application.OpenURL(url);
}

