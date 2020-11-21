using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public int charType;
    private int defaultBeastiesSpawnCount = 5;
    public string displayName;
    public string gid;
    private List<Beastie> beasties = new List<Beastie>();
    private Beastie selectedBeastie;
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
        return selectedBeastie;
    }

    public void SetSelectedBeastie(Beastie beastie)
    {
        selectedBeastie = beastie;
    }

    public void RemoveSelectedBeastie()
    {
        if (selectedBeastie != null)
        {
            beasties.Remove(selectedBeastie);
        }
    }
}
