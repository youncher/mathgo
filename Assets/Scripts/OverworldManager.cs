using Mapbox.Examples;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    [SerializeField] private GameObject character1;
    [SerializeField] private GameObject character2;
    [SerializeField] private Camera mainCamera;
    private GameManager gameManager;
    private GameObject player;
    private float MIN_RANGE = 5.0f;
    private float MAX_RANGE = 50.0f;
    
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
        // set a tag for the user's player character
        player.tag = "UserCharacter";
        // Slime model tiny - using this to increase size
        player.transform.localScale += new Vector3(3.0f, 3.0f, 3.0f);
        mainCamera.transform.parent = player.transform;

        TMPro.TMP_Text avatarNameField = GameObject.Find("AvatarName").GetComponent<TMPro.TMP_Text>();
        string displayName = gameManager.displayName;

        if (displayName != null)
        {
            avatarNameField.SetText(gameManager.displayName);
        }
    }

    private void PopulateMapWithBeasties()
    {
        if (gameManager.ExistingBeastiesCount == 0)
        {
            InstantiateNewBeasties();
        }
    }

    private void InstantiateNewBeasties()
    {
        int beastiesToSpawn = gameManager.DefaultBeastiesSpawnCount;
        for (int i = 0; i < beastiesToSpawn; i++)
        {
            Beastie beastie = BeastieFactory.Instance.GenerateBeastie();
            float x = player.transform.position.x + GenerateRange();
            float y = player.transform.position.y;
            float z = player.transform.position.z + GenerateRange();
            gameManager.AddBeastie(Instantiate(beastie, new Vector3(x, y, z), Quaternion.Euler(0.0f, UnityEngine.Random.Range(0.0f, 360.0f), 0.0f)));
        }
    }

    private float GenerateRange()
    {
        // Reference: https://www.youtube.com/watch?v=S827tTi8OCo&list=PL86WBCjNmqh4bDQycScKKlP0ETkjo39gG
        float randomNum = Random.Range(MIN_RANGE, MAX_RANGE);
        int direction = Random.Range(0, 2) == 0 ? 1 : -1;
        return randomNum * direction;
    }
}