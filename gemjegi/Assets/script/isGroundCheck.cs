using UnityEngine;

public class isGroundCheck : MonoBehaviour
{
    public bool isGrounded = false; // 지면에 닿았는지 여부

    // 지면에 닿았는지 여부 확인 (OnCollisionEnter2D 사용)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 지면에 닿았으면 회전 불가능
        }
    }

    // 충돌이 끝났을 때 지면에 떨어진 경우
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // 지면에서 떨어지면 회전 가능
        }
    }

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
