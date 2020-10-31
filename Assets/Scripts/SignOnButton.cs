using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SignOnButton : MonoBehaviour
{
    [SerializeField] private GameObject loader;


    public Canvas SignOnCanvas;
    public Canvas NewCharacterCanvas;

    private Text debugSignOnStatusText;
    private string webClientId = Keys.GetWebClientId();
    private GoogleSignInConfiguration configuration;
    private UserInfo userInfo;
    
    void Awake() {
      configuration = new GoogleSignInConfiguration {
            WebClientId = webClientId,
            RequestIdToken = true
      };
    }

    void Start()
    {
      debugSignOnStatusText = GameObject.Find("DebugSignOnStatusText").GetComponent<Text>();
    }

    public void OnSignIn() {
      GoogleSignIn.Configuration = configuration;
      GoogleSignIn.Configuration.UseGameSignIn = false;
      GoogleSignIn.Configuration.RequestIdToken = true;
      AddStatusText("Calling SignIn");

      GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
        OnAuthenticationFinished);
    }
    
    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task) {
      if (task.IsFaulted) {
        using (IEnumerator<System.Exception> enumerator =
                task.Exception.InnerExceptions.GetEnumerator()) {
          if (enumerator.MoveNext()) {
            GoogleSignIn.SignInException error =
                    (GoogleSignIn.SignInException)enumerator.Current;
            AddStatusText("Got Error: " + error.Status + " " + error.Message);
          } else {
            AddStatusText("Got Unexpected Exception?!?" + task.Exception);
          }
        }
      } else if(task.IsCanceled) {
        AddStatusText("Canceled");
      } else  { // Google auth success
        userInfo = new UserInfo {gid = task.Result.UserId, displayName = task.Result.DisplayName};

        // Send user info to check if existing mathgo user
        StartCoroutine(CheckUserExists());
      }
    }
    
    // Check if Google user is an existing Math Go user
    IEnumerator CheckUserExists()
    {
      string jsonToSend = JsonUtility.ToJson(userInfo);
      
      UnityWebRequest request = UnityWebRequest.Post("http://mathgo-46d6d.wl.r.appspot.com/user/validation", jsonToSend);
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
        Debug.Log("POST successful!");
        UserInfo responseUserInfo = JsonUtility.FromJson<UserInfo>(response);
        userInfo.avatar = responseUserInfo.avatar;
        userInfo.existingUser = responseUserInfo.existingUser;
        
        if (userInfo.existingUser)
        {
          //AddStatusText("Existing user: " + userInfo.displayName);

          // Loading up user data into game manager, and loading new scene
          loader.GetComponent<GameManager>().charType = userInfo.avatar;
          SceneManager.LoadScene(1);

            }
            else
        {
          AddStatusText("New Math Go user!");
          ChangeToNewCharacterScreen();
        }
      }
    }
    
    public void ChangeToNewCharacterScreen()
    {
      SignOnCanvas.enabled = false;
      NewCharacterCanvas.enabled = true;
    }
    
    private List<string> messages = new List<string>();
    void AddStatusText(string text) {
      if (messages.Count == 5) {
        messages.RemoveAt(0);
      }
      messages.Add(text);
      string txt = "";
      foreach (string s in messages) {
        txt += "\n" + s;
      }
      debugSignOnStatusText.text = txt;
    }
}
