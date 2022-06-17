using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject SlimePrefab;
    public Transform SpawnPoint;
    public float SpawnInterval;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnSlimes), 0f, SpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSlimes()
    {
        Instantiate(SlimePrefab, SpawnPoint.position, SpawnPoint.rotation);
    }
}
