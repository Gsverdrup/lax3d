using UnityEngine;

public class NorthGoalieController : MonoBehaviour
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

        // Vector from crease center to ball (XZ plane)
        Vector3 toBall = ball.position - creaseCenter.position;
        toBall.y = 0f;

        // Compute angle around Y, flipped in Z
        float angle = Mathf.Atan2(toBall.x, -toBall.z) * Mathf.Rad2Deg;
        float clamped = Mathf.Clamp(angle, minAngle, maxAngle);

        // Rebuild target on a flipped (downward) arc
        float rad = clamped * Mathf.Deg2Rad;
        Vector3 arcOffset = new Vector3(
            Mathf.Sin(rad),
            0f,
            -Mathf.Cos(rad)    // <- negate Cos here
        ) * radius;

        Vector3 targetPos = creaseCenter.position + arcOffset;

        // Smooth move
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
