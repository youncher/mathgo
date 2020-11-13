using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beastie : MonoBehaviour
{
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
            gameManager.SelectedBeastie = this.gameObject;
            SceneManager.LoadScene(Constant.CaptureScene);
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(Constant.CaptureScene));
        }
    }
}
