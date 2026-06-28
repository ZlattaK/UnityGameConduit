using UnityEngine;

public class EntranceTrigger : MonoBehaviour
{
    public LastCamera lastCamera;

    [Header("Тег кулона")]
    public string requiredTag = "Pendant";

    [Header("Музыка следующей локации")]
    public AudioClip nextLocationMusic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(requiredTag))
        {
            Debug.Log("Кулон активировал переход");

            MusicManager music = FindObjectOfType<MusicManager>();
            if (music != null && nextLocationMusic != null)
            {
                music.PlayMusic(nextLocationMusic);
            }

            lastCamera.StartTransition();
        }
    }
}