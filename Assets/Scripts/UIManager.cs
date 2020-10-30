using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas signOnCanvas;
    public Canvas newCharacterCanvas;
    public Button confirmButton;
    [SerializeField]
    private GameObject[] characters;
    private int characterType = -1;
    private string characterName = "";

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("No UIManager instance found!");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        TogglePlayerSelectCharacters(false);

        // Couldnt find a way in Unity UI to set the enable/disable - can remove if someoone else finds it
        signOnCanvas.enabled = true;
        newCharacterCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public bool ShouldEnableConfirmButton()
    {
        return characterType != -1 && characterName.Length > 0;
    }

    public void UpdateCharacterTypeSelected(int charType)
    {
        Debug.Log(string.Format("Update Character Type: {0}", charType));
        characterType = charType;
    }

    public void UpdateCharacterName(string charName)
    {
        Debug.Log(string.Format("Update Character Name: {0}", charName));
        characterName = charName;
    }
    
    public bool HandleLogin()
    {
        // TODO: Controls Google Auth flow? Maybe can be moved to another class/manager...
        return true;
    }

    public void HandleRegistration()
    {
        // TODO: Save Character selection and name, transition to map view
        Debug.Log(string.Format("REGISTRATION: Character Name: {0}, Character Type: {1}", characterName, characterType));
    }

    public void TogglePlayerSelectCharacters(bool flag)
    {
        foreach (var character in characters)
        {
            character.SetActive(flag);
        }
    }
}
