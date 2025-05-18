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
        if (hasBall && ball != null)
        {
            ball.transform.position = transform.position;
            if (ballRb != null)
            {
                ballRb.isKinematic = true;
            }
        } 

        if (hasBall && Input.GetMouseButtonDown(0)) {
            ShootBall();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            hasBall = true;
            ballController.inPossesion = true;
        }
    }

    void OnMove (InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void ShootBall() {
        // Get the mouse click direction from the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            hasBall = false;
            ballRb.isKinematic = false;
            ballController.inPossesion = false;

            // Get the direction from the ball to the hit point
            Vector3 direction = (hit.point - ball.transform.position).normalized;

            ball.transform.position = transform.position + direction * 1.5f;
            ball.SetActive(true);
            ballRb.linearVelocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;

            // Apply impulse force in that direction
            ballRb.AddForce(direction * shootForce, ForceMode.Impulse);
        }
    }
}
