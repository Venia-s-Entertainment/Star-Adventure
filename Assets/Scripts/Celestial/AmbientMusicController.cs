using UnityEngine;
using Settings;
using System.Collections;

public class AmbientMusicController : MonoBehaviour
{
    [SerializeField] SoundsContainer ambientMusic;
    public float targetVolume;
    private AudioSource musicBox;
    public AudioClip newClip;

    [Header("Debug")]
    [SerializeField] Console con;

    private void Start()
    {
        musicBox = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (musicBox.volume == 0)
        {
            targetVolume = 1;
            musicBox.clip = newClip;
        }

        SetVolume(targetVolume);
    }
    private void SetVolume(float targetVolume)
    {
        if (!musicBox.isPlaying) 
            musicBox.Play();          

        float volume = 0.1f * Time.deltaTime;

        if (musicBox.volume > targetVolume)
        {
            musicBox.volume = Mathf.Clamp(musicBox.volume, targetVolume, 1);
            musicBox.volume -= volume;
        }

        if (musicBox.volume < targetVolume)
        {
            musicBox.volume = Mathf.Clamp(musicBox.volume, 0, 1);
            musicBox.volume += volume;
        }
    }
    public void SetNewClip(int clipNumber)
    {
        try
        {
            newClip = ambientMusic.clips[clipNumber];
            targetVolume = 0;
        }
        catch (System.Exception)
        {
            con.Debug($"There's no audio clip with id '{clipNumber}'", "red");
        }              
    }
}
