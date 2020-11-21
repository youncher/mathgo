using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beastie : MonoBehaviour
{
    public bool Selected { get; set; } = false;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    private void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name == Constant.OverworldMap)
        {
            GameObject loader = GameObject.Find("Loader");
            GameManager gameManager = loader.GetComponent<GameManager>();
            gameManager.SetSelectedBeastie(this);

            SceneManager.LoadScene(Constant.CaptureScene);
        }
    }
}
