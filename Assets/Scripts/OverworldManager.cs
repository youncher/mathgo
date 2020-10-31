using Mapbox.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    [SerializeField] private GameObject character1;
    [SerializeField] private GameObject character2;
    // Start is called before the first frame update
    void Start()
    {
        GameObject LBG = GameObject.Find("LocationBasedGame");
        GameObject loader = GameObject.Find("Loader");
        int charType = loader.GetComponent<GameManager>().charType;
        if (charType == 0)
        {
            GameObject character = Instantiate(character1, new Vector3(0, 0, 0), Quaternion.identity);
            character.AddComponent(typeof(ImmediatePositionWithLocationProvider));
            character.AddComponent(typeof(RotateWithLocationProvider));
            character.transform.parent = LBG.transform;
        }
        else
        {
            GameObject character = Instantiate(character2, new Vector3(0, 0, 0), Quaternion.identity);
            character.AddComponent(typeof(ImmediatePositionWithLocationProvider));
            character.AddComponent(typeof(RotateWithLocationProvider));
            character.transform.parent = LBG.transform;


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
