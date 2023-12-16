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
    public float ForwardSpeed;
    public float SidewaySpeed;
    public float IdleSpeed;
    public float IdleTurnSpeed;
}