using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Method 
{
    public static void DrawWireArc(Vector3 position, Vector3 forwardDir, float anglesRange, float radius, float maxSteps = 50)
    {
        float forwardAnglesDir = GetAnglesFromDir(position, forwardDir);

        Vector3 initialPos = position;

        Vector3 posA = initialPos;

        float stepAngles = anglesRange / maxSteps;
        float angle = forwardAnglesDir - anglesRange / 2;

        for (int i = 0; i <= maxSteps; i++)
        {
            float rad = Mathf.Deg2Rad * angle;
            Vector3 posB = initialPos;
            posB += new Vector3(radius * Mathf.Cos(rad), 0, radius * Mathf.Sin(rad));

            Gizmos.DrawLine(posA, posB);

            angle += stepAngles;
            posA = posB;
        }

        Gizmos.DrawLine(posA, initialPos);
    }

    public static float GetAnglesFromDir(Vector3 pos, Vector3 dir)
    {
        Vector3 forwardLimitPos = pos + dir;
        float sourceAngles = Mathf.Rad2Deg * Mathf.Atan2(forwardLimitPos.z - pos.z, forwardLimitPos.x - pos.x);
        return sourceAngles;
    }

    public static float GetNewValue(float OldMin,float OldMax,float NewMin,float NewMax,float oldValue)
    {
        return ((oldValue - OldMin) * ((NewMax - NewMin) / (OldMax - OldMin))) + NewMin;
    }

    public static Color SetAlpha(this Color a,float alpha)
    {
        a = new Color(a.r, a.g, a.b, alpha);
        return a;
    }
}
