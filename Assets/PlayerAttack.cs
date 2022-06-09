using DG.Tweening;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAttack : MonoBehaviour
{
    private ThirdPersonController thirdPersonController;
    [SerializeField] private bool inputCast;
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

    [Header("CastAim")]
    [SerializeField] private LayerMask MouseColliderLayerMask;
    [SerializeField] private GameObject playerCursor;
    [SerializeField] private GameObject magicCircle;

    //[SerializeField] private float desiredDurationRecoil;
    //[SerializeField] private float elapsedTimeRecoil;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        thirdPersonController = GetComponent<ThirdPersonController>();
        _animIDCast = Animator.StringToHash("Cast");

    }

    // Update is called once per frame
    void Update()
    {
        inputCast = Input.GetMouseButton(0);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        fireballTimer -= Time.deltaTime;
        CastAnimationLerp();
        playerCursor.transform.position = GetMousePosition();
        //Debug.Log(playerCursor.transform.position);

        magicCircle.SetActive(inputCast);

        if (inputCast && fireballTimer <= 0)
        {
            fireballTimer = fireballCoolDown;
            //Vector3 finalPosition = new Vector3(firePoint.position.x + Random.value * RandomArea, firePoint.position.y + Random.value * RandomArea, firePoint.position.z);
            GameObject spawnedFireball = Instantiate(fireball, firePoint.position, firePoint.rotation);
            // Debug.Log(fireballSpeed * Vector3.forward);
            //Fireball.GetComponent<Rigidbody>().AddForce(fireballSpeed * firePoint.transform.forward);
            fireBallArray.Add(spawnedFireball.GetComponent<Rigidbody>());
        }

        foreach (Rigidbody fireballRB in fireBallArray)
        {
            fireballRB.position = firePoint.position;
        }

        if (castLastFrame && !inputCast)
        {
            foreach (Rigidbody fireballRB in fireBallArray)
            {
                fireballRB.AddForce(fireballSpeed * (playerCursor.transform.position - firePoint.transform.position));
            }
            fireBallArray = new List<Rigidbody>();
        }

        castLastFrame = inputCast;
    }

    private void CastAnimationLerp()
    {
        if (inputCast)
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
        if (castLastFrame && !inputCast)
        {
            DOVirtual.Float(0, 1, .1f, (x) => HandRecoil.weight = x).OnComplete(() => DOVirtual.Float(1, 0, .3f, (x) => HandRecoil.weight = x));
        }
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
