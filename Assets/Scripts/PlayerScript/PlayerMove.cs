using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public bool isGrounded;
    public bool isSlide;
    public int jumpCount = 0;
    public int maxJumpCount = 2;
    public float jumpForce = 10f;
    public float slideDuration = 1f;
    private float slideTimer = 0f;
    private Rigidbody2D _rigidbody2D;
    public int maxHp = 100;
    public int currentHp;
    public float tickRate = 1f;
    public int tickDamage = 1;
    private float tickTimer = 0f;
    private BoxCollider2D playerCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    public Vector2 sliderColliderSize = new Vector2(1f, 0.8f);
    public Vector2 sliderColliderOffset = new Vector2(0f, -0.4f);
    public float dieOffsetY = -15f;

    public LayerMask groundLayer; // 레이어 설정
    public float groundCheckDistance = 0.1f; // raycast 길이 
    public Vector2 groundCheckOffset = new Vector2(0f, -0.6f); // 레이저 쏘는 위치 n만큼 내림

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = playerCollider.size;
        originalColliderOffset = playerCollider.offset;
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();

        if (maxJumpCount > jumpCount && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.X))) // 버튼 이름에 맞게 변경
        {
            Jump();
        }

        if (!isSlide && Input.GetKey(KeyCode.C)) // || Input.GetButtonDown("Slide") 추후 슬라이드 버튼 추가하면 같이 추가
        {
            Slide();
        }

        if (isSlide && Input.GetKeyUp(KeyCode.C))
        {
            isSlide = false;
            playerCollider.size = originalColliderSize;
            playerCollider.offset = originalColliderOffset;
        }

        if (transform.position.y < dieOffsetY)
        {
            Die();
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
            playerCollider.size = sliderColliderSize;
            playerCollider.offset = sliderColliderOffset;
        }

    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // 태그 Ground 로
        {
            isGrounded = true;
            jumpCount = 0; // 착지 후 카운트 초기화
        }
    }*/
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0;

        }
    }
    public void Heal(int heal)
    {
        currentHp += heal;
        currentHp = Mathf.Min(currentHp, maxHp);
        Debug.Log("체력 회복됨: " + currentHp);
    }

    public void Die()
    {
        currentHp = 0;
        _rigidbody2D.velocity = Vector2.zero;
        Debug.Log("Player Die");
        //애니메이션 추가 예정? 혹은 바로 결과창?
    }
    public void TryJump() //모바일 버튼 용 메써드
    {
        if (maxJumpCount > jumpCount && isGrounded)
        {
            Jump();
        }
    }

    public void TrySlide()
    {
        if (!isSlide && isGrounded)
        {
            Slide();
        }

    }

    void GroundCheck() // raycast 발사하여 그라운드 체크  2층 무한점프 방지용 코드
    {

        float halfHeight = playerCollider.bounds.extents.y;
        float halfwidth = playerCollider.bounds.extents.x * 0.9f;
        Bounds bounds = playerCollider.bounds;
        Vector2 center = new Vector2(bounds.center.x, bounds.min.y) + groundCheckOffset;
        Vector2 leftRayPoint = center + Vector2.left * halfwidth;
        Vector2 rightRayPoint = center + Vector2.right * halfwidth;

        RaycastHit2D leftHit = Physics2D.Raycast(leftRayPoint, Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRayPoint, Vector2.down, groundCheckDistance, groundLayer);

        bool wasGrounded = isGrounded;
        isGrounded = (leftHit.collider != null) || (rightHit.collider != null);


        if(isGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }

        Debug.DrawRay(leftRayPoint, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
        Debug.DrawRay(rightRayPoint, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
}
