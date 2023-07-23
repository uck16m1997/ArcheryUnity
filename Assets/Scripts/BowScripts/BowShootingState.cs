using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowShootingState : BowBaseState
{

    private float currentLerpTime;
    private float maximumLerpTime = 1f;
    private float currentArrowForce;
    private float maximumArrowForce = 200f;
    private Rigidbody arrowRigidbody;
    public event System.Action BowReleased;
    public event System.Action BowDrawing;

    public BowShootingState(float maxArrFoce, float maxLerpTime)
    {
        maximumArrowForce = maxArrFoce;
        maximumLerpTime = maxLerpTime;
    }



    public override void HandleInput(BowController bowController)
    {

        if (Input.GetMouseButton(0))
        {
            PowerUpBow(bowController);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseBow(bowController);
            bowController.ActiveState = BowController.Idle;
            bowController.ActiveState.OnEnter(bowController);
        }
    }

    private void PowerUpBow(BowController bowController)
    {

        currentLerpTime += Time.deltaTime;

        if (currentLerpTime > maximumLerpTime)
        {
            currentLerpTime = maximumLerpTime;
        }
        float perc = currentLerpTime / maximumLerpTime;
        currentArrowForce = Mathf.Lerp(0, maximumArrowForce, perc);

        bowController.TrajectoryDrawer.UpdateTrajectory(
            bowController.ArrowTransform.forward * currentArrowForce,
            ArrowFactory.Instance.ArrowPrefab.GetComponent<Rigidbody>(),
            bowController.ArrowTransform.position
        );
    }


    private void ReleaseBow(BowController bowController)
    {
        BowReleased?.Invoke();
        bowController.BowAnimator.SetBool("drawing", false);

        bowController.ShootArrow(currentArrowForce);

        currentLerpTime = 0;
        currentArrowForce = 0;

        bowController.TrajectoryDrawer.HideLine();
    }

    public override void OnEnter(BowController bowController)
    {
        BowDrawing?.Invoke();
        bowController.BowAnimator.SetBool("drawing", true);
    }
}
