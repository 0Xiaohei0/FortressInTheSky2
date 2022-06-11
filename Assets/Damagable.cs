using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int maxHealth;
    public HealthBar healthBar;
    public int currentHealth;

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
        }
    }
}
