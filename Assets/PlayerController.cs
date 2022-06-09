using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;

    Rigidbody myRB;
    Animator myAnim;

    bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float move = Input.GetAxisRaw("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector3(0, myRB.velocity.y, move * runSpeed);

        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }
}
