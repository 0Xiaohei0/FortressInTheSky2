using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gemAmountUI;
    [SerializeField] Button ManaRegenUpgradeButton;
    [SerializeField] TextMeshProUGUI ManaRegenValueUI;
    [SerializeField] TextMeshProUGUI ManaRegenCostUI;
    private PlayerStat playerStat;
    // Start is called before the first frame update
    void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        gemAmountUI.text = playerStat.GemAmount.ToString();
        ManaRegenUpgradeButton.interactable = playerStat.GemAmount >= playerStat.getManaRegenCost();
        ManaRegenValueUI.text = Mathf.RoundToInt(playerStat.getManaRegenSpeed()).ToString() + "/s";
        ManaRegenCostUI.text = Mathf.RoundToInt(playerStat.getManaRegenCost()).ToString();
    }
}
