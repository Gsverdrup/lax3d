using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool inPossesion;
    public Vector3 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inPossesion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inPossesion) {
            gameObject.SetActive(false);
        }
    }
}
