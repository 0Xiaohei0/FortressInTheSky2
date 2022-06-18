using UnityEngine;

public class HoverBossAI : Damagable
{
    public Transform FirePoint1;
    public Transform FirePoint2;
    private int NextFirePoint;

    private bool facingLeft = true;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        dropLootTarget = GameObject.FindWithTag("DropLootTracker");
        NextFirePoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Fire()
    {

    }

    void OnDrawGizmosSelected()
    {
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
