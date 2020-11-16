using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    [SerializeField]
    private int buttonId;
    
    public void HandleButtonClick()
    {
        CaptureSceneManager.Instance.CheckAnswer(buttonId);
    }
}
