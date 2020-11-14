using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CaptureSceneManager : MonoBehaviour
{
    [SerializeField] private ARPlaneManager PlaneManager;
    [SerializeField] private Camera ARCamera;
    private GameManager gameManager;
    private Beastie captureViewBeastie;
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


    // TODO: Deselect and reenable all beasties when going back to overworld
    // Add Method in Gamemanager to remove selectedBeastie from List?
    void Start()
    {
        GameObject loader = GameObject.Find("Loader");
        gameManager = loader.GetComponent<GameManager>();
        gameManager.SetAllBeastiesActive(false);

        PlaneManager.planesChanged += PlaceBeastieOnPlane;
    }

    void Update()
    {
        TurnBeastieTowardsCamera();
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
}
