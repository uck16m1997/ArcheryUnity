using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{

    // How far will the target will be moving
    [SerializeField]
    private float amplitude = 1f;

    // Speed of the movement
    [SerializeField]
    private float timePeriod = 1f;

    // Probability of this target being mobile
    [SerializeField]
    private float probOfMovement = 0.5f;
    private ParticleSystem impactParticles;
    private Vector3 startPosition;
    private bool isMoving = false;
    public event System.Action GotHit;

    // Start is called before the first frame update
    void Start()
    {
        // Find Particle System 
        impactParticles = transform.GetComponentInChildren<ParticleSystem>();
        // Initialize this target
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // If game has started and this target isMoving
        if (GameManager.GameStarted && isMoving)
        {
            PeriodicMovement();
        }
    }

    public void Initialize()
    {
        // Will this target be mobile
        isMoving = UnityEngine.Random.Range(0f, 1f) >= probOfMovement;
        // Starting from transform position
        startPosition = transform.localPosition;
    }

    public void CleanUp()
    {
        // Stop movement on hit
        isMoving = false;
        // Stop collisions to not block the player
        GetComponent<Collider>().enabled = false;
        // To enable effects of gravity again
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject, 1f);
    }

    void PeriodicMovement()
    {
        // Parameter that will be given to sin wave to calculate movement
        float theta = Time.timeSinceLevelLoad / timePeriod;

        // Change in position relative to the base position
        Vector3 deltaPosition = new Vector3(0, 0, Mathf.Sin(theta) * amplitude);
        transform.position = startPosition + deltaPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If collision happened with an Arrow object
        if (collision.gameObject.CompareTag("Arrow"))
        {
            // Audio 
            // impactSound.volume = collision.relativeVelocity.normalized.magnitude;
            GotHit?.Invoke();

            // Play particle effects 
            impactParticles.Play();

            // Increase score and decrease targets onHit
            GameManager.Score++;
            TargetSpawner.TotalTargets--;

            // Arrow object will be sticking to this target
            collision.transform.parent = transform;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // Do clean ups
            CleanUp();
        }
    }
}
