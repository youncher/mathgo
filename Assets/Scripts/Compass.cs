using Mapbox.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    private bool DeviceOrientationActive = true;
    private GameObject player;
    public void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("UserCharacter");
        }
    }

    public bool deviceOrientationActive
    {
        get => DeviceOrientationActive;
        set => DeviceOrientationActive = value;
    }

    public void TurnOnDeviceOrientation()
    {
        if (DeviceOrientationActive == false)
        {
            DeviceOrientationActive = true;
            Camera.main.transform.position = Camera.main.GetComponent<Gestures>().camPos;
            Camera.main.transform.eulerAngles = Camera.main.GetComponent<Gestures>().camRot;
            gameObject.GetComponent<RotateWithLocationProvider>().enabled = true;
            player.GetComponent<RotateWithLocationProvider>().enabled = true;
        }
    }
}
