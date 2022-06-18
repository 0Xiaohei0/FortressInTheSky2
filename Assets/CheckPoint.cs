using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Material inActiveMaterial;
    public Material activeMaterial;
    public MeshRenderer coreColor;
    public GameObject CheckPointReachedUI;
    public float messageDuration;

    private void Awake()
    {
        coreColor.material = inActiveMaterial;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerStat>().RespawnLocation = transform.position;
            CheckPointReachedUI.SetActive(true);
            coreColor.material = activeMaterial;
            Invoke(nameof(DisableCheckPointReachedUI), messageDuration);
        }
    }

    private void DisableCheckPointReachedUI()
    {
        CheckPointReachedUI.SetActive(false);
    }
}
