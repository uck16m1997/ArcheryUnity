using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowIdleState : BowBaseState
{

    public event System.Action BowIdle;

    public override void HandleInput(BowController bowController)
    {
        if (Input.GetMouseButtonDown(0))
        {
            bowController.ActiveState = BowController.Shooting;
            bowController.ActiveState.OnEnter(bowController);
        }
    }

    public override void OnEnter(BowController bowController)
    {
        bowController.BowAnimator.SetBool("drawing", false);
        BowIdle?.Invoke();
    }
}
