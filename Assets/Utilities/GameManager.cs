using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int charType;
    public int beastiesSpawned = 5;
    public string displayName;
    public string gid;
    [SerializeField] private List<Beastie> beasties = new List<Beastie>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddBeastie(Beastie beastie)
    {
        beasties.Add(beastie);
    }

    public void SetAllBeastiesActive(bool flag)
    {
        beasties.ForEach(beastie => beastie.gameObject.SetActive(flag));
    }

    public Beastie GetSelectedBeastie()
    {
        foreach (Beastie beastie in beasties)
        {
            if (beastie.Selected)
            {
                return beastie;
            }
        }

        return null;
    }
}
