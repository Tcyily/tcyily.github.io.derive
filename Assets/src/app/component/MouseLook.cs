using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{

    // Use this for initialization
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;

    private bool __listener_flag = true;
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("xuan down");
            __listener_flag = !__listener_flag;
        }
        if (!__listener_flag)
        {
            transform.GetComponent<FPSInput>().enabled = false;
            return;
        }
        transform.GetComponent<FPSInput>().enabled = true;
        if (axes == RotationAxes.MouseX)
        {
            // transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y ") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            //  float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y;
            
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
