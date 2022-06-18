using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private bool inputShield;
    [SerializeField] private GameObject shield;
    [SerializeField] private float manaCost;
    [SerializeField] private bool inputShieldLastFrame;
    private PlayerStat playerStat;

    private void Start()
    {
        playerStat = GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        inputShield = Input.GetKey(KeyCode.LeftShift);
        shield.SetActive(inputShield);

        if (playerStat != null) // use mana
        {
            if (inputShield)
            {
                playerStat.Mana -= manaCost * Time.deltaTime;
            }
        }

        inputShieldLastFrame = inputShield;
    }
}
