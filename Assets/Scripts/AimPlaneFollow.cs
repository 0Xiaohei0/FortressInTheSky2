using UnityEngine;

public class AimPlaneFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
    }
}
