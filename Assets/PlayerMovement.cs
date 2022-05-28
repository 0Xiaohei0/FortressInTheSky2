using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 1f;

    private Animator anim;
    private Rigidbody rigidBody;
    private bool isMoving = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
    }
}
