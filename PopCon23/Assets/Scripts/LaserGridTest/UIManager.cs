using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject EmergencyButtonControls;

    public void EmergencyButtonToggle()
    {
        if (EmergencyButtonControls.activeSelf == false){
            EmergencyButtonControls.SetActive(true);
        }
        else
        {
            EmergencyButtonControls.SetActive(false);
        }
    }
}
