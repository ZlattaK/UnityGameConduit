using UnityEngine;

public class OpenPasswordPanel : MonoBehaviour
{
    public PasswordPanelController controller;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            controller.OpenPanel();
        }
    }
}