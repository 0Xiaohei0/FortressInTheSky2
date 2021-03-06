using UnityEngine;

public class HoverBotAI : Damagable
{

    Collider[] wallCollisions;
    [SerializeField] float wallCheckRadius;
    [SerializeField] float wallCheckRadiusIdle;
    [SerializeField] float wallCheckRadiusCombat;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheck;

    public Transform playerTransform;
    public Transform gunTransform;

    [Header("Fireing")]
    public Transform FirePoint1;
    public Transform FirePoint2;
    public Transform targetedPosition;
    private int NextFirePoint;
    public GameObject hoverTurretBullet;
    public float hoverTurretBulletSpeed;
    public float hoverTurretFireInterval;
    public bool hasTarget;



    private bool facingLeft = true;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        dropLootTarget = GameObject.FindWithTag("DropLootTracker");
        NextFirePoint = 1;
        InvokeRepeating(nameof(Fire), 0f, hoverTurretFireInterval);
    }

    // Update is called once per frame
    void Update()
    {
        wallCollisions = Physics.OverlapSphere(wallCheck.position, wallCheckRadius, wallLayer);
        hasTarget = wallCollisions.Length > 0;
        if (hasTarget)
        {

            wallCheckRadius = wallCheckRadiusCombat;
            playerTransform = wallCollisions[0].transform;
            gunTransform.LookAt(playerTransform);
            if (facingLeft)
                gunTransform.Rotate(new Vector3(0, 1, 0), -90);
            else
                gunTransform.Rotate(new Vector3(0, 1, 0), 90);
            if (playerTransform.position.z > transform.position.z && facingLeft) Flip();
            else if (playerTransform.position.z < transform.position.z && !facingLeft) Flip();
        }
        else
        {
            wallCheckRadius = wallCheckRadiusIdle;
        }
    }

    private void Fire()
    {
        if (hasTarget)
        {
            Transform FirePoint = NextFirePoint == 1 ? FirePoint1 : FirePoint2;
            GameObject createdBullet = Instantiate(hoverTurretBullet, FirePoint.position, FirePoint.rotation);
            NextFirePoint = NextFirePoint == 1 ? 2 : 1;
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * (playerTransform.position - FirePoint.transform.position).normalized);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
