using DG.Tweening;
using UnityEngine;

public class SpawnedAutoScale : MonoBehaviour
{
    public float openTime;
    public float duration;
    [SerializeField] private Vector3 startingScale;
    public bool destory;
    private void Awake()
    {
        Debug.Log("Awake");
        startingScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    void OnEnable()
    {
        Debug.Log("Enable");
        transform.DOScale(startingScale, openTime);
        if (destory)
        {
            Invoke(nameof(ScaleDownAndDestroy), openTime + duration);
        }
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
        transform.localScale = Vector3.zero;
    }

    private void ScaleDownAndDestroy()
    {
        transform.DOScale(Vector3.zero, openTime);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, openTime + 2f);
    }

}
