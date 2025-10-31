using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public bool isGrounded;
    public bool isSlide;
    public int jumpCount = 0;
    public int maxJumpCount = 2;
    public float jumpForce = 5f;
    public float slideDuration = 1f;
    private float slideTimer = 0f;
    private Rigidbody2D _rigidbody2D;
    public int maxHp = 100;
    public int currentHp;
    public float tickRate = 1f;
    public int tickDamage = 1;
    private float tickTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxJumpCount > jumpCount && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.X))) // 버튼 이름에 맞게 변경
        {
            Jump();
        }

        if (!isSlide && Input.GetKey(KeyCode.C)) // || Input.GetButtonDown("Slide") 추후 슬라이드 버튼 추가하면 같이 추가
        {
            Slide();
        }
        
        if(isSlide && Input.GetKeyUp(KeyCode.C))
        {
            isSlide = false;
        }
        



        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJump", !isGrounded);
        animator.SetBool("isSlide", isSlide);

        tickTimer += Time.deltaTime;
        if (tickTimer >= tickRate)
        {
            tickTimer = 0f;
            TakeDamage(tickDamage);
        }
    }

    void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
        jumpCount++;
        isGrounded = false;
    }

    void Slide()
    {
        if (!isSlide)
        {
            isSlide = true;
            slideTimer = 0f;
            animator.SetTrigger("isSlide");
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // �ٴ� �̸��� �°� ����
        {
            isGrounded = true;
            jumpCount = 0; // ���� �� ���� ī��Ʈ �ʱ�ȭ
        }
    }
    public void TakeDamage(int damage)  // ���߿� ���� ����
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0; // ���� Die �߰�

        }
    }



}
