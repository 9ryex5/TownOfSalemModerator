using UnityEngine;

public class Music : MonoBehaviour
{

    private AudioSource myAudioSource;
    public AudioClip[] nightTown;
    public AudioClip[] dayTown;
    public AudioClip mafia;
    public AudioClip serialKiller;
    public AudioClip werewolf;
    public AudioClip witch;
    public AudioClip winTown;

    private AudioClip nextClip;

    private bool fadingOut;
    private bool fadingIn;
    private float maxVolume;
    [Header("Config")]
    public float fadeDownPerSecond;
    public float fadeInPerSecond;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (fadingOut)
        {
            myAudioSource.volume -= fadeDownPerSecond * Time.deltaTime;
            if (myAudioSource.volume <= 0)
            {
                if (nextClip != null)
                    Play();
                else
                    myAudioSource.Stop();

                fadingOut = false;
            }
        }

        if (fadingIn)
        {
            myAudioSource.volume += fadeInPerSecond * Time.deltaTime;
            if (myAudioSource.volume >= maxVolume)
            {
                myAudioSource.volume = maxVolume;
                fadingIn = false;
            }
        }
    }

    public void PlayNightTown()
    {
        ChangeClip(nightTown[Random.Range(0, nightTown.Length)]);
    }

    public void PlayDayTown()
    {
        ChangeClip(dayTown[Random.Range(0, dayTown.Length)]);
    }

    public void PlayMafia()
    {
        ChangeClip(mafia);
    }

    public void PlaySerialKiller()
    {
        ChangeClip(serialKiller);
    }

    public void PlayWerewolf()
    {
        ChangeClip(werewolf);
    }

    public void PlayWitch()
    {
        ChangeClip(witch);
    }

    public void PlayWinTown()
    {
        ChangeClip(winTown);
    }

    private void ChangeClip(AudioClip _clip)
    {
        nextClip = _clip;

        if (myAudioSource.isPlaying)
        {
            fadingOut = true;
        }
        else
            Play();
    }

    private void Play()
    {
        myAudioSource.clip = nextClip;
        fadingIn = true;
        myAudioSource.Play();
    }

    public void Stop()
    {
        fadingOut = true;
        nextClip = null;
    }

    public void setMaxVolume(float _maxVolume)
    {
        maxVolume = _maxVolume;
    }
}
