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
    }
}
