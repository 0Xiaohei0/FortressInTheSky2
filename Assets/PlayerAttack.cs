using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private bool inputCast;
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int fireballSpeed;
    [SerializeField] private float RandomArea;
    [SerializeField] private List<Rigidbody> fireBallArray;

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
    [SerializeField] private GameObject playerCursorIcon;
    [SerializeField] private GameObject magicCircle;
    private PlayerCursor playerCursor;

    //[SerializeField] private float desiredDurationRecoil;
    //[SerializeField] private float elapsedTimeRecoil;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerCursor = GetComponent<PlayerCursor>();
    }

    // Update is called once per frame
    void Update()
    {
        inputCast = Input.GetMouseButton(0);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerCursorIcon.transform.position = playerCursor.MousePosition;
        fireballTimer -= Time.deltaTime;
        CastAnimationLerp();
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
                fireballRB.AddForce(fireballSpeed * (playerCursorIcon.transform.position - firePoint.transform.position));
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
}
