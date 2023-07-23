using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    [Range(3, 30)]
    private int lineSegmentCount = 20;

    private List<Vector3> linePoints = new List<Vector3>();

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        // Get velocity for impulse force
        Vector3 velocity = (forceVector / rigidbody.mass);
        // Duration of the flight 
        float flightDuration = QuadraticEquation(velocity.y, startingPoint.y);
        // Every segment will be a step and take stepTime to complete
        float stepTime = flightDuration / lineSegmentCount;
        // Clear lines
        linePoints.Clear();
        linePoints.Add(startingPoint);
        for (int i = 1; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;

            // Calculate where will the projectile pass through at each time
            Vector3 movementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed + 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed);

            Vector3 newPointOnLine = startingPoint + movementVector;

            // Stop on hit
            RaycastHit hit;
            if (Physics.Raycast(linePoints[i - 1], newPointOnLine - linePoints[i - 1], out hit, (newPointOnLine - linePoints[i - 1]).magnitude))
            {
                linePoints.Add(hit.point);
                break;
            }

            linePoints.Add(startingPoint + movementVector);
        }

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }

    float QuadraticEquation(float speedY, float height)
    {
        // Solve the quadratic equation for time and take the positive root
        float time = (speedY * -1 + Mathf.Sqrt(speedY * speedY + -2 * Physics.gravity.y * height)) / Physics.gravity.y;
        if (time < 0)
        {
            time = (speedY * -1 - Mathf.Sqrt(speedY * speedY + -2 * Physics.gravity.y * height)) / Physics.gravity.y;
        }
        return time;
    }



    public void HideLine()
    {
        // Clear lines and lineRenderer
        linePoints.Clear();
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
}
