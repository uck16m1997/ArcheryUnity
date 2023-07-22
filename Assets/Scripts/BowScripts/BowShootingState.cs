using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowShootingState : BowBaseState
{
    private float _currentLerpTime;
    private float _maxLerpTime = 1f;
    private float _currentArrowForce;
    private float _maximumArrowForce = 200f;
    public event System.Action BowReleased;
    public event System.Action BowDrawing;

    public BowShootingState(float maximumArrowForce, float maxLerpTime)
    {
        _maximumArrowForce = maximumArrowForce;
        _maxLerpTime = maxLerpTime;
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
        if (bowController.BowAnimator.GetBool("drawing"))
        {
            _currentLerpTime += Time.deltaTime;

            if (_currentLerpTime > _maxLerpTime)
            {
                _currentLerpTime = _maxLerpTime;
            }

            float perc = _currentLerpTime / _maxLerpTime;
            _currentArrowForce = Mathf.Lerp(0, _maximumArrowForce, perc);
        }
    }


    private void ReleaseBow(BowController bowController)
    {
        BowReleased?.Invoke();
        bowController.BowAnimator.SetBool("drawing", false);

        bowController.ShootArrow(_currentArrowForce);

        _currentLerpTime = 0;
        _currentArrowForce = 0;
    }

    public override void OnEnter(BowController bowController)
    {
        BowDrawing?.Invoke();
        bowController.BowAnimator.SetBool("drawing", true);
    }
}
