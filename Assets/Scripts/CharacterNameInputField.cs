using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterNameInputField : MonoBehaviour
{
    public TMP_InputField characterNameField;
    public Button confirmButton;
    private int MAX_NAME_LENGTH = 16;

    // Start is called before the first frame update
    void Start()
    {
        characterNameField.characterLimit = MAX_NAME_LENGTH;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleNameChanged()
    {
        UIManager.Instance.UpdateCharacterName(characterNameField.text);
    }
}
