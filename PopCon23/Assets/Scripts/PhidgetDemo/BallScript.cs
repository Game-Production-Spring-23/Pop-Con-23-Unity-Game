using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;

public class BallScript : MonoBehaviour
{

    RFID rfid;

    // Start is called before the first frame update
    void Start()
    {
        rfid = new RFID();
        //Open
        rfid.Open(1000);

        Debug.Log(rfid);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rfid.TagPresent);
        
    }
}
