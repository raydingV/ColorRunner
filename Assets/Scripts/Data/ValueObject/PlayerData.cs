using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public PlayerMovementData playerMovementData;
}
    
[Serializable]
public class PlayerMovementData
{
    [Header("Runner")] 
    public Vector2 ClampValues;
    public float RunnerForwardSpeed;
    public float RunnerSidewaySpeed;
    [Range(0,1)] public float RunnerMaxRotateAngle;
    public float RunnerTurnSpeed;
    [Space][Header("Idle")]
    public float IdleSpeed;
    public float IdleTurnSpeed;
}