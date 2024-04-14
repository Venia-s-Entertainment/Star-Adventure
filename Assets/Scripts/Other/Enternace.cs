using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enternace : MonoBehaviour
{
    [SerializeField] UnityEvent OnPlayerEnter;
    [SerializeField] UnityEvent OnPlayerExit;

    [Header("Message")]
    [SerializeField] Text MessageBox;
    [SerializeField] string msgAfterEnter;
    [SerializeField] string msgAfterExit;

    public bool InInteractionZone;
    // Update is called once per frame
    private void Update()
    {
        if (InInteractionZone)
        {
            if (Input.GetKeyDown(KeyCode.F) && !Settings.GameSettings.isPaused)
                OnPlayerEnter.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MessageBox.text = msgAfterEnter;
            InInteractionZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MessageBox.text = msgAfterExit;
            OnPlayerExit.Invoke();

            InInteractionZone = false;
        }
    }
}
