// NormalFireRateAction.cs
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/NormalFireRate")]
public class NormalFireRateAction : Action
{
    public override void Act(StateController controller)
    {
        PlayerStateController player = (PlayerStateController)controller;
        player.SetNormalFireRate();
    }
}