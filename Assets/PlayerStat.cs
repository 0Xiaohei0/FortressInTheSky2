using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int MaxHealth;
    public int GemAmount;
    public int Health;
    public HealthBar healthBar;
    public HealthBar manaBar;
    public float MaxMana;
    public float Mana;
    public float ManaRegenInterval;
    public float ManaRegenAmount;

    public int ManaRegenLevel;
    public int ManaRegenCost;
    public float ManaRegenAmountMultiplier;
    public float ManaRegenCostMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        Mana = MaxMana;
        InvokeRepeating(nameof(RegenMana), 0f, ManaRegenInterval);
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

    public void useMana(int mana)
    {
        Mana -= mana;
        manaBar.UpdateHealth((float)Mana / MaxMana);
    }
    public void RegenMana()
    {
        Mana += ManaRegenAmount;
        Mana = Mathf.Min(MaxMana, Mana);
        manaBar.UpdateHealth((float)Mana / MaxMana);
    }

    public void manaRegenLevelUp()
    {
        GemAmount -= getManaRegenCost();
        ManaRegenLevel++;
        ManaRegenAmount = ManaRegenAmount * Mathf.Pow(ManaRegenAmountMultiplier, ManaRegenLevel);
    }

    public float getManaRegenSpeed()
    {
        return ManaRegenAmount / ManaRegenInterval;
    }
    public int getManaRegenCost()
    {
        return Mathf.RoundToInt(ManaRegenCost * Mathf.Pow(ManaRegenCostMultiplier, ManaRegenLevel));
    }
}
