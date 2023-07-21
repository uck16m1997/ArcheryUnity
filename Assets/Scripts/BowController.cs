using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.5f;

    [SerializeField]
    private Vector2 moveRange = new Vector2(50, 95);

    private Vector3 targetAngle;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse X and Y 
        targetAngle.y += Input.GetAxis("Mouse X") * moveSpeed;
        targetAngle.x += Input.GetAxis("Mouse Y") * moveSpeed;

        targetAngle.y = Mathf.Clamp(targetAngle.y, -moveRange.y * 0.5f, moveRange.y * 0.5f);
        targetAngle.x = Mathf.Clamp(targetAngle.x, -moveRange.x * 0.5f, moveRange.x * 0.5f);

        Quaternion targetRotation = Quaternion.Euler(-targetAngle.x, targetAngle.y, 0);
        transform.localRotation = startRotation * targetRotation;
    }
}
