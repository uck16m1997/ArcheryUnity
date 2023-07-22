using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private Transform _arrowTransform;

    [SerializeField]
    private float _maximumArrowForce = 200f;
    [SerializeField]
    private float _maxLerpTime = 1f;
    public Transform ArrowTransform
    {
        get => _arrowTransform;
    }

    public static BowIdleState Idle;
    public static BowShootingState Shooting;
    private BowBaseState _activeState;
    public BowBaseState ActiveState
    {
        get => _activeState;
        set => _activeState = value;
    }

    private Animator _bowAnimator;

    public Animator BowAnimator
    {
        get => _bowAnimator;
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
        Shooting = new BowShootingState(_maximumArrowForce, _maxLerpTime);

        _activeState = Idle;
        _bowAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        _activeState.HandleInput(this);
    }
}
