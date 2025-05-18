using UnityEngine;
using TMPro;

public class NorthGoalController : MonoBehaviour
{
    public TMP_Text score1;
    public int score_count = 0;


    public GameObject ball;
    private Rigidbody ballRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRb = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        score1.text = score_count.ToString();
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball"))
        {
            score_count += 1;
            ball.transform.position = new Vector3(0, 0.125f, 0);
            ballRb.linearVelocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
        }
    }
}
