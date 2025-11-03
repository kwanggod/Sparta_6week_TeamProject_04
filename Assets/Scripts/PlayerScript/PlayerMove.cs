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
    public bool isDie;
    public int jumpCount = 0;
    public int maxJumpCount = 2;
    public float jumpForce = 10f;
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
    public bool isHit = false;
    public bool isInvincible = false; // 무적
    public float invincibleDuration = 1f; // 무적 지속시간
    public float invincibleTime = 0f;
    public float hitAnimeDuration = 0.8f; // hit애니메이션 지속시간
    private Coroutine invCoroutine = null;


    //public LayerMask groundLayer; // 레이어 설정
    //public float groundCheckDistance = 0.1f; // raycast 길이 
    //public Vector2 groundCheckOffset = new Vector2(0f, -0.6f); // 레이저 쏘는 위치 n만큼 내림

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = playerCollider.size;
        originalColliderOffset = playerCollider.offset;
        currentHp = maxHp;
        isDie = false;
    }

    // Update is called once per frame
    void Update()
    {
        //GroundCheck();

        if (maxJumpCount > jumpCount && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.X)) && !isDie) // 버튼 이름에 맞게 변경
        {
            Jump();
        }

        if (!isSlide && Input.GetKey(KeyCode.C) && !isDie) // || Input.GetButtonDown("Slide") 추후 슬라이드 버튼 추가하면 같이 추가
        {
            Slide();
        }

        if (isSlide && Input.GetKeyUp(KeyCode.C) && !isDie)
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
        animator.SetBool("isDie", isDie);

        tickTimer += Time.deltaTime;
        if (tickTimer >= tickRate)
        {
            tickTimer = 0f;
            TakeDamage(tickDamage);
        }

        if (currentHp <= 0)
        {
            Die();
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
        if (!isSlide && isGrounded)
        {
            isSlide = true;
            animator.SetTrigger("isSlide");
            playerCollider.size = sliderColliderSize;
            playerCollider.offset = sliderColliderOffset;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // 태그 Ground 로
        {
            Vector3 vector3 = _rigidbody2D.velocity;   // 겹치기 무한점프 방지
            if (vector3 == Vector3.zero)
            {
                isGrounded = true;
                jumpCount = 0; // 착지 후 카운트 초기화
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (isInvincible || isHit)
        {
            Debug.Log("무적");
            return;
        }

        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
            return;

        }

        if (invCoroutine != null)
        {
            StopCoroutine(invCoroutine);

        }

        if (damage > tickDamage) // 틱뎀에 의한 무적발생 방지
        {
            invCoroutine = StartCoroutine(OnHitRoutine());
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
        if (isDie) return;
        isDie = true;
        currentHp = 0;
        _rigidbody2D.velocity = Vector2.zero;

        Debug.Log("Player Die");
        animator.SetTrigger("isDie");
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce * 1.5f);
        GameManager.instance.GroundStop();
        //애니메이션 추가 예정? 혹은 바로 결과창?

        GameManager.instance.EndGame();
    }

    private IEnumerator GotoResultScene()
    {
        GameManager.instance.EndGame();

        // 🔹 2. 0.5초 정도 기다린 후 결과 씬으로 이동
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }

    public void TryJump() //모바일 버튼 용 메써드
    {
        if (maxJumpCount > jumpCount && isGrounded && !isDie)
        {
            Jump();
        }
    }

    public void TrySlide()
    {
        if (!isSlide && isGrounded && !isDie)
        {
            Slide();
        }

    }
    IEnumerator OnHitRoutine()
    {
        isHit = true;
        isInvincible = true;
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(hitAnimeDuration);
        animator.SetBool("isHit", false);
        isHit = false;
        yield return new WaitForSeconds(invincibleDuration - hitAnimeDuration); // anim 시간 이후 남은 무적시간 지속
        isInvincible = false;
    }

}
