﻿using StereoKit;
using StereoKit.Framework;

namespace Nazar.Framework.Steppers;

public class AvatarSkeleton : IStepper
{
    private readonly LinePoint[] _armLine = new LinePoint[4];
    private readonly LinePoint[] _headLine = new LinePoint[3];

    public bool Enabled => true;

    public bool Initialize()
    {
        for (int i = 0; i < _headLine.Length; i++)
        {
            _headLine[i].color = Color32.White;
            _headLine[i].thickness = 0.01f;
        }

        for (int i = 0; i < 4; i++)
        {
            _armLine[i].color = Color32.White;
            _armLine[i].thickness = 0.01f;
        }

        return true;
    }

    public void Shutdown()
    {
    }

    public void Step()
    {
        const float shoulderWidth = 0.16f; // half the total shoulder width
        const float forearm = 0.22f; // length of the forearm
        const float upperArm = 0.25f; // length of the upper arm
        const float headLength = 0.1f; // length from head point to neck
        const float neckLength = 0.02f; // length from neck to center shoulders
        const float shoulderDrop = 0.05f; // shoulder drop height from shoulder center
        const float elbowFlare = 0.45f; // Elbows point at a location 1m down, and elbowFlare out from the shoulder

        Pose head = Input.Head;

        // Head, neck, and shoulder center
        _headLine[0].pt = head.position;
        _headLine[1].pt = _headLine[0].pt + head.orientation * V.XYZ(0, -headLength, 0);
        _headLine[2].pt = _headLine[1].pt - V.XYZ(0, neckLength, 0);
        _headLine[0].pt = Vec3.Lerp(_headLine[0].pt, _headLine[1].pt, 0.1f);
        Lines.Add(_headLine);

        // Shoulder forward facing direction is head direction weighted 
        // equally with the direction of both hands.
        Vec3 forward =
            head.Forward.X0Z.Normalized * 2
            + (Input.Hand(Handed.Right).wrist.position - _headLine[2].pt.X0Z).Normalized
            + (Input.Hand(Handed.Left).wrist.position - _headLine[2].pt.X0Z).Normalized;
        forward = forward * 0.25f;
        Vec3 right = Vec3.PerpendicularRight(forward, Vec3.Up).Normalized;

        // Now for each arm
        for (int h = 0; h < 2; h++)
        {
            float handed = h == 0 ? -1 : 1;
            Hand hand = Input.Hand((Handed) h);
            Vec3 handPos = hand.wrist.position;
            if (!hand.IsTracked) continue;

            _armLine[0].pt = _headLine[2].pt;
            _armLine[1].pt = _armLine[0].pt + right * handed * shoulderWidth - Vec3.Up * shoulderDrop;
            _armLine[3].pt = handPos;

            // Triangle represented by 3 edges, forearm, upperArm, and armDist
            float armDist = Math.Min(forearm + upperArm, Vec3.Distance(_armLine[1].pt, handPos));

            // Heron's formula to find area
            float s = (forearm + upperArm + armDist) / 2;
            float area = SKMath.Sqrt(s * (s - forearm) * (s - upperArm) * (s - armDist));
            // Height of triangle based on area
            float offsetH = 2 * area / armDist;
            // Height can now be used to calculate how far off the elbow is
            float offsetD = SKMath.Sqrt(Math.Abs(offsetH * offsetH - upperArm * upperArm));

            // Elbow calculation begins somewhere along the line between the
            // shoulder and the wrist.
            Vec3 dir = (handPos - _armLine[1].pt).Normalized;
            Vec3 at = _armLine[1].pt + dir * offsetD;
            // The elbow naturally flares out to the side, rather than 
            // dropping straight down. Here, we find a point to flare out 
            // towards.
            Vec3 flarePoint = _headLine[2].pt + right * handed * elbowFlare - Vec3.Up;
            Plane flarePlane = new(flarePoint, Ray.FromTo(_headLine[2].pt, handPos).Closest(flarePoint) - flarePoint);
            Vec3 dirDown = (flarePlane.Closest(at) - at).Normalized;
            _armLine[2].pt = at + dirDown * offsetH;

            Lines.Add(_armLine);
            for (int i = 1; i < 5; i++)
                Lines.Add(handPos, hand[(FingerId) i, JointId.KnuckleMajor].position, Color32.White,
                    new Color32(255, 255, 255, 0), 0.01f);
        }
    }
}