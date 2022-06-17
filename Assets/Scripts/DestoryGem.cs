using UnityEngine;

public class DestoryGem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStat>().GemAmount++;
            Destroy(gameObject);
        }
    }
}
