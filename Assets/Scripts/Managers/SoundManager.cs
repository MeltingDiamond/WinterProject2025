using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // This script can play, pause, stop audio sources and change the volume of Audio Mixer groups
    
    public AudioMixer masterMixer;
    public AudioMixerGroup masterMixerGroup;
    private static AudioMixer _mixer;
    private static AudioMixerGroup _masterMixerGroup;
    public static SoundManager instance;
    private static AudioSource _audioSource;

    //public AudioClip backgroundMusic;
    public AudioSource backgroundMusic;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        _audioSource = GetComponent<AudioSource>();
        _mixer = masterMixer;
    }

    private void Start()
    {
        //PlayAudioClip(backgroundMusic);
        PlayAudioSource(backgroundMusic);
    }


    public void PlayAudioSource(AudioSource sound)
    {
        if(!sound.isPlaying)
            sound.Play();
    }
    
    public void PlayAudioClip(AudioClip clip, AudioMixerGroup mixerGroup = null)
    {
        _audioSource.outputAudioMixerGroup = mixerGroup ? mixerGroup : _masterMixerGroup;
        _audioSource.PlayOneShot(clip);
    }

    public  void StopAudioSource(AudioSource sound)
    {
        if(sound.isPlaying)
            sound.Stop();
    }
    
    public  void StopAllAudioClips()
    {
        _audioSource.Stop();
    }
    
    public  void PauseAudioSource(AudioSource sound)
    {
        if(sound.isPlaying)
            sound.Pause();
    }
}
