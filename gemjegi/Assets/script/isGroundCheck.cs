using UnityEngine;

public class isGroundCheck : MonoBehaviour
{
    carmovement CarMovement;
    private Rigidbody2D rb;
    public float checkDistance = 0.1f; // 바닥 체크 거리
    public LayerMask groundLayer; // 바닥 레이어

    void Start()
    {
        CarMovement = transform.parent.GetComponent<carmovement>();
        //CarMovement = GetComponentInParent<carmovement>();
        rb = GetComponent<Rigidbody2D>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   /* // 지면에 닿았는지 여부 확인 (OnCollisionEnter2D 사용)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CarMovement.isGrounded = true; // 지면에 닿았으면 회전 불가능
        }
    }

    // 충돌이 끝났을 때 지면에 떨어진 경우
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CarMovement.isGrounded = false; // 지면에서 떨어지면 회전 가능
        }
    }*/

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            CarMovement.isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            CarMovement.isGrounded = false;
        }
    }*/
}
