using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    [SerializeField] AudioSource ambience;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tree"))
        {
            other.gameObject.layer = 7;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (!ambience.isPlaying)
            {
                ambience.Play();
            }

            ambience.UnPause();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ambience.Pause();
        }
        if (other.gameObject.CompareTag("Tree"))
        {
            other.gameObject.layer = 6;
        }
    }
}
