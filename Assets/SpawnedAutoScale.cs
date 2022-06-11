using DG.Tweening;
using UnityEngine;

public class SpawnedAutoScale : MonoBehaviour
{
    public float openTime;
    public float duration;
    [SerializeField] private Vector3 startingScale;
    [SerializeField] private GameObject lightObject;
    public bool destory;
    private void Awake()
    {
        startingScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    void OnEnable()
    {
        lightObject.SetActive(true);
        transform.DOScale(startingScale, openTime);
        if (destory)
        {
            Invoke(nameof(ScaleDownAndDestroy), openTime + duration);
        }
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
        lightObject.SetActive(false);
    }

    private void ScaleDownAndDestroy()
    {
        lightObject.SetActive(false);
        transform.DOScale(Vector3.zero, openTime);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, openTime + 2f);
    }

}
