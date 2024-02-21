using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RocketStatsData
{
    public float Force;
    public float MaxVelocity;
    public RocketStatsData(float force, float maxVelocity)
    {
        Force = force;
        MaxVelocity = maxVelocity;
    }
}
