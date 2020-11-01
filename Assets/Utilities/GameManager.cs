using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int charType;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
