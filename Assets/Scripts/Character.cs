using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int characterType;
    [SerializeField]
    private GameObject SpotLight;

    private void OnMouseDown()
    {
        Vector3 spotLightPosition = new Vector3(this.transform.position.x, 10, this.transform.position.z);
        SpotLight.transform.position = spotLightPosition;
        UIManager.Instance.UpdateCharacterTypeSelected(characterType);
    }
}
