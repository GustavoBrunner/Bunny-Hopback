using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;

    public static AudioController instance { get => _instance; }

    [SerializeField]
    private AudioSource effectSource;

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource stepSource;

    [SerializeField]
    private AudioSource typeSource;

    [SerializeField]
    private AudioSource BeepSource;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }

        stepSource.clip = Resources.Load<AudioClip>("Sounds/Footsteps1");
        
        stepSource.playOnAwake = true;
        musicSource.clip = Resources.Load<AudioClip>("Music/feeling-happy");
        musicSource.loop = true;
        musicSource.playOnAwake = true;


        typeSource.clip = Resources.Load<AudioClip>("Sounds/keyboard-typing");
        typeSource.loop = false;
        typeSource.playOnAwake = false;

        BeepSource.clip = Resources.Load<AudioClip>("Sounds/hospital-monitor");
        BeepSource.playOnAwake = false;
        BeepSource.loop = false;
    }
    public void PlayStep()
    {
        stepSource.enabled = true;
        Debug.Log("Walking soung");
    }
    public void StopStep()
    {
        stepSource.enabled = false;
        Debug.Log("Stop walking soung");
    }

    public void UpdatePosition(Transform tf)
    {
        transform.position = tf.position;
    }
    public void PlayMusic()
    {
        musicSource?.Play();
        stepSource?.Play();
    }
    public void StopAllMusics()
    {
        musicSource?.Stop();
        stepSource?.Stop();
    }
    public void RestartMusic()
    {
        musicSource?.Stop();
        musicSource?.Play();
    }
    private void OnDestroy()
    {
        StopAllMusics();
    }
    public void PlayKeyboard()
    {
        typeSource?.Play();
    }
    public void StopKeyboard()
    {
        typeSource?.Stop();
    }
    public void PlayBeep()
    {
        StartCoroutine(FinalBeep());
    }
    private IEnumerator FinalBeep()
    {
        BeepSource?.Play();
        yield return new WaitForSeconds(4f);
        BeepSource?.Stop();
    }

    public void ChangeMusicPitch(int loop)
    {
        switch(loop)
        {
            case 1:
                musicSource.pitch = 0.74f;
                
                break;
            case 2:
                musicSource.pitch = 0.5f;
                break;
            case 3:
                musicSource.pitch = 1f;
                break;
            default:
                Debug.Break();
                break;
        }
    }
}

