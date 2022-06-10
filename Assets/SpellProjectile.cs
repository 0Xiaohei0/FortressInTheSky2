using UnityEngine;

public class SpellProjectile : MonoBehaviour
{

    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private LayerMask HitableLayers;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");

        if (HitableLayers == (HitableLayers | (1 << other.gameObject.layer)))
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObjct in colliders)
        {
            Rigidbody rb = nearbyObjct.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
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
