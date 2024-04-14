using UnityEngine;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform upFloor = null;
    [SerializeField] Transform downFloor = null;

    [Header("Message")]
    [SerializeField] Text msg;

    private Transform player;
    private bool InInteractionZone = false;

    private void Update()
    {
        if (InInteractionZone)
        {
            if (upFloor != null && Input.GetKeyDown(KeyCode.I))
                player.transform.position = TeleportTo(upFloor);
            if (downFloor != null && Input.GetKeyDown(KeyCode.K))
                player.transform.position = TeleportTo(downFloor);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;

            msg.text = $"Press I key to go up\nPress K key to go down";
            InInteractionZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            msg.text = null;
            InInteractionZone = false;
        }
    }
    Vector3 TeleportTo(Transform target)
    {
        return target.position;
    }
}
