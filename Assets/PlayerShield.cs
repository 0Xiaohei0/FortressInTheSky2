using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private bool inputShield;
    [SerializeField] private GameObject shield;
    [SerializeField] private float manaCost;
    [SerializeField] private bool inputShieldLastFrame;
    private PlayerStat playerStat;
    [SerializeField] float innerCounterRadius;
    [SerializeField] float outerCounterRadius;
    [SerializeField] float lastInputTime;
    [SerializeField] float counterWindow;
    [SerializeField] float damageMultiplier;

    Collider[] shieldCollisions;
    [SerializeField] LayerMask shieldCheckLayers;


    private void Start()
    {
        playerStat = GetComponent<PlayerStat>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(shield.transform.position, innerCounterRadius);
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(shield.transform.position, outerCounterRadius);
    }

    // Update is called once per frame
    void Update()
    {
        inputShield = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            lastInputTime = Time.time;
        }
        shield.SetActive(inputShield);

        if (playerStat != null) // use mana
        {
            if (inputShield)
            {
                playerStat.Mana -= manaCost * Time.deltaTime;
            }
        }

        if (inputShield)
        {
            shieldCollisions = Physics.OverlapSphere(shield.transform.position, outerCounterRadius, shieldCheckLayers);
            if (shieldCollisions.Length > 0)
            {
                if (Time.time - lastInputTime <= counterWindow)
                {
                    foreach (Collider collider in shieldCollisions)
                    {
                        if (collider.tag == "EnemyBullet")
                        {
                            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
                            SpellProjectile sp = collider.gameObject.GetComponent<SpellProjectile>();
                            if (sp.Creator != null)
                            {
                                sp.setTracking(sp.Creator);
                            }
                            else
                            {
                                rb.velocity = -rb.velocity;
                            }
                            collider.gameObject.tag = "DeflectedBullet";
                            sp.Damage = (int)(sp.Damage * damageMultiplier);
                        }
                    }
                }
                else
                {
                    foreach (Collider collider in shieldCollisions)
                    {
                        if (collider.tag == "EnemyBullet")
                        {
                            SpellProjectile spellProjectile = collider.gameObject.GetComponent<SpellProjectile>();
                            spellProjectile.doDamageToPlayer = false;
                            spellProjectile.Explode();
                        }
                    }
                }
            }
        }
        inputShieldLastFrame = inputShield;
    }
}
