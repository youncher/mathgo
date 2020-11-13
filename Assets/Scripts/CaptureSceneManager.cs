using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CaptureSceneManager : MonoBehaviour
{
    [SerializeField] private ARPlaneManager PlaneManager;
    [SerializeField] private Camera ARCamera;
    private GameManager gameManager;
    private Beastie selectedBeastie;
    private static CaptureSceneManager _instance;
    private bool hasSpawnedBeastie { get; set; } = false;

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
        gameManager.DisableUnselectedBeasties();
        selectedBeastie = gameManager.GetSelectedBeastie();

        PlaneManager.planesChanged += PlaceBeastieOnPlane;
    }

    void Update()
    {
        TurnBeastieTowardsCamera();
    }

    private void PlaceBeastieOnPlane(ARPlanesChangedEventArgs obj)
    {
        ARPlane plane = obj.added[0];

        if (plane != null && !hasSpawnedBeastie)
        {
            selectedBeastie.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            selectedBeastie.transform.position = plane.transform.position;
            hasSpawnedBeastie = true;
        }
    }


    private void TurnBeastieTowardsCamera()
    {
        if (hasSpawnedBeastie)
        {
            selectedBeastie.transform.LookAt(ARCamera.transform);
        }
    }
}
