using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int MaxHealth;
    public int GemAmount;
    private int health;
    public HealthBar healthBar;
    public HealthBar manaBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public float MaxMana;
    private float mana;
    public float ManaRegenInterval;
    public float ManaRegenAmount;

    public int ManaRegenLevel;
    public int ManaRegenCost;
    public float ManaRegenAmountMultiplier;
    public float ManaRegenCostMultiplier;

    public int Health
    {
        get => health; set
        {
            health = Mathf.Clamp(value, 0, MaxHealth); healthBar.UpdateHealth((float)Health / MaxHealth);
            healthText.text = Mathf.Round(Health).ToString() + "/" + MaxHealth.ToString();
        }
    }
    public float Mana
    {
        get => mana; set
        {
            mana = Mathf.Clamp(value, 0, MaxMana); manaText.text = Mathf.Round(Mana).ToString() + "/" + MaxMana.ToString();
            manaBar.UpdateHealth((float)Mana / MaxMana);
        }
    }

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

        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void useMana(int mana)
    {
        Mana -= mana;

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
