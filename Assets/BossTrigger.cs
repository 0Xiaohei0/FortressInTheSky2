using Cinemachine;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera bossCam;
    public BoxCollider bossTriggerHitbox;
    public GameObject HoverBossPrefab;
    public GameObject HoverBoss;
    public GameObject HoverBossUI;
    public HealthBar HoverBossHealthBar;
    public GameObject Player;
    public Transform hoverBotParent;
    private GameOverManager gameOverManager;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        bossTriggerHitbox = GetComponent<BoxCollider>();
        bossCam.Priority = 9;
        HoverBossUI.SetActive(false);
        HoverBoss.SetActive(false);
        gameOverManager = GameObject.FindWithTag("GameOverManager").GetComponent<GameOverManager>();
    }
    private void Update()
    {
        if (Player.transform.position.z < transform.position.z)
        {
            bossCam.Priority = 9;
            gameOverManager.ClearPorjectiles();
            HoverBossUI.SetActive(false);
            if (HoverBoss != null)
                Destroy(HoverBoss);
            bossTriggerHitbox.isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            bossTriggerHitbox.isTrigger = false;
            bossCam.Priority = 11;
            HoverBossUI.SetActive(true);
            if (HoverBoss != null)
                HoverBoss.SetActive(true);
            else
            {
                HoverBoss = Instantiate(HoverBossPrefab, hoverBotParent.position, hoverBotParent.rotation);
                HoverBoss.transform.parent = hoverBotParent.transform;
                HoverBoss.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                HoverBoss.GetComponent<HoverBossAI>().healthBar = HoverBossHealthBar;
            }
        }
    }
}
