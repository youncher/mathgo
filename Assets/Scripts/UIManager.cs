using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loader;

    GameObject dialog = null;

    public Canvas signOnCanvas;
    public Canvas newCharacterCanvas;
    public Button confirmButton;
    [SerializeField]
    private GameObject[] characters;
    private int characterType = -1;
    private string characterName = "";

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("No UIManager instance found!");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        TogglePlayerSelectCharacters(false);

        // Couldnt find a way in Unity UI to set the enable/disable - can remove if someoone else finds it
        signOnCanvas.enabled = true;
        newCharacterCanvas.enabled = false;


        // Request permission for location services
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            dialog = new GameObject();
        }
        #endif
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public bool ShouldEnableConfirmButton()
    {
        return characterType != -1 && characterName.Length > 0;
    }

    public void UpdateCharacterTypeSelected(int charType)
    {
        Debug.Log(string.Format("Update Character Type: {0}", charType));
        characterType = charType;
    }

    public void UpdateCharacterName(string charName)
    {
        Debug.Log(string.Format("Update Character Name: {0}", charName));
        characterName = charName;
    }
    
    public bool HandleLogin()
    {
        // TODO: Controls Google Auth flow? Maybe can be moved to another class/manager...
        return true;
    }

    public void HandleRegistration()
    {
        // Preparing UserInfo object to send to server registration route
        UserInfo userInfo = new UserInfo
        {
            gid = loader.GetComponent<GameManager>().gid,
            displayName = characterName,
            avatar = characterType,
            addSuccessful = false
        };

        // Storing name and char type into game manager
        Debug.Log(string.Format("REGISTRATION: Character Name: {0}, Character Type: {1}", characterName, characterType));
        loader.GetComponent<GameManager>().displayName = characterName;
        loader.GetComponent<GameManager>().charType = characterType;
        // Starting Coroutine to make network call for registering user
        StartCoroutine(AddUser(userInfo));
    }

    IEnumerator AddUser(UserInfo userInfo)
    {
        string jsonToSend = JsonUtility.ToJson(userInfo);
        UnityWebRequest request = UnityWebRequest.Post("https://test-or-mathgo.wn.r.appspot.com/user/registration", jsonToSend);
        //UnityWebRequest request = UnityWebRequest.Post("http://mathgo-46d6d.wl.r.appspot.com/user/registration", jsonToSend);


        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonToSend));
        yield return request.SendWebRequest();
        var response = request.downloadHandler.text;

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            UserInfo responseUserInfo = JsonUtility.FromJson<UserInfo>(response);

            if (!responseUserInfo.addSuccessful)
            {
                // TODO Handle unsuccessful login
                Debug.Log("Unable to Add New User");
            }
            else
            {
                Debug.Log("POST successful! User Added");
                SceneManager.LoadScene(1);
            }
        }

    }


    public void TogglePlayerSelectCharacters(bool flag)
    {
        foreach (var character in characters)
        {
            character.SetActive(flag);
        }
    }
}
