using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour, IInteractable
{
    public string Name;
    public string[] lines;
    public float lineSpeed = 0.5f;

    [Header("Eviroment")]
    [SerializeField] bool destroyAtTheEnd;
    [SerializeField] GameObject dialogueWindow;
    [SerializeField] Text dialogueText;
    [SerializeField] UnityEvent afterDialogueEvent;

    [Header("Other")]
    [SerializeField] PlayerController pl;

    int currentLine;
    public void Interact()
    {
        StartDialogue();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Settings.GameSettings.isPaused)
        {
            NextLine();
        }
    }
    private void NextLine()
    {
        StopAllCoroutines();

        dialogueText.text = string.Empty;

        currentLine++;
        if (currentLine > lines.Length - 1)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(DrawLines());
    }
    private void StartDialogue()
    {
        enabled = true;
        dialogueWindow.SetActive(true);

        pl.Disable();
        pl.UpdateAnimation();

        currentLine = 0;
        StartCoroutine(DrawLines());
    }
    private void EndDialogue()
    {
        pl.Enable();

        dialogueWindow.SetActive(false);
        enabled = false;

        dialogueText.text = string.Empty;

        if (destroyAtTheEnd)
        {
            gameObject.layer = 0;
        }

        afterDialogueEvent.Invoke();
    }
    IEnumerator DrawLines()
    {
        dialogueText.text = $"<color=orange>{Name}:</color>\t";

        foreach (char item in lines[currentLine].ToCharArray())
        {
            dialogueText.text += item;

            yield return new WaitForSeconds(lineSpeed);
        }
    }
}
