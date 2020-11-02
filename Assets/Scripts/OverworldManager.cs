using Mapbox.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    [SerializeField] private GameObject character1;
    [SerializeField] private GameObject character2;
    private GameManager gameManager;
    private GameObject player;
    private float MIN_RANGE = -5.0f;
    private float MAX_RANGE = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject loader = GameObject.Find("Loader");
        gameManager = loader.GetComponent<GameManager>();

        LoadCharacter();
        PopulateMapWithBeasties();
    }


    private void LoadCharacter()
    {
        GameObject LBG = GameObject.Find("LocationBasedGame");
        int charType = gameManager.charType;
        if (charType == 0)
        {
            player = Instantiate(character1, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            player = Instantiate(character2, new Vector3(0, 0, 0), Quaternion.identity);
        }

        player.AddComponent(typeof(ImmediatePositionWithLocationProvider));
        player.AddComponent(typeof(RotateWithLocationProvider));
        player.transform.parent = LBG.transform;
        // Slime model tiny - using this to increase size
        player.transform.localScale += new Vector3(3.0f, 3.0f, 3.0f);
    }

    private void PopulateMapWithBeasties()
    {
        int beastiesToSpawn = gameManager.beastiesSpawned;
        for (int i = 0; i < beastiesToSpawn; i++)
        {
            InstantiateBeastie();
        }

        gameManager.beastiesSpawned -= beastiesToSpawn;
    }

    private void InstantiateBeastie()
    {
        Beastie beastie = BeastieFactory.Instance.GenerateBeastie();
        float x = player.transform.position.x + GenerateRange();
        float y = player.transform.position.y;
        float z = player.transform.position.z + GenerateRange();
        Instantiate(beastie, new Vector3(x, y, z), Quaternion.Euler(0.0f, UnityEngine.Random.Range(0.0f, 360.0f), 0.0f));
    }

    private float GenerateRange()
    {
        // Reference: https://www.youtube.com/watch?v=S827tTi8OCo&list=PL86WBCjNmqh4bDQycScKKlP0ETkjo39gG
        float randomNum = UnityEngine.Random.Range(MIN_RANGE, MAX_RANGE);
        int direction = UnityEngine.Random.Range(0, 1) == 0 ? 1 : -1;
        return randomNum * direction;
    }
}
