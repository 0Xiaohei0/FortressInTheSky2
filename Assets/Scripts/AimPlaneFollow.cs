using UnityEngine;

public class AimPlaneFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            transform.position = target.transform.position;
    }
}
