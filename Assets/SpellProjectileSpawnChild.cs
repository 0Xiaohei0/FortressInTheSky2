using UnityEngine;

public class SpellProjectileSpawnChild : MonoBehaviour
{
    public float SpawnInterval;
    public GameObject ChildPrefab;
    public float childSpeed;
    private SpellProjectile sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpellProjectile>();
        InvokeRepeating(nameof(SpawnChild), 0f, SpawnInterval);
    }

    public void SpawnChild()
    {
        GameObject createdBullet = Instantiate(ChildPrefab, transform.position, transform.rotation);
        createdBullet.GetComponent<SpellProjectile>().Creator = sp.Creator;
        createdBullet.GetComponent<Rigidbody>().AddForce(childSpeed * transform.up);

        createdBullet = Instantiate(ChildPrefab, transform.position, transform.rotation);
        createdBullet.GetComponent<SpellProjectile>().Creator = sp.Creator;
        createdBullet.GetComponent<Rigidbody>().AddForce(childSpeed * -transform.up);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
