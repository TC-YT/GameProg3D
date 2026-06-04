using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardForce = 500f;
    public float lateralForce = 15f;
    public float maxLateralPosition = 3f;
    public float targetSpeed = 100f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ForwardMovement();
        LateralMovement();
    }

    void ForwardMovement()
    {
        float currentSpeed = rb.linearVelocity.z;

        if (currentSpeed < targetSpeed)
        {
            rb.AddForce(Vector3.forward * forwardForce * Time.fixedDeltaTime, ForceMode.Force);
        }
        else if (currentSpeed > targetSpeed)
        {
            Vector3 clampedVelocity = rb.linearVelocity;

            clampedVelocity.z = targetSpeed;

            rb.linearVelocity = clampedVelocity;
        }

        //Debug.Log(targetSpeed.ToString() + " -- " + currentSpeed.ToString("F2"));
    }

    void LateralMovement()
    {
        float direction = Input.GetAxis("Horizontal");

        Vector3 lateralVelocity = rb.linearVelocity;
        lateralVelocity.x = direction * lateralForce;
        rb.linearVelocity = lateralVelocity;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x,-maxLateralPosition, maxLateralPosition);
        transform.position = clampedPosition;

        //Debug.Log(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.destroyClip);
            GameStateManager.instance.ChangeToGameOver();
        }
    }
}
