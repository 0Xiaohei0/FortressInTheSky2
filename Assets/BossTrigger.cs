using Cinemachine;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera bossCam;
    public BoxCollider bossTriggerHitbox;
    public GameObject HoverBoss;

    private void Start()
    {
        bossTriggerHitbox = GetComponent<BoxCollider>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            bossTriggerHitbox.isTrigger = false;
            bossCam.Priority = 11;
            HoverBoss.SetActive(true);
        }
    }
}
