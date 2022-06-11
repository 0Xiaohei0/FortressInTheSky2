using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gemAmountUI;
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
    }
}
