using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    public Button confirmButton;

    // Start is called before the first frame update
    void Start()
    {
        confirmButton.enabled = false;
        confirmButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldBeEnabled = UIManager.Instance.ShouldEnableConfirmButton();
        
        if (shouldBeEnabled && confirmButton.enabled == false)
        {
            Debug.Log("Enabling Confirm Button");
            confirmButton.enabled = true;
            confirmButton.interactable = true;
        }
        else if (!shouldBeEnabled && confirmButton.enabled == true)
        {
            Debug.Log("Disabling Confirm Button");
            confirmButton.enabled = false;
            confirmButton.interactable = false;
        }
    }

    public void HandleRegistration()
    {
        UIManager.Instance.HandleRegistration();
    }
}
