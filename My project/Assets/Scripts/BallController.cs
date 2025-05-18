using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool inPossesion;
    public Vector3 movement;
    private Rigidbody ballRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        inPossesion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inPossesion)
        {
            gameObject.SetActive(false);
        }

        if (transform.position.y < -1)
        {
            transform.position = new Vector3(0, 0.125f, 0);
            ballRb.linearVelocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag("Goalie")) {
        ballRb.linearVelocity *= 0.4f; // Reduce speed after hit
    }
}
}
