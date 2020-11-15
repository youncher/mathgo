using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public int charType;
    private int defaultBeastiesSpawnCount = 5;
    public string displayName;
    public string gid;
    private List<Beastie> beasties = new List<Beastie>();
    private int selectedBeastieIndex { get; set; } = -1;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int DefaultBeastiesSpawnCount
    {
        get => defaultBeastiesSpawnCount;
    }

    public int ExistingBeastiesCount
    {
        get => beasties.Count;
    }

    public void AddBeastie(Beastie beastie)
    {
        beasties.Add(beastie);
    }

    public Beastie GetBeastie(int index)
    {
        return beasties[index];

    }
    
    public void SetAllBeastiesActive(bool flag)
    {
        beasties.ForEach(beastie => beastie.gameObject.SetActive(flag));
    }

    public Beastie GetSelectedBeastie()
    {
        for (int i = 0; i < beasties.Count; i++ )
        {
            if (beasties[i].Selected)
            {
                selectedBeastieIndex = i;
                return beasties[i];
            }
        }

        return null;
    }

    public void RemoveSelectedBeastie()
    {
        if (selectedBeastieIndex != -1)
        {
           beasties.RemoveAt(selectedBeastieIndex);
           selectedBeastieIndex = -1;
        }
    }
}
