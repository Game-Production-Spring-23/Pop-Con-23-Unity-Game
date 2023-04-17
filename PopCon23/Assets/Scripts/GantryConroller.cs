using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;

public class GantryConroller : MonoBehaviour
{
    VoltageRatioInput sliderX;
    VoltageRatioInput sliderY;
    public Transform railPos;
    public Transform manipPos;
    public Transform targetPos;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        sliderX = new VoltageRatioInput();
        sliderY = new VoltageRatioInput();

        sliderX.Channel = 0;
        sliderX.Open(Phidget.DefaultTimeout);
        sliderX.DataInterval = 16;

        sliderY.Channel = 1;
        sliderY.Open(Phidget.DefaultTimeout);
        sliderY.DataInterval = 16;
    }

    // Update is called once per frame
    void Update()
    {
        railPos.position = new Vector3(x: (float)sliderX.VoltageRatio * -20, y: -3.5f);
        manipPos.position = new Vector3(x: railPos.position.x, y: (float)sliderY.VoltageRatio * -7);
        Debug.Log((float)sliderX.VoltageRatio);
    }
}
