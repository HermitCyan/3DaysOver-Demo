using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    public float walkSpeed = 2f;    // 走路速度
    public float runSpeed = 5f;     // 跑步速度
    public float jumpForce = 10f;  // 跳跃力量

    private Rigidbody2D rb;
    private bool canJump = true;   // 控制是否可以跳跃

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 获取刚体组件
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float move = Input.GetAxis("Horizontal");
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed; // 判断是否按下跑步键

        // 设置水平速度
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    void HandleJump()
    {
        // 按下跳跃键
        if (Input.GetButtonDown("Jump"))
        {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 设置垂直速度实现跳跃
                canJump = false; // 设置为不允许再次跳跃
            }
        }
    }

  
    
