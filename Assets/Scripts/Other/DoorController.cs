using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] Animator door;
    [SerializeField] bool isOpened;
    public UnityEvent OnOpen;
    public UnityEvent OnClose;
    public void Interact()
    {
        if (!isOpened)
        {
            OnOpen.Invoke();
            door.SetTrigger("Open");
        }
        else
        {
            OnClose.Invoke();
            door.SetTrigger("Close");
        }

        isOpened = !isOpened;
    }
}
