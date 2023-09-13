using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap;
using Leap.Unity;

public static class HandPoints
{
    private static Vector3 initialPosition = new Vector3(0, 0, 0);

    public static void UpdateHandPoints(GameObject[] points, Hand hand)
    {
        if (hand != null)
        {
            // palm
            points[0].transform.localPosition = hand.PalmPosition;

            // thumb
            Finger finger = hand.Fingers[0];

            Bone metacarpal = finger.bones[0];
            Bone proximal = finger.bones[1];
            Bone intermediate = finger.bones[2];
            Bone distal = finger.bones[3];

            //points[1].transform.localPosition = metacarpal.PrevJoint;
            points[1].transform.localPosition = proximal.NextJoint;
            points[2].transform.localPosition = intermediate.NextJoint;
            points[3].transform.localPosition = distal.NextJoint;

            // index
            finger = hand.Fingers[1];

            metacarpal = finger.bones[0];
            proximal = finger.bones[1];
            intermediate = finger.bones[2];
            distal = finger.bones[3];

            points[4].transform.localPosition = metacarpal.PrevJoint;
            points[5].transform.localPosition = metacarpal.NextJoint;
            points[6].transform.localPosition = proximal.NextJoint;
            points[7].transform.localPosition = intermediate.NextJoint;
            points[8].transform.localPosition = distal.NextJoint;

            // middle, ring
            for (int f = 2; f < 4; f++)
            {
                finger = hand.Fingers[f];

                metacarpal = finger.bones[0];
                proximal = finger.bones[1];
                intermediate = finger.bones[2];
                distal = finger.bones[3];

                //points[9+((f-2)*5)].transform.localPosition = metacarpal.PrevJoint;
                points[9+((f-2)*4)].transform.localPosition = metacarpal.NextJoint;
                points[9+((f-2)*4)+1].transform.localPosition = proximal.NextJoint;
                points[9+((f-2)*4)+2].transform.localPosition = intermediate.NextJoint;
                points[9+((f-2)*4)+3].transform.localPosition = distal.NextJoint;
            }

            // pinky
            finger = hand.Fingers[4];

            metacarpal = finger.bones[0];
            proximal = finger.bones[1];
            intermediate = finger.bones[2];
            distal = finger.bones[3];

            points[17].transform.localPosition = metacarpal.PrevJoint;
            points[18].transform.localPosition = metacarpal.NextJoint;
            points[19].transform.localPosition = proximal.NextJoint;
            points[20].transform.localPosition = intermediate.NextJoint;
            points[21].transform.localPosition = distal.NextJoint;
        }
        else // if hand is null, return hand points to initial position (0, 0, 0), so that they are not visible to the camera
        {
            foreach (var point in points)
            {
                point.transform.localPosition = initialPosition;
            }
        }
    }
}
