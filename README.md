Star 4th 4조 유니티 팀 프로젝트
쿠키런 형식의 게임 제작
x로 점프, c키로 슬라이딩(pc)
버튼 조작 (모바일)
최대한 점수를 많이 얻는 것이 목표인 쿠키런 형식의 게임입니다.

-점프와 슬라이드

        void Jump()
    {
    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
    jumpCount++;
    isGrounded = false;

    sfxController?.PlayJumpSFX();
    }
----------------------------------------------------------------------
    void Slide()
    {
    if (!isSlide && isGrounded)
    {
        isSlide = true;
        animator.SetTrigger("isSlide");
        playerCollider.size = sliderColliderSize;
        playerCollider.offset = sliderColliderOffset;

        sfxController?.PlaySlideSFX();
    }

    }


-플레이어가 장애물에 부딪히면 Hp가 감소하고 일시적으로 무적상태가 됩니다.

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
------------------------------------------------------------------------------
    IEnumerator OnHitRoutine()
    {
    isHit = true;
    isInvincible = true;
    sfxController?.PlayHitSFX();
    animator.SetBool("isHit", true);
    yield return new WaitForSeconds(hitAnimeDuration);
    animator.SetBool("isHit", false);
    isHit = false;
    yield return new WaitForSeconds(invincibleDuration - hitAnimeDuration); // anim 시간 이후 남은 무적시간 지속
    isInvincible = false;
    }

-플레이어의 Hp가 0이 되면 사망하며 결과창으로 이동합니다.

     public void Die()
    {
     if (isDie) return;
     isDie = true;
     currentHp = 0;
     _rigidbody2D.velocity = Vector2.zero;

     Debug.Log("Player Die");
     animator.SetTrigger("isDie");
     _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce * 1.5f);
     //애니메이션 추가 예정? 혹은 바로 결과창?
     sfxController?.PlayDieSFX();
     GameManager.instance.EndGame();
    }
