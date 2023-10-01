using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Leap;
using Leap.Unity;


public interface IHand
{
    IList<IFinger> Fingers { get; }
    Vector3 PalmPosition { get; }
}

public interface IFinger
{
    IList<IBone> bones { get; }
}

public interface IBone
{
    Vector3 PrevJoint { get; }
    Vector3 NextJoint { get; }
}


public class LeapHand : IHand
{
    private IList<IFinger> fingers;
    private Vector3 palmPosition;

    public LeapHand(IList<IFinger> newFingers, Vector3 newPalmPosition)
    {
        fingers = newFingers;
        palmPosition = newPalmPosition;
    }

    public IList<IFinger> Fingers { get => fingers; }
    public Vector3 PalmPosition { get => palmPosition; }
    
    public static explicit operator LeapHand(Hand hand) => (hand == null) ? null : new LeapHand(hand.Fingers.ConvertAll((finger) => (LeapFinger)finger).ToList<IFinger>(), hand.PalmPosition);
}

public class LeapFinger : IFinger
{
    private IList<IBone> Bones;

    public LeapFinger(IList<IBone> newBones)
    {
        Bones = newBones;
    }

    public IList<IBone> bones { get => Bones; }

    public static explicit operator LeapFinger(Finger finger) => new LeapFinger(Array.ConvertAll(finger.bones, (bone) => (LeapBone)bone));
}

public class LeapBone : IBone
{
    private Vector3 prevJoint;
    private Vector3 nextJoint;

    public LeapBone(Vector3 newPrevJoint, Vector3 newNextJoint)
    {
        prevJoint = newPrevJoint;
        nextJoint = newNextJoint;
    }

    public Vector3 PrevJoint { get => prevJoint; }
    public Vector3 NextJoint { get => nextJoint; }

    public static explicit operator LeapBone(Bone bone) => new LeapBone(bone.PrevJoint, bone.NextJoint);
}


public static class HandPoints
{
    private static Vector3 initialPosition = new Vector3(0, 0, 0);

    public static void UpdateHandPoints(GameObject[] points, IHand hand)
    {
        if (hand != null)
        {
            // palm
            points[0].transform.localPosition = hand.PalmPosition;

            // thumb
            IFinger finger = hand.Fingers[0];

            IBone metacarpal = finger.bones[0];
            IBone proximal = finger.bones[1];
            IBone intermediate = finger.bones[2];
            IBone distal = finger.bones[3];

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
