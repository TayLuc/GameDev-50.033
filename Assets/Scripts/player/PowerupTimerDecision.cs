// PowerupTimerDecision.cs
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/PowerupTimer")]
public class PowerupTimerDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        PlayerStateController player = (PlayerStateController)controller;

        // Check if the powerup duration has elapsed
        return player.CheckIfCountDownElapsed(player.powerupDuration);
    }
}