using Cinemachine;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera bossCam;
    public BoxCollider bossTriggerHitbox;
    public GameObject HoverBoss;
    public GameObject HoverBossUI;
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        bossTriggerHitbox = GetComponent<BoxCollider>();
        bossCam.Priority = 9;
        HoverBossUI.SetActive(false);
        HoverBoss.SetActive(false);
    }
    private void Update()
    {
        if (Player.transform.position.z < transform.position.z)
        {
            bossCam.Priority = 9;
            HoverBossUI.SetActive(false);
            HoverBoss.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            bossTriggerHitbox.isTrigger = false;
            bossCam.Priority = 11;
            HoverBossUI.SetActive(true);
            HoverBoss.SetActive(true);
        }
    }
}
