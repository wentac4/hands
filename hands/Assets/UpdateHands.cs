using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap;
using Leap.Unity;

public class UpdateHands : MonoBehaviour
{
    public LeapProvider leapProvider;

    public GameObject[] leftHandPoints;
    public GameObject[] rightHandPoints;

    void Update()
    {
        HandPoints.UpdateHandPoints(leftHandPoints, leapProvider.CurrentFrame.GetHand(Chirality.Left));   // left hand
        HandPoints.UpdateHandPoints(rightHandPoints, leapProvider.CurrentFrame.GetHand(Chirality.Right)); // right hand
    }
}
