using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;

public class AdvancedHandVisualizer : MonoBehaviour
{
    public LeapProvider leapProvider;
    public LineRenderer palmLine;
    public Transform palmSpike;
    private void OnEnable()
    {
        leapProvider.OnUpdateFrame += OnUpdateFrame;
    }
    private void OnDisable()
    {
        leapProvider.OnUpdateFrame -= OnUpdateFrame;
    }

    void OnUpdateFrame(Frame frame)
    {
        foreach(Hand hand in frame.Hands)
        {
            foreach(Finger finger in hand.Fingers)
            {
                Debug.Log("Finger" + finger.Id + " on hand" + hand.Id + " is " + finger.IsExtended);
            }
        }
        palmLine.SetPositions(new Vector3[] { frame.Hands[0].PalmPosition, frame.Hands[0].Direction });
        palmSpike.position = frame.Hands[0].PalmPosition - new Vector3(0,0,0);
        palmSpike.rotation = frame.Hands[0].Rotation;
    }
}
