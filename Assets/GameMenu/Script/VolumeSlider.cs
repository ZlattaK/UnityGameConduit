using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;        
    [SerializeField] private MusicManager musicManager;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public void ChangeVolume()
    {
        if (musicManager == null || slider == null)
            return;

        musicManager.SetVolume(slider.value);
    }

}
