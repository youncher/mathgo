using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CaptureSceneManager : MonoBehaviour
{
    [SerializeField] private Canvas questionAnswerCanvas;
    [SerializeField] private Canvas warning;

    private bool warningActive = true;


    [SerializeField] private ARPlaneManager PlaneManager;
    [SerializeField] private Camera ARCamera;
    private GameManager gameManager;
    private Beastie captureViewBeastie;
    
    private float sceneTransitionDelay = 1.5f;
    
    private MathProblem[] mathProblems;
    private MathProblem mathProblem;
    
    private Button buttonA;
    private Button buttonB;
    private Button buttonC;
    private Button buttonD;
    
    private TextMeshProUGUI questionText;
    private TextMeshProUGUI answerAText;
    private TextMeshProUGUI answerBText;
    private TextMeshProUGUI answerCText;
    private TextMeshProUGUI answerDText;

    private TextMeshProUGUI correctStatusFront;
    private TextMeshProUGUI correctStatusShadow;

    private bool answerProvided = false;

    private static CaptureSceneManager _instance;

    public static CaptureSceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("No CaptureSceneManager instance found!");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    
    void Start()
    {
        GameObject loader = GameObject.Find("Loader");
        gameManager = loader.GetComponent<GameManager>();
        gameManager.SetAllBeastiesActive(false);

        PlaneManager.planesChanged += PlaceBeastieOnPlane;

        mathProblem = new MathProblem();

        buttonA = GameObject.Find("ButtonA").GetComponent<Button>();
        buttonB = GameObject.Find("ButtonB").GetComponent<Button>();
        buttonC = GameObject.Find("ButtonC").GetComponent<Button>();
        buttonD = GameObject.Find("ButtonD").GetComponent<Button>();
        
        questionText = GameObject.Find("CaptureQuestionText").GetComponent<TextMeshProUGUI>();
        answerAText = GameObject.Find("CaptureAnswerAText").GetComponent<TextMeshProUGUI>();
        answerBText = GameObject.Find("CaptureAnswerBText").GetComponent<TextMeshProUGUI>();
        answerCText = GameObject.Find("CaptureAnswerCText").GetComponent<TextMeshProUGUI>();
        answerDText = GameObject.Find("CaptureAnswerDText").GetComponent<TextMeshProUGUI>();

        correctStatusFront = GameObject.Find("CorrectStatusTextFront").GetComponent<TextMeshProUGUI>();
        correctStatusShadow = GameObject.Find("CorrectStatusTextShadow").GetComponent<TextMeshProUGUI>();

        // after getting button references, we deactivate the canvas
        questionAnswerCanvas.gameObject.SetActive(false);

        StartCoroutine(GetMathProblems());

    }

    void Update()
    {
        if (captureViewBeastie != null && warningActive)
        {
            warningActive = false;
            Destroy(warning.gameObject);
        }
        TurnBeastieTowardsCamera();
        
        if (answerProvided == true)
        {
            sceneTransitionDelay -= Time.deltaTime;
            if (sceneTransitionDelay <= 0)
            {
                Destroy(captureViewBeastie.gameObject);
                gameManager.SetAllBeastiesActive(true);
                SceneManager.LoadScene(Constant.OverworldMap);
            }
        }
        if (captureViewBeastie != null && (beastieVisible(captureViewBeastie)))
        {
            questionAnswerCanvas.gameObject.SetActive(true);
        }
        else
        {
            questionAnswerCanvas.gameObject.SetActive(false);
        }

    }

    private void PlaceBeastieOnPlane(ARPlanesChangedEventArgs obj)
    {
        if (captureViewBeastie == null && obj.added.Count > 0 && obj.added[0] != null)
        {
            ARPlane plane = obj.added[0];
            Beastie selectedBeastie = gameManager.GetSelectedBeastie();

            captureViewBeastie = Instantiate(selectedBeastie, plane.transform.position, Quaternion.identity);
            Debug.Log(selectedBeastie.Equals(captureViewBeastie));

            captureViewBeastie.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            captureViewBeastie.transform.position = plane.transform.position;
            captureViewBeastie.gameObject.SetActive(true);
        }
    }

    private void TurnBeastieTowardsCamera()
    {
        if (captureViewBeastie != null)
        {
            captureViewBeastie.transform.LookAt(ARCamera.transform);
        }
    }


    // reference: https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html
    private bool beastieVisible(Beastie beast)
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(ARCamera);
        if (GeometryUtility.TestPlanesAABB(planes, beast.GetComponent<Collider>().bounds))
            return true;
        else
            return false;
    }





    IEnumerator GetMathProblems()
    {
        UnityWebRequest request = UnityWebRequest.Get(Constant.MathProblemsUri);
        request.SetRequestHeader ("Accept", "application/json");
        yield return request.SendWebRequest();
        
        var response = request.downloadHandler.text;
        
        if (request.isNetworkError || request.isHttpError)
        {
            // TODO: Handle error
            Debug.Log(request.error);
        }
        else
        {
            mathProblems = JsonHelper.getJsonArray<MathProblem>(response);
            SelectMathProblem();
            DisplayMathProblem();
        }
    }
    
    private void SelectMathProblem()
    {
        var selectedIndex = Random.Range(0, mathProblems.Length);
        mathProblem.Question = mathProblems[selectedIndex].Question;
        mathProblem.PotentialAnswers = mathProblems[selectedIndex].PotentialAnswers;
        mathProblem.CorrectIndex = mathProblems[selectedIndex].CorrectIndex;
    }
    
    private void DisplayMathProblem()
    {
        questionText.SetText(mathProblem.Question);
        answerAText.SetText(mathProblem.PotentialAnswers[0]);
        answerBText.SetText(mathProblem.PotentialAnswers[1]);
        answerCText.SetText(mathProblem.PotentialAnswers[2]);
        answerDText.SetText(mathProblem.PotentialAnswers[3]);
    }

    public void CheckAnswer(int buttonId)
    {
        answerProvided = true;
        if (buttonId == mathProblem.CorrectIndex)
        {
            correctStatusFront.color = new Color32(33, 255, 0, 255);
            correctStatusShadow.color = new Color32(60, 70, 59, 255);
            SetCorrectStatus("Correct");
            gameManager.RemoveSelectedBeastie();
        }
        else
        {
            correctStatusFront.color = new Color32(240, 8, 8, 255);
            correctStatusShadow.color = new Color32(100, 10, 10, 255);
            SetCorrectStatus("Incorrect");
            gameManager.SetSelectedBeastie(null);
        }
        DisableAnswerButtons();
    }

    private void SetCorrectStatus(string status)
    {
        correctStatusFront.SetText(status);
        correctStatusShadow.SetText(status);
    }
    
    private void DisableAnswerButtons()
    {
        buttonA.enabled = false;
        buttonB.enabled = false;
        buttonC.enabled = false;
        buttonD.enabled = false;
    }
}
