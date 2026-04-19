using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    // 무적
    public bool isInvincible = false;
    public float invincibleTime = 3f;

    // 속도 증가
    public float speedBoostMultiplier = 2f;
    public float speedBoostTime = 3f;
    private bool isSpeedBoosted = false;

    // 점프 증가
    public float jumpBoostMultiplier = 1.5f;
    public float jumpBoostTime = 3f;
    private bool isJumpBoosted = false;

    private Rigidbody2D rb;
    private Animator pAni;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private float moveInput;

    private Coroutine invincibleCoroutine;
    private Coroutine speedCoroutine;
    private Coroutine jumpCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        pAni.SetBool("isMove", moveInput != 0);
        pAni.SetBool("isGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        if (isSpeedBoosted)
            currentSpeed *= speedBoostMultiplier;

        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }

        if (collision.CompareTag("Respawn"))
        {
            if (!isInvincible)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            float currentJump = jumpForce;

            if (isJumpBoosted)
                currentJump *= jumpBoostMultiplier;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * currentJump, ForceMode2D.Impulse);
        }
    }

    // =========================
    // 무적
    // =========================
    public void ActivateInvincibility()
    {
        if (invincibleCoroutine != null)
            StopCoroutine(invincibleCoroutine);

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine());
    }

    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;

        float timer = 0f;

        while (timer < invincibleTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);

            timer += 0.2f;
        }

        isInvincible = false;
        spriteRenderer.color = Color.white;
    }

    // =========================
    // 속도 증가
    // =========================
    public void ActivateSpeedBoost()
    {
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        speedCoroutine = StartCoroutine(SpeedBoostCoroutine());
    }

    IEnumerator SpeedBoostCoroutine()
    {
        isSpeedBoosted = true;

        float timer = 0f;

        while (timer < speedBoostTime)
        {
            spriteRenderer.color = Color.yellow;
            timer += Time.deltaTime;
            yield return null;
        }

        isSpeedBoosted = false;
        spriteRenderer.color = Color.white;
    }

    // =========================
    // 점프 증가
    // =========================
    public void ActivateJumpBoost()
    {
        if (jumpCoroutine != null)
            StopCoroutine(jumpCoroutine);

        jumpCoroutine = StartCoroutine(JumpBoostCoroutine());
    }

    IEnumerator JumpBoostCoroutine()
    {
        isJumpBoosted = true;

        float timer = 0f;

        while (timer < jumpBoostTime)
        {
            spriteRenderer.color = Color.green;

            timer += Time.deltaTime;
            yield return null;
        }

        isJumpBoosted = false;
        spriteRenderer.color = Color.white;
    }
}