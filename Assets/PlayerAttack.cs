using DG.Tweening;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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

    [Header("CastAnimation")]
    [SerializeField] private Rig HandRig;
    [SerializeField] private Rig HandRecoil;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float desiredDuration;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float StartingLerpValue;

    //[SerializeField] private float desiredDurationRecoil;
    //[SerializeField] private float elapsedTimeRecoil;

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
        CastAnimationLerp();

        if (_input.cast && fireballTimer <= 0)
        {
            if (thirdPersonController._hasAnimator)
            {
                //thirdPersonController._animator.SetBool(_animIDCast, _input.cast);
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
                //thirdPersonController._animator.SetBool(_animIDCast, _input.cast);
            }
            foreach (Rigidbody fireballRB in fireBallArray)
            {
                fireballRB.AddForce(fireballSpeed * firePoint.transform.forward);
            }
        }

        castLastFrame = _input.cast;
    }

    private void CastAnimationLerp()
    {
        if (_input.cast)
        {
            DOVirtual.Float(HandRig.weight, 1f, 0.2f, SetAimRigWeight);
        }
        else
        {
            DOVirtual.Float(HandRig.weight, 0, .2f, SetAimRigWeight);
        }
        void SetAimRigWeight(float weight)
        {
            HandRig.weight = weight;
        }
        if (castLastFrame && !_input.cast)
        {
            DOVirtual.Float(0, 1, .1f, (x) => HandRecoil.weight = x).OnComplete(() => DOVirtual.Float(1, 0, .3f, (x) => HandRecoil.weight = x));
        }
    }
}
