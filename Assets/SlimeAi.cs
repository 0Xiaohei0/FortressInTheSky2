using UnityEngine;

public class SlimeAi : MonoBehaviour
{
    [SerializeField] private Transform jumpDirection;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpInterVal;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(Jump), 4f, jumpInterVal);
    }

    private void Jump()
    {
        Vector3 JumpVector = jumpDirection.position - transform.position;
        JumpVector.x = 0;
        rb.AddForce(Vector3.Normalize(JumpVector) * jumpForce);
        Quaternion rotationUpright = Quaternion.LookRotation(rb.transform.forward, Vector3.up);
        GetComponent<Rigidbody>().MoveRotation(rotationUpright);
    }
}
