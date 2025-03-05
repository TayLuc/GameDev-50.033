// PowerupCollectedDecision.cs
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/PowerupCollected")]
public class PowerupCollectedDecision : Decision
{
    // Specify which powerup type this decision checks for
    public PowerupType powerupToCheck = PowerupType.FireRateBoost;

    public override bool Decide(StateController controller)
    {

        PlayerStateController player = (PlayerStateController)controller;

        // If player has collected the specified powerup type
        bool powerupMatches = player.currentPowerupType == powerupToCheck;

        // If it matches and we're not already in powered up state, 
        // we should reset the state timer
        if (powerupMatches && player.currentState.name != "PoweredUpState")
        {
            player.stateTimeElapsed = 0f;
        }

        return powerupMatches;
    }
}