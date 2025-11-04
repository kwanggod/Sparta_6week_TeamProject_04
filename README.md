# 🍪Star 4th 4조 유니티 팀 프로젝트
---
- 쿠키런 형식의 게임 제작<br>
- x로 점프, c키로 슬라이딩(pc)<br>
- 버튼 조작 (모바일)<br>

최대한 점수를 많이 얻는 것이 목표인 쿠키런 형식의 게임입니다.<br><br>

---
## 💁‍♂️ 프로젝트 팀원 및 역할
|**팀원**| <img width="48" height="48" alt="백광렬" src="https://github.com/user-attachments/assets/697c47d9-5a48-4442-a758-0348c529998f" /> <br>(팀장)백광렬 |<img width="48" height="48" alt="1-35-63-403" src="https://github.com/user-attachments/assets/c81f119b-0c80-4bef-91c6-94313bb06b36" /> <br>**오정훈**| <img width="48" height="48" alt="강동현" src="https://github.com/user-attachments/assets/4396254c-cbcd-448d-90a1-9e9950aa6701" /> <br>강동현|<img width="48" height="48" alt="이하람" src="https://github.com/user-attachments/assets/4fcca52b-1165-4b1f-a9c8-4aea4d4c4d76" /> <br>이하람 | <img width="48" height="48" alt="임성규" src="https://github.com/user-attachments/assets/af575d83-3d47-4545-9f81-b5c9d39bc287" /> <br>임성규|
|:---:|:---:|:---:|:---:|:---:|:---:|
|**역할**|맴 디자인<br> 아이템| 사운드 | 발표, UI| 기획, 스테이지<br>시작화면|캐릭터(플레이어)|
<br>

---

## 🎮 게임 소개
|게임 시연|
|:---:|
|![게임시연영상](https://github.com/user-attachments/assets/b11c516a-42d3-4d9a-bb8e-e3d55b6e3560)|

- 장애물을 피하고 아이템을 먹으며 점수 획득
- 슬라이드, 점프와 같은 액션 가능
- 일정 시간마다 체력이 닳으며 체력이 없으면 점수와 같이 결과창 출력
- 
<br>

---

## 🧩 주요 기능 코드 
<br>

### 점프와 슬라이드 

<br>

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

<br>

|점프|슬라이드|
|:---:|:---:|
|![Jump](https://github.com/user-attachments/assets/989d2c2f-3081-4914-a822-58cd614ea2df)| ![Slide](https://github.com/user-attachments/assets/0da77af1-f1c0-42b6-a9a4-b249c314da1e) |

<br>

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
