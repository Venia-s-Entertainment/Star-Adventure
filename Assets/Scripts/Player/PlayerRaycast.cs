using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] Text raycastText;

    bool isHitting;
    GameObject interactableObject;
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 10 && enabled)
        {
            raycastText.text = "Press E key to interact";

            isHitting = true;
            interactableObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Clear();
        }
    }
    private void Update()
    {
        if (isHitting)
        {
            IInteractable interactable = interactableObject.GetComponent<IInteractable>();

            if (Input.GetKeyDown(KeyCode.E))
                interactable.Interact();
        }
    }
    public void Clear()
    {
        raycastText.text = string.Empty;
        isHitting = false;
        interactableObject = null;
    }
}
