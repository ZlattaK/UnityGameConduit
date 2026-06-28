using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    private float musicVolume = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Загружаем громкость, если она была сохранена
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        audioSrc.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        audioSrc.volume = musicVolume;

        // Сохраняем громкость
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (audioSrc.clip == newClip) return;

        audioSrc.clip = newClip;
        audioSrc.Play();
    }
}
