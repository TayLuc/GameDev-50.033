// PoweredUpFireRateAction.cs
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/PoweredUpFireRate")]
public class PoweredUpFireRateAction : Action
{
    public override void Act(StateController controller)
    {
        PlayerStateController player = (PlayerStateController)controller;
        player.SetPoweredUpFireRate();
    }
}