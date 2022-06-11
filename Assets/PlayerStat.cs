using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int MaxHealth;
    public int GemAmount;
    public int Health;
    public HealthBar healthBar;
    public int Mana;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    public void takeDamage(int damage)
    {
        Health -= damage;
        healthBar.UpdateHealth((float)Health / MaxHealth);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
