using UnityEngine;

[CreateAssetMenu(fileName = "SoundsContainer", menuName = "Venia/SoundsContainer")]
public class SoundsContainer : ScriptableObject
{
    public AudioClip[] clips;

    public AudioClip GetRandomClip()
    {
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        return clip;
    }
}
