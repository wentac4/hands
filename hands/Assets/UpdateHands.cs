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
        HandPoints.UpdateHandPoints<LeapHand>(leftHandPoints, leapProvider.CurrentFrame.GetHand(Chirality.Left) ?? new LeapHand());   // left hand
        HandPoints.UpdateHandPoints<LeapHand>(rightHandPoints, leapProvider.CurrentFrame.GetHand(Chirality.Right) ?? new LeapHand()); // right hand
    }
}
