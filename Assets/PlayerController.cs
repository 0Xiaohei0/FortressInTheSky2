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

    bool facingRight;
    private PlayerCursor playerCursor;
    private bool canDoubleJump;
    [SerializeField] private GameObject doubleJumpEffect;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        playerCursor = GetComponent<PlayerCursor>();
        facingRight = true;
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
                grounded = false;
                myRB.velocity = Vector3.up * jumpHeight;
                //myAnim.SetBool("jump", true);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (canDoubleJump)
                    {
                        grounded = false;
                        myRB.velocity = Vector3.up * jumpHeight;
                        Instantiate(doubleJumpEffect, groundCheck.position, groundCheck.rotation);
                        //myAnim.SetBool("jump", true);
                        canDoubleJump = false;
                    }
                }
            }

        }
        float move = Input.GetAxisRaw("Horizontal");
        myAnim.SetFloat("speed", move * (facingRight ? 1 : -1));
        //myAnim.SetFloat("verticalSpeed", myRB.velocity.y);
        //myAnim.SetBool("grounded", grounded);
        myAnim.SetBool("isWalking", Mathf.Abs(move) > 0);

        myRB.velocity = new Vector3(0, myRB.velocity.y, move * runSpeed);

        if (playerCursor.MousePosition.z > transform.position.z && !facingRight) Flip();
        else if (playerCursor.MousePosition.z < transform.position.z && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }
}
