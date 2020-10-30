using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignOnButton : MonoBehaviour
{
    public Canvas SignOnCanvas;
    public Canvas NewCharacterCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleSignOn()
    {
        // TODO: Add google auth - if character already exists in database, goto map scene, else, new character screen displayed
        ChangeToNewCharacterScreen();
    }

    public void ChangeToNewCharacterScreen()
    {
        UIManager.Instance.TogglePlayerSelectCharacters(true);

        SignOnCanvas.enabled = false;
        NewCharacterCanvas.enabled = true;
    }
}
