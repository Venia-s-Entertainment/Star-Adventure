using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] UnityEvent OnKill;
    public void Kill()
    {
        player.cam.transform.parent = null;
        player.cam.gameObject.AddComponent<BoxCollider>();
        player.cam.gameObject.AddComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;
        player.gameObject.SetActive(false);

        OnKill.Invoke();
    }
}
