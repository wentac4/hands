using Leap;
using Leap.Unity;
using UnityEngine;

public class HandPoints : MonoBehaviour
{
    public LeapProvider leapProvider;

    public GameObject[] leftHandPoints;
    public GameObject[] rightHandPoints;

    private Vector3 initialPosition = new Vector3(0, 0, 0);

    private void RenderHandPoints(GameObject[] handPoints, Hand hand)
    {
        if (hand != null)
        {
            // palm
            handPoints[0].transform.localPosition = hand.PalmPosition;

            // thumb
            Finger finger = hand.Fingers[0];

            Bone metacarpal = finger.bones[0];
            Bone proximal = finger.bones[1];
            Bone intermediate = finger.bones[2];
            Bone distal = finger.bones[3];

            //handPoints[1].transform.localPosition = metacarpal.PrevJoint;
            handPoints[1].transform.localPosition = proximal.NextJoint;
            handPoints[2].transform.localPosition = intermediate.NextJoint;
            handPoints[3].transform.localPosition = distal.NextJoint;

            // index
            finger = hand.Fingers[1];

            metacarpal = finger.bones[0];
            proximal = finger.bones[1];
            intermediate = finger.bones[2];
            distal = finger.bones[3];

            handPoints[4].transform.localPosition = metacarpal.PrevJoint;
            handPoints[5].transform.localPosition = metacarpal.NextJoint;
            handPoints[6].transform.localPosition = proximal.NextJoint;
            handPoints[7].transform.localPosition = intermediate.NextJoint;
            handPoints[8].transform.localPosition = distal.NextJoint;

            // middle, ring
            for (int f = 2; f < 4; f++)
            {
                finger = hand.Fingers[f];

                metacarpal = finger.bones[0];
                proximal = finger.bones[1];
                intermediate = finger.bones[2];
                distal = finger.bones[3];

                //handPoints[9+((f-2)*5)].transform.localPosition = metacarpal.PrevJoint;
                handPoints[9+((f-2)*4)].transform.localPosition = metacarpal.NextJoint;
                handPoints[9+((f-2)*4)+1].transform.localPosition = proximal.NextJoint;
                handPoints[9+((f-2)*4)+2].transform.localPosition = intermediate.NextJoint;
                handPoints[9+((f-2)*4)+3].transform.localPosition = distal.NextJoint;
            }

            // pinky
            finger = hand.Fingers[4];

            metacarpal = finger.bones[0];
            proximal = finger.bones[1];
            intermediate = finger.bones[2];
            distal = finger.bones[3];

            handPoints[17].transform.localPosition = metacarpal.PrevJoint;
            handPoints[18].transform.localPosition = metacarpal.NextJoint;
            handPoints[19].transform.localPosition = proximal.NextJoint;
            handPoints[20].transform.localPosition = intermediate.NextJoint;
            handPoints[21].transform.localPosition = distal.NextJoint;
        }
        else // if hand is null, return hand points to initial position (0, 0, 0), so that they are not visible to the camera
        {
            foreach (var point in handPoints)
            {
                point.transform.localPosition = initialPosition;
            }
        }
    }

    void Update()
    {
        RenderHandPoints(leftHandPoints, leapProvider.CurrentFrame.GetHand(Chirality.Left));   // left hand
        RenderHandPoints(rightHandPoints, leapProvider.CurrentFrame.GetHand(Chirality.Right)); // right hand
    }
}
