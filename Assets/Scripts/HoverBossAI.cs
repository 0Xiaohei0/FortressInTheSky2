using UnityEngine;

public class HoverBossAI : Damagable
{
    public Transform FirePoint1;
    public Transform FirePoint2;
    private int NextFirePoint;
    public Transform playerTransform;
    public GameObject hoverBossBullet1;
    public GameObject hoverBossBullet2;
    public MeshRenderer hoverBossCore;
    public Material hoverBossEnrageMat;
    public Material hoverBossNormalMat;
    private Animator anim;

    private bool facingLeft = true;

    public Transform gunTransform;
    public Transform targetedPosition;

    [Header("Fireing1")]
    public float hoverTurretBulletSpeed;
    public float hoverTurretFireInterval;
    public bool hasTarget;
    public bool isFiringStage1;
    public bool isFiringStage2;
    public float Stage2FireInterval;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hoverBossCore.material = hoverBossNormalMat;
        currentHealth = maxHealth;
        dropLootTarget = GameObject.FindWithTag("DropLootTracker");
        NextFirePoint = 1;
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        gunTransform.LookAt(playerTransform);
        if (facingLeft)
            gunTransform.Rotate(new Vector3(0, 1, 0), -90);
        else
            gunTransform.Rotate(new Vector3(0, 1, 0), 90);
        if (playerTransform.position.z > transform.position.z && facingLeft) Flip();
        else if (playerTransform.position.z < transform.position.z && !facingLeft) Flip();

        //Debug.Log(currentHealth / maxHealth);
        if (((float)currentHealth / maxHealth) <= 0.5)
        {
            anim.SetBool("Stage2", true); ;
        }
    }

    public void Stage1Fire()
    {
        if (playerTransform != null)
        {
            Transform FirePoint = NextFirePoint == 1 ? FirePoint1 : FirePoint2;

            GameObject createdBullet = Instantiate(hoverBossBullet1, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * (playerTransform.position - FirePoint.transform.position).normalized);

            createdBullet = Instantiate(hoverBossBullet1, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;

            Vector3 Yoffset = new Vector3(0, 3, 0);
            Vector3 BulletDirection = (playerTransform.position - FirePoint.transform.position + Yoffset).normalized;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * BulletDirection);

            createdBullet = Instantiate(hoverBossBullet1, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * (playerTransform.position - FirePoint.transform.position - Yoffset).normalized);

            createdBullet = Instantiate(hoverBossBullet1, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * (playerTransform.position - FirePoint.transform.position + 2 * Yoffset).normalized);

            createdBullet = Instantiate(hoverBossBullet1, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * (playerTransform.position - FirePoint.transform.position - 2 * Yoffset).normalized);

            NextFirePoint = NextFirePoint == 1 ? 2 : 1;
        }
        if (isFiringStage1)
            Invoke(nameof(Stage1Fire), hoverTurretFireInterval);
    }


    public void Stage2Fire()
    {
        if (playerTransform != null)
        {
            Transform FirePoint = NextFirePoint == 1 ? FirePoint1 : FirePoint2;

            GameObject createdBullet = Instantiate(hoverBossBullet2, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * (playerTransform.position - FirePoint.transform.position).normalized);

            createdBullet = Instantiate(hoverBossBullet2, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;

            Vector3 Yoffset = new Vector3(0, 3, 0);
            Vector3 BulletDirection = (playerTransform.position - FirePoint.transform.position + Yoffset).normalized;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * BulletDirection);

            createdBullet = Instantiate(hoverBossBullet2, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * (playerTransform.position - FirePoint.transform.position - Yoffset).normalized);

            createdBullet = Instantiate(hoverBossBullet2, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * (playerTransform.position - FirePoint.transform.position + 2 * Yoffset).normalized);

            createdBullet = Instantiate(hoverBossBullet2, FirePoint.position, FirePoint.rotation);
            createdBullet.GetComponent<SpellProjectile>().Creator = targetedPosition;
            createdBullet.GetComponent<Rigidbody>().AddForce(hoverTurretBulletSpeed * (playerTransform.position - FirePoint.transform.position - 2 * Yoffset).normalized);

            NextFirePoint = NextFirePoint == 1 ? 2 : 1;
        }
        if (isFiringStage2)
            Invoke(nameof(Stage2Fire), Stage2FireInterval);
    }

    public void SetEnrageMat()
    {
        hoverBossCore.material = hoverBossEnrageMat;
    }
    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
