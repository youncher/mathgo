using UnityEngine;

public class BackButton : MonoBehaviour
{
    private CaptureSceneManager captureSceneManager;
    private AudioSource buttonAudio;
    public AudioClip buttonSound;
    
    void Start()
    {
        captureSceneManager = GameObject.Find("CaptureSceneManager").GetComponent<CaptureSceneManager>();
        buttonAudio = GetComponent<AudioSource>();
    }

    public void HandleClickCaptureScene()
    {
        buttonAudio.PlayOneShot(buttonSound, 1.0f);
        captureSceneManager.GoToMapScene();
    }
}
