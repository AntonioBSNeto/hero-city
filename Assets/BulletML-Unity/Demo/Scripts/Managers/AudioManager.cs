using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource; // Fonte de áudio para música
    public AudioSource sfxSource; // Fonte de áudio para efeitos sonoros
    public AudioClip demoFightMusic; // Trilha sonora para Demo_Fight

    void Awake()
    {
        // Garantir que apenas uma instância do AudioManager exista
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}