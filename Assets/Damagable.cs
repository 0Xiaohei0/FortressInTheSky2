using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int maxHealth;
    public HealthBar healthBar;
    public int currentHealth;
    public GameObject dropLootPrefab;
    public GameObject dropLootTarget;

    private void Start()
    {
        dropLootTarget = GameObject.FindGameObjectWithTag("DropLootTracker");
    }

    public void takeDamage(int damage)
    {
        if (!healthBar.gameObject.activeInHierarchy)
        {
            healthBar.gameObject.SetActive(true);
        }
        currentHealth -= damage;
        healthBar.UpdateHealth((float)currentHealth / maxHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            for (int i = 0; i < maxHealth / 10; i++)
            {
                Vector3 verticalOffset = new(0, Random.Range(1f, 2f));
                Debug.Log(verticalOffset);
                var loot = Instantiate(dropLootPrefab, transform.position + verticalOffset, Quaternion.identity);
                loot.GetComponent<Follow>().target = dropLootTarget.transform;
            }
        }
    }
}
