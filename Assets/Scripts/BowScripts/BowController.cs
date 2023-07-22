using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private Transform arrowTransform;

    [SerializeField]
    private float maximumArrowForce = 200f;
    [SerializeField]
    private float maxLerpTime = 1f;
    public Transform ArrowTransform
    {
        get => arrowTransform;
    }

    public static BowIdleState Idle;
    public static BowShootingState Shooting;
    private BowBaseState activeState;
    public BowBaseState ActiveState
    {
        get => activeState;
        set => activeState = value;
    }

    private Animator bowAnimator;

    public Animator BowAnimator
    {
        get => bowAnimator;
    }

    public void ShootArrow(float currentArrowForce)
    {
        GameObject arrow = ArrowFactory.Instance.GetProduct(ArrowTransform.position, ArrowTransform.rotation);
        arrow.GetComponent<Rigidbody>().AddForce(ArrowTransform.forward * currentArrowForce, ForceMode.Impulse);

    }



    // Start is called before the first frame update
    void Awake()
    {
        Idle = new BowIdleState();
        Shooting = new BowShootingState(maximumArrowForce, maxLerpTime);

        activeState = Idle;
        bowAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        activeState.HandleInput(this);
    }
}
