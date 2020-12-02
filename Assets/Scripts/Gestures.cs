using Mapbox.Examples;
using UnityEngine;

public class Gestures : MonoBehaviour
{
    private GameObject target;
    Camera cam;
    [SerializeField, Range(0, 3)] private float rotationRate = 0.4f;
    [SerializeField] GameObject compassImage;
    [SerializeField] GameObject textWarning;
    private bool firstWarning = true;
    private Vector3 camRotation;
    private Vector3 camPosition;
    public float perspectiveZoomSpeed = .5f;


    public void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (target == null)
        {
            target = gameObject.transform.parent.gameObject;

        }
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("Touching at: " + touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch phase began at: " + touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (firstWarning)
                {
                    textWarning.gameObject.SetActive(true);
                    firstWarning = false;
                    Destroy(textWarning.gameObject, 5.0f);
                }
                // Saving the camera transform when player uses gesture while application is using deice orientation
                if(compassImage.GetComponent<Compass>().deviceOrientationActive == true)
                {
                    camRotation = cam.transform.eulerAngles;
                    camPosition = cam.transform.position;
                }
                // Disabling the rotate with locatin provider script in the compass and the player character
                compassImage.GetComponent<RotateWithLocationProvider>().enabled = false;
                target.GetComponent<RotateWithLocationProvider>().enabled = false;
                compassImage.GetComponent<Compass>().deviceOrientationActive = false;


                Debug.Log("Touch phase Moved");
                // Rotate the map as well as the compass to the left, or to the right depending on whether the touch is detected above or below the player character
                if (touch.position.y > cam.WorldToScreenPoint(target.transform.position).y)
                {
                    transform.RotateAround(target.transform.position, Vector3.up, -touch.deltaPosition.x * rotationRate);
                    compassImage.transform.Rotate(0, 0, -touch.deltaPosition.x * rotationRate, Space.Self);

                }
                else if (touch.position.y < cam.WorldToScreenPoint(target.transform.position).y)
                {
                    transform.RotateAround(target.transform.position, Vector3.up, touch.deltaPosition.x * rotationRate);
                    compassImage.transform.Rotate(0, 0, touch.deltaPosition.x * rotationRate, Space.Self);

                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch phase Ended");
            }
        }
        // pinch to zoom, https://www.youtube.com/watch?v=srcIPtyWlMs
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            
            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            
            float difference = prevMagnitude - currentMagnitude;

            zoom(difference);
        }
    }

    // Getters for the camera position and rotation
    public Vector3 camRot
    {
        get => camRotation;
    }

    public Vector3 camPos
    {
        get => camPosition;
    }

    void zoom(float increment)
    {
        Camera.main.fieldOfView += increment * perspectiveZoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView , 35f, 70f);
    }
}