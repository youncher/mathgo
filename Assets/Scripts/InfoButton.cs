using UnityEngine;

public class InfoButton : MonoBehaviour
{
    private GameObject overlayObject;
    private AudioSource buttonAudio;
    public AudioClip buttonSound;
    
    void Start()
    {
        buttonAudio = GetComponent<AudioSource>();
        overlayObject = GameObject.Find("TutorialOverlay");
        overlayObject.SetActive(false);
    }
    
    public void HandleClickActivateInfo()
    {
        buttonAudio.PlayOneShot(buttonSound, 1.0f);
        overlayObject.SetActive(true);
    }
}
