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
    public Vector2 ClampValues = new Vector2(-4, 4);

    public float RunnerForwardSpeed = 10f;
    public float RunnerSidewaySpeed = 2f;
    public float SizeUpValue = 0.025f;
    public float MaxSizeValue = 2.5f;
    public float MinSizeValue = 1f;

    [Space]
    [Header("Idle")]
    public float IdleSpeed = 8f;
    public float IdleTurnSpeed = .5f;
}