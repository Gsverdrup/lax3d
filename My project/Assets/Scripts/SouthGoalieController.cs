using UnityEngine;

public class SouthGoalieController : MonoBehaviour
{
    public Transform ball;
    public Transform creaseCenter;
    public float radius = 2.0f;
    public float moveSpeed = 3.0f;
    public float minAngle = -60f; // degrees
    public float maxAngle = 60f;  // degrees

    void Update()
    {
        if (ball == null || creaseCenter == null) return;

        // Direction from crease center to ball
        Vector3 toBall = ball.position - creaseCenter.position;
        toBall.y = 0f; // Keep on same plane

        // Get angle from forward direction
        float angle = Mathf.Atan2(toBall.x, toBall.z) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        // Convert back to position on arc
        float rad = angle * Mathf.Deg2Rad;
        Vector3 targetPos = new Vector3(
            Mathf.Sin(rad),
            0f,
            Mathf.Cos(rad)
        ) * radius + creaseCenter.position;

        // Smoothly move goalie to target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
