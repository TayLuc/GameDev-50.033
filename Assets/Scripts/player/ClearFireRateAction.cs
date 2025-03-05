// ClearPowerupAction.cs
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/ClearPowerup")]
public class ClearPowerupAction : Action
{
    public override void Act(StateController controller)
    {
        PlayerStateController player = (PlayerStateController)controller;
        player.currentPowerupType = PowerupType.Default;
    }
}