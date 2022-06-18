using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public float projectileClearRadius;
    Collider[] groundCollisions;
    [SerializeField] LayerMask projectileLayer;
    public GameObject GameOverUI;

    private void Awake()
    {
        GameOverUI.SetActive(false);
    }
    public void EndGame()
    {
        ClearPorjectiles();
        Invoke(nameof(SetUIActive), 3f);
    }

    public void ClearPorjectiles()
    {
        //clear projectiles
        groundCollisions = Physics.OverlapSphere(transform.position, projectileClearRadius, projectileLayer);
        foreach (Collider collider in groundCollisions)
        {
            if (collider.GetComponent<SpellProjectile>() != null)
            {
                Destroy(collider.gameObject);
            }
        }
    }

    private void SetUIActive()
    {
        //show UI
        GameOverUI.SetActive(true);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, projectileClearRadius);
    }
}
