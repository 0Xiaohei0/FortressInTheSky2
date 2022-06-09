using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private LayerMask MouseColliderLayerMask;
    [SerializeField] public Vector3 MousePosition;


    private void Update()
    {
        MousePosition = GetMousePosition();
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, MouseColliderLayerMask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
