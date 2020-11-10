using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: Currently a placeholder to ensure only Beasties are created from Factory
public class Beastie : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene(Constant.CaptureScene);
    }
}
