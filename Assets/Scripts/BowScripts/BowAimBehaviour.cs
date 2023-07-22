using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAimBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 0.5f;

    [SerializeField]
    private Vector2 _moveRange = new Vector2(50, 95);

    private Vector3 _targetAngle;
    private Quaternion _startRotation;

    // Start is called before the first frame update
    void Start()
    {
        _startRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse X and Y 
        _targetAngle.y += Input.GetAxis("Mouse X") * _moveSpeed;
        _targetAngle.x += Input.GetAxis("Mouse Y") * _moveSpeed;

        _targetAngle.y = Mathf.Clamp(_targetAngle.y, -_moveRange.y * 0.5f, _moveRange.y * 0.5f);
        _targetAngle.x = Mathf.Clamp(_targetAngle.x, -_moveRange.x * 0.5f, _moveRange.x * 0.5f);

        Quaternion targetRotation = Quaternion.Euler(-_targetAngle.x, _targetAngle.y, 0);
        transform.localRotation = _startRotation * targetRotation;
    }
}
