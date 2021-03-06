using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    private int maxHealth;
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

    public Image experienceBar;
    private float experience;
    public float maxExperience;
    private int level;
    public TextMeshProUGUI levelText;

    public int ManaRegenLevel;
    public int ManaRegenCost;
    public float ManaRegenAmountMultiplier;
    public float ManaRegenCostMultiplier;

    public Vector3 RespawnLocation;
    public float OriginalxCord;

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

    public float Experience
    {
        get => experience;
        set
        {
            experience = value;
            if (experience >= maxExperience)
            {
                experience -= maxExperience;
                Level++;
                MaxHealth += 5; MaxMana += 5;
            }
            experienceBar.fillAmount = experience / maxExperience;
        }
    }

    public int Level
    {
        get => level; set { level = value; levelText.text = "Lv." + value.ToString(); }
    }

    public int MaxHealth
    {
        get => maxHealth; set
        {
            maxHealth = value;
            Health = value;
        }
    }

    private void Awake()
    {
        Experience = 0;
        MaxHealth = 100;
        Level = 1;
        OriginalxCord = transform.position.x;
        RespawnLocation = transform.position;
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
            Respawn();
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

    public void Respawn()
    {
        transform.position = new Vector3(OriginalxCord, RespawnLocation.y, RespawnLocation.z);
        Health = maxHealth;
        Mana = MaxMana;
        gameObject.SetActive(true);
    }
}
