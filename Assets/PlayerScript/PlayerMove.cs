using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public bool isGrounded;
    public int jumpCount = 0;
    public int maxJumpCount = 2;
    public float jumpForce = 5f;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.X)) // 버튼 이름에 맞게 수정
        {
            Jump();
        }
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJump", !isGrounded);
    }

    void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
        jumpCount++;
        isGrounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))  // 바닥 이름에 맞게 수정
        {
            isGrounded = true;
            jumpCount = 0; // 착지 시 점프 카운트 초기화
        }
    }
}
