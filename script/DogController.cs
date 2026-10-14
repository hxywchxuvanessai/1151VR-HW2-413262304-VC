
using UnityEngine;
using UnityEngine.InputSystem;

public class DogController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;

    // 低於這個高度就重生
    public float fallLimit = -6f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float move;
    private bool jumpRequested;
    private bool isGrounded;

    private Vector2 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 記錄小狗的起始位置
        startPosition = rb.position;
    }

    void Update()
    {
        move = 0f;

        if (Keyboard.current == null)
            return;

        // 往左移動
        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            move = -1f;
        }

        // 往右移動
        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            move = 1f;
        }

        // 按 Space 跳躍
        if (Keyboard.current.spaceKey.wasPressedThisFrame
            && isGrounded)
        {
            jumpRequested = true;
        }

        // 根據移動方向翻轉小狗
        if (move < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (move > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        // 掉下平台時重生
        if (rb.position.y < fallLimit)
        {
            Respawn();
            return;
        }

        Vector2 velocity = rb.linearVelocity;

        // 左右移動
        velocity.x = move * speed;

        // 跳躍
        if (jumpRequested)
        {
            velocity.y = jumpForce;
            jumpRequested = false;
            isGrounded = false;
        }

        rb.linearVelocity = velocity;
    }

    // 重生功能
    void Respawn()
    {
        Debug.Log("小狗掉下平台，回到起點！");

        rb.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        move = 0f;
        jumpRequested = false;
        isGrounded = false;
    }

    // 確認小狗站在地板上
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                break;
            }
        }
    }

    // 離開地板
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
