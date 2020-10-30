using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastieFactory : MonoBehaviour
{
    [SerializeField]
    private Beastie[] beasties;

    private static BeastieFactory _instance;
    public static BeastieFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("No BeastieFactory instance found!");
            }

            return _instance;
        }
    }


    private void Awake()
    {
        _instance = this;
    }

    public Beastie GenerateBeastie()
    {
        int index = Random.Range(0, beasties.Length);
        return beasties[index];
    }
}
