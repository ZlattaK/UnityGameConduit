using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordSystem : MonoBehaviour
{
    public TMP_InputField inputField; 
    public string correctPassword = "4729";

    public GameObject lockedObject; 

    public void CheckPassword()
    {
        if (inputField.text == correctPassword)
        {
            Debug.Log("Пароль верный");

            if (lockedObject != null)
                lockedObject.SetActive(true); 

            gameObject.SetActive(false); 
        }
        else
        {
            Debug.Log("Неверный пароль");
        }
    }
}