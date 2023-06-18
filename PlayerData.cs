using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data")]
public class PlayerData: ScriptableObject
{
    [Header("Movement")]
    public float swipeRange;
    public float dashForce;
    public float maxSwipeBooster;
    public float swipeTime;
    public float normalDrag;
    public float chargedDrag;
    public float swipeCooldown;
    public float terminalVelocity;
    public float timeToTerminal;
}
