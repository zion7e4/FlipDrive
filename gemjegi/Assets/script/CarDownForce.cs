using UnityEngine;

public class CarDownForce : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform frontPoint;
    public float downForceStrength = 10f;

    carmovement carMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        carMovement = GetComponent<carmovement>();
    }

    void FixedUpdate()
    {
        if (carMovement.isOnGround)
        {
            Vector2 downForce = Vector2.down * downForceStrength;
            rb.AddForceAtPosition(downForce, frontPoint.position);
        }
    }
}
