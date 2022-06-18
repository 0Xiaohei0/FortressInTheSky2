using UnityEngine;

public class SpellProjectile : MonoBehaviour
{

    [SerializeField] public int Damage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private Vector3 explosionOffset;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private LayerMask HitableLayers;
    [SerializeField] private LayerMask ExplosionHitableLayers;
    [SerializeField] public bool doDamageToPlayer;
    [SerializeField] public Vector3 StartingPosition;
    [SerializeField] public float AutoDestoryDistance;

    [Header("Counter Tracking")]
    [SerializeField] public Transform Creator;
    [SerializeField] public bool isTracking;
    [SerializeField] public Transform trackingTarget;
    private Rigidbody rb;
    public float speed;
    public float rotateSpeed = 200f;

    public GameObject damageText;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartingPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (HitableLayers == (HitableLayers | (1 << other.gameObject.layer)))
        {
            Explode();
        }
    }
    private void Update()
    {
        if ((transform.position - StartingPosition).magnitude >= AutoDestoryDistance)
        {
            Destroy(gameObject);
        }
    }

    public void setTracking(Transform target)
    {
        trackingTarget = target;
        isTracking = true;
    }

    void FixedUpdate()
    {
        if (isTracking && trackingTarget != null)
        {
            rb.velocity = rb.velocity.magnitude * (Creator.position - rb.transform.position).normalized;
        }
    }

    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        CinemachineShake.Instance.ShakeCamera(2f, 0.2f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        if (gameObject.tag == "DeflectedBullet")
            Debug.Log("Exploding" + gameObject.tag);
        foreach (Collider nearbyObjct in colliders)
        {
            if (ExplosionHitableLayers == (ExplosionHitableLayers | (1 << nearbyObjct.gameObject.layer)))
            {
                Rigidbody rb = nearbyObjct.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position + explosionOffset, explosionRadius);
                }
            }

            if (HitableLayers == (HitableLayers | (1 << nearbyObjct.gameObject.layer)))
            {
                Damagable damagable = nearbyObjct.GetComponent<Damagable>();
                if (damagable != null)
                {
                    damagable.takeDamage(Damage);
                    if (damageText != null)
                    {
                        DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
                        indicator.SetDamageText(Damage);
                    }
                }

                if (doDamageToPlayer)
                {
                    PlayerStat playerStat = nearbyObjct.GetComponent<PlayerStat>();
                    if (playerStat != null)
                    {
                        playerStat.takeDamage(Damage);
                    }
                }
            }
        }
        if (gameObject.tag == "DeflectedBullet")
            Debug.Log("Destorying" + gameObject.tag);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
