using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;

    Rigidbody myRB;
    Animator myAnim;

    bool facingRight;
    private PlayerCursor playerCursor;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        playerCursor = GetComponent<PlayerCursor>();
        facingRight = true;
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxisRaw("Horizontal");
        myAnim.SetFloat("speed", move * (facingRight ? 1 : -1));
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
