using DG.Tweening;
using UnityEngine;

public class SpawnedAutoScale : MonoBehaviour
{
    public float openTime;
    public float duration;
    private Vector3 startingScale;
    private void Awake()
    {
        startingScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(startingScale, openTime);
        Invoke(nameof(ScaleDownAndDestroy), openTime + duration);
    }

    private void ScaleDownAndDestroy()
    {
        transform.DOScale(Vector3.zero, openTime);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, openTime + 2f);
    }

}
