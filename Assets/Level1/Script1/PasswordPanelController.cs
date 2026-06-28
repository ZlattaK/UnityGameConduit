using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PasswordPanelController : MonoBehaviour
{
    [Header("Main Panel")]
    public GameObject passwordPanel;      

    [Header("Computer Screens")]
    public GameObject computerImage1;     
    public GameObject computerImage2;     

    [Header("UI Elements")]
    public TMP_InputField inputField;
    public GameObject errorText;
    public Button exitButton;

    [Header("Password Settings")]
    public string correctPassword = "1234";

    [Header("Optional Dialogue Panel")]
    public GameObject dialogPanel;       

    [Header("Canvas")]
    public GameObject canvas;

    private bool isUnlocked = false;

    
    public event System.Action OnComputerClosed;

    void Awake()
    {
        if (passwordPanel == null)
            passwordPanel = this.gameObject;

        if (exitButton != null)
            exitButton.onClick.AddListener(ClosePanel);
    }

    public void OpenPanel()
    {
        if (canvas != null)
            canvas.SetActive(true);

        passwordPanel.SetActive(true);

        computerImage1.SetActive(true);
        computerImage2.SetActive(false);

        inputField.text = "";
        errorText.SetActive(false);

        isUnlocked = false; 

        inputField.Select();
        inputField.ActivateInputField();
    }

    public void CheckPassword()
    {
        if (inputField.text == correctPassword)
        {
            computerImage1.SetActive(false);
            computerImage2.SetActive(true);

            errorText.SetActive(false);

            isUnlocked = true; 
        }
        else
        {
            errorText.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        passwordPanel.SetActive(false);

        if (canvas != null)
            canvas.SetActive(false);


        if (isUnlocked)
        {
            OnComputerClosed?.Invoke();

            if (dialogPanel != null)
                dialogPanel.SetActive(true);
        }

        computerImage1.SetActive(true);
        computerImage2.SetActive(false);

        inputField.text = "";
        errorText.SetActive(false);

        isUnlocked = false; 
    }
}
