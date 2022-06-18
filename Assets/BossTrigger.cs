using Cinemachine;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera bossCam;
    public BoxCollider bossTriggerHitbox;
    public GameObject HoverBoss;
    public GameObject HoverBossUI;

    private void Start()
    {
        bossTriggerHitbox = GetComponent<BoxCollider>();
        bossCam.Priority = 9;
        HoverBossUI.SetActive(false);
        HoverBoss.SetActive(false);
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
