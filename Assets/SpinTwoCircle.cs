using UnityEngine;

public class SpinTwoCircle : MonoBehaviour
{
    public GameObject circle1;
    public GameObject circle2;
    public float spinSpeed;


    // Update is called once per frame
    void Update()
    {
        circle1.transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed);
        circle2.transform.Rotate(Vector3.up * Time.deltaTime * -spinSpeed);
        //Vector3 eulers = circle1.transform.rotation.eulerAngles;
        //circle1.transform.rotation = Quaternion.Euler(new Vector3(eulers.x + spinSpeed, eulers.y, eulers.z));

        //Vector3 eulers2 = circle2.transform.rotation.eulerAngles;
        //circle2.transform.rotation = Quaternion.Euler(new Vector3(eulers2.x - spinSpeed, eulers2.y, eulers2.z));
    }
}
