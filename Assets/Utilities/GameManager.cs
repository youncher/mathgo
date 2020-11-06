﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int charType;
    public int beastiesSpawned = 5;
    public string displayName;
    public string gid;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
