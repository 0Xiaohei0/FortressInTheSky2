using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 offsetFlipped = new Vector3(offset.x, offset.y, offset.z * (playerController.facingRight ? 1 : -1));
        transform.position = playerController.transform.position + offsetFlipped;
    }
}
