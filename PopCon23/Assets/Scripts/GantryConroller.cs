using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;

public class GantryConroller : MonoBehaviour
{
    VoltageRatioInput sliderX;
    VoltageRatioInput sliderY;

    VoltageRatioInput pressure;

    public Transform railPos;
    public Transform manipPos;
    public Transform targetPos;

    public float pressureThreshold;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        sliderX = new VoltageRatioInput();
        sliderY = new VoltageRatioInput();
        pressure = new VoltageRatioInput();

        sliderX.Channel = 0;
        sliderX.Open(Phidget.DefaultTimeout);
        sliderX.DataInterval = 16;

        sliderY.Channel = 1;
        sliderY.Open(Phidget.DefaultTimeout);
        sliderY.DataInterval = 16;

        pressure.Channel = 2;
        pressure.Open(Phidget.DefaultTimeout);
        pressure.DataInterval = 16;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos.position = new Vector3(x: (float)sliderX.VoltageRatio * -20, y: (float)sliderY.VoltageRatio * -7);
        railPos.position = Vector3.MoveTowards(railPos.position, new Vector3(x: targetPos.position.x, y: railPos.position.y), speed);
        manipPos.position = Vector3.MoveTowards(manipPos.position, new Vector3(x: railPos.position.x, y: manipPos.position.y), speed * 30);
        manipPos.position = Vector3.MoveTowards(manipPos.position, new Vector3(x: manipPos.position.x, y: targetPos.position.y), speed);
        
        if(pressure.VoltageRatio > pressureThreshold)
        {
            Debug.Log("Pushy....");
        }
    }
}
