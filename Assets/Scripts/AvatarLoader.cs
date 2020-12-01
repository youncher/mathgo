using UnityEngine;

public class AvatarLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;

    void Start()
    {
        GameObject loader = GameObject.Find("Loader");
        GameManager gameManager = loader.GetComponent<GameManager>();

        int charType = gameManager.charType;

        if (charType < characters.Length)
        {
            Vector3 avatarPosition = new Vector3(transform.position.x + 5f, transform.position.y - 0.25f, transform.position.z - 5.25f);
            GameObject player = Instantiate(characters[charType], avatarPosition, Quaternion.Euler(0, 145, 0));
            player.transform.parent = transform;
        }
    }
}
