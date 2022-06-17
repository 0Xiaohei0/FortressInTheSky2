using UnityEngine;

public class Follow : MonoBehaviour
{

    public Transform target;
    public float MinModifier;
    public float MaxModifier;

    Vector3 _velocity = Vector3.zero;

    public bool isFollowing;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void startFollow()
    {
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }
    }
}
