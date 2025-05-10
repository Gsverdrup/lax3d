using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float moveSpeed = 0;

    public bool hasBall;
    public GameObject ball;
    private BallController ballController;
    private Rigidbody ballRb;
    public Vector3 ballOffset = new Vector3(1f, 0.75f, 0f);

    public float shootForce = 20f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        
        if (ball != null) {
            ballController = ball.GetComponent<BallController>();
            ballRb = ball.GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate() {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void Update() {
        if (hasBall && ball != null) {
            ball.transform.position = transform.position + transform.TransformDirection(ballOffset);
            if (ballRb != null) {
                ballRb.isKinematic = true;
            }
        } 

        if (hasBall && Input.GetMouseButtonDown(0)) {
            ShootBall();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ball") /*&& !(other.inPossesion)*/) {
            hasBall = true;
            // ball.SetActive(false);
            ballController.inPossesion = true;
        }
    }

    void OnMove (InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void ShootBall() {
        hasBall = false;
        ballRb.isKinematic = false;
        ballController.inPossesion = false;
        
        //ball.transform.position = transform.position + transform.forward * 1.5f + Vector3.up * 0.5f;
        //ball.SetActive(true);
        ballRb.linearVelocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;

        //Vector3 shotForce = transform.forward * 30f + Vector3.up * 2f;
        //ballRb.AddForce(shotForce, ForceMode.Impulse);

        // Get the mouse click direction from the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            // Get the direction from the ball to the hit point
            Vector3 direction = (hit.point - ball.transform.position).normalized;

            // Apply impulse force in that direction
            ballRb.AddForce(direction * shootForce, ForceMode.Impulse);
        }
    }
}
