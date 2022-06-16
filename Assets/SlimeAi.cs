using UnityEngine;

public class SlimeAi : Damagable
{
    [Header("Jump")]
    [SerializeField] float jumpVar;
    [SerializeField] private Transform jumpDirection;
    [SerializeField] private float jumpForce;
    [SerializeField] float jumpInterVal;
    private Rigidbody rb;

    Collider[] wallCollisions;
    [SerializeField] float wallCheckRadius;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheck;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();

        float randomTime = Random.Range(jumpInterVal - jumpVar, jumpInterVal + jumpVar);
        Invoke(nameof(Jump), randomTime);

        dropLootTarget = GameObject.FindWithTag("DropLootTracker");
        Debug.Log(dropLootTarget);
    }
    private void Update()
    {
        wallCollisions = Physics.OverlapSphere(wallCheck.position, wallCheckRadius, wallLayer);
        if (wallCollisions.Length > 0) FlipRotate();
    }
    private void Jump()
    {
        Vector3 JumpVector = jumpDirection.position - transform.position;
        JumpVector.x = 0;
        rb.AddForce(Vector3.Normalize(JumpVector) * jumpForce);
        Quaternion rotationUpright = Quaternion.LookRotation(rb.transform.forward, Vector3.up);
        GetComponent<Rigidbody>().MoveRotation(rotationUpright);

        float randomTime = Random.Range(jumpInterVal - jumpVar, jumpInterVal + jumpVar);
        Invoke(nameof(Jump), randomTime);
    }
    void FlipRotate()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        transform.rotation = Quaternion.Euler(rot);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
    }
}
