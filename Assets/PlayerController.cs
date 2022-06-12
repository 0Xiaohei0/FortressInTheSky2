using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;

    Rigidbody myRB;
    Animator myAnim;

    Collider[] groundCollisions;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float jumpHeight;
    [SerializeField] bool grounded;
    private PlayerStat playerStat;

    public bool facingRight;
    private PlayerCursor playerCursor;
    private bool canDoubleJump;
    [SerializeField] private GameObject doubleJumpEffect;
    [SerializeField] private int doubleJumpManaCost;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        playerCursor = GetComponent<PlayerCursor>();
        playerStat = GetComponent<PlayerStat>();
        facingRight = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
    private void Update()
    {
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;

        if (grounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded)
            {
                HandleJump();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (canDoubleJump && playerStat.Mana >= doubleJumpManaCost)
                    {
                        playerStat.useMana(doubleJumpManaCost);
                        HandleJump();
                        Instantiate(doubleJumpEffect, groundCheck.position, groundCheck.rotation);
                        canDoubleJump = false;
                    }
                }
            }

        }
        float move = Input.GetAxisRaw("Horizontal");
        float moveSmooth = Input.GetAxis("Horizontal");

        //myAnim.SetFloat("speed", move * (facingRight ? 1 : -1));
        myAnim.SetFloat("velocity", moveSmooth * (facingRight ? 1 : -1) + 1);
        //myAnim.SetFloat("verticalSpeed", myRB.velocity.y);
        myAnim.SetBool("grounded", grounded);
        //myAnim.SetBool("isWalking", Mathf.Abs(move) > 0);

        myRB.velocity = new Vector3(0, myRB.velocity.y, move * runSpeed);

        if (playerCursor.MousePosition.z > transform.position.z && !facingRight) Flip();
        else if (playerCursor.MousePosition.z < transform.position.z && facingRight) Flip();
    }

    private void HandleJump()
    {
        grounded = false;
        myRB.velocity = Vector3.up * jumpHeight;
        myAnim.SetBool("jump", true);
        Invoke(nameof(SetAnimJumpToFalse), 0.1f);
    }

    private void SetAnimJumpToFalse()
    {
        myAnim.SetBool("jump", false);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }
}
