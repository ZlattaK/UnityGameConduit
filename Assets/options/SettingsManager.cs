using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject settingsPanel;

    private void Start()
    {
        // на старте панель точно скрыта
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    // ЎестерЄнка
    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    //  рестик
    public void CloseSettings()
    {
        Debug.Log("CLOSE CLICK WORKS");
        settingsPanel.SetActive(false);
    }

    //  нопка выхода
    public void ExitGame()
    {
        Debug.Log("Exit game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}