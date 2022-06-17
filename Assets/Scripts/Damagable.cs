using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int maxHealth;
    public HealthBar healthBar;
    public int currentHealth;
    public GameObject dropLootPrefab;
    public GameObject dropLootTarget;


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
                Vector3 Offset = new(0, Random.Range(1f, 2f), Random.Range(1f, 2f));
                var loot = Instantiate(dropLootPrefab, transform.position + Offset, Quaternion.identity);
                loot.GetComponent<Follow>().target = dropLootTarget.transform;
            }
        }
    }
}
