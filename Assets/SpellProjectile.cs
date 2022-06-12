using UnityEngine;

public class SpellProjectile : MonoBehaviour
{

    [SerializeField] private int Damage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private Vector3 explosionOffset;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private LayerMask HitableLayers;

    private void OnTriggerEnter(Collider other)
    {
        if (HitableLayers == (HitableLayers | (1 << other.gameObject.layer)))
        {

            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        CinemachineShake.Instance.ShakeCamera(2f, 0.2f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObjct in colliders)
        {
            Rigidbody rb = nearbyObjct.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position + explosionOffset, explosionRadius);
            }

            Damagable damagable = nearbyObjct.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.takeDamage(Damage);
            }
        }
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
