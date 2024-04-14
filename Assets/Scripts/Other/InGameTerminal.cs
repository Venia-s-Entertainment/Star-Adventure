using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameTerminal : MonoBehaviour, IInteractable
{
    public bool isOpened = false;
    private string password = string.Empty;
    [SerializeField] int passwordLength = 5;
    [SerializeField] GameObject hud;
    [SerializeField] Dialogue passwordNote;
    [SerializeField] PlayerController player;
    [SerializeField] InputField terminal;
    [SerializeField] Text placeholder;
    [SerializeField] Text pswTextReminder;

    [Header("On terminal open text")]
    [SerializeField] string openText = "Hello World";

    [Header("On wrong password text")]
    [SerializeField] string onWrong = "false";
    [SerializeField] UnityEvent OnWrongPasswordEvent;

    [Header("On right password text")]
    [SerializeField] string onRight = "true";
    [SerializeField] UnityEvent OnRightPasswordEvent;

    void Start()
    {
        GeneratePassword();
    }
    public void Interact()
    {
        OpenTerminal();
    }

    private void OpenTerminal()
    {
        StartCoroutine(AwaitForTap());

        isOpened = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        terminal.gameObject.SetActive(true);
        hud.SetActive(false);
        placeholder.text = openText;

        player.Disable();
        player.UpdateAnimation();
    }
    private void CloseTerminal()
    {
        StopAllCoroutines();

        isOpened = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        terminal.gameObject.SetActive(false);
        hud.SetActive(true);

        terminal.text = string.Empty;

        player.Enable();
    }
    private void GeneratePassword()
    {
        System.Random rnd = new();

        string[] symbols = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

        for (int i = 0; i < passwordLength; i++)
        {
            password += symbols[rnd.Next(0, symbols.Length)];
        }

        passwordNote.lines[0] = $"Security password: {password}";
        pswTextReminder.text = $"Password: {password}";
    }
    public void Submit()
    {

        if (terminal.text == password)
        {
            placeholder.text = $"<color=cyan>{onRight}\n";
            OnRightPasswordEvent.Invoke();
        }
        else
        {
            placeholder.text = $"<color=red>{onWrong}\n";
            OnWrongPasswordEvent.Invoke();
        }

        terminal.text = string.Empty;
        placeholder.text += $"Press 'Space' key to close terminal...</color>";
    }
    IEnumerator AwaitForTap()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return new WaitForSeconds(0);
        }

        CloseTerminal();
    }
}
