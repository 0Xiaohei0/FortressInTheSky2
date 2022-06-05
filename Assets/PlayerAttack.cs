using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private ThirdPersonController thirdPersonController;
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int fireballSpeed;
    [SerializeField] private float RandomArea;
    [SerializeField] private List<Rigidbody> fireBallArray;

    private int _animIDCast;

    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float fireballCoolDown;
    public float fireballTimer;

    private bool castLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        _input = GetComponent<StarterAssetsInputs>();
        _animIDCast = Animator.StringToHash("Cast");
    }

    // Update is called once per frame
    void Update()
    {
        fireballTimer -= Time.deltaTime;

        if (_input.cast && fireballTimer <= 0)
        {
            if (thirdPersonController._hasAnimator)
            {
                thirdPersonController._animator.SetBool(_animIDCast, _input.cast);
            }
            fireballTimer = fireballCoolDown;
            Vector3 finalPosition = new Vector3(firePoint.position.x + Random.value * RandomArea, firePoint.position.y + Random.value * RandomArea, firePoint.position.z);
            GameObject spawnedFireball = Instantiate(fireball, finalPosition, firePoint.rotation);
            // Debug.Log(fireballSpeed * Vector3.forward);
            //Fireball.GetComponent<Rigidbody>().AddForce(fireballSpeed * firePoint.transform.forward);
            fireBallArray.Add(spawnedFireball.GetComponent<Rigidbody>());
        }
        if (castLastFrame && !_input.cast)
        {
            if (thirdPersonController._hasAnimator)
            {
                thirdPersonController._animator.SetBool(_animIDCast, _input.cast);
            }
            foreach (Rigidbody fireballRB in fireBallArray)
            {
                fireballRB.AddForce(fireballSpeed * firePoint.transform.forward);
            }
        }

        castLastFrame = _input.cast;
    }
}
