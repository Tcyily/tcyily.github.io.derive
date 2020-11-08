using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float state = 0;
    public float minFall = -1.5f;
    public float vertSpeed = 0;
    public float jumpSpeed =49.0f;
    public float check;
    RaycastHit hit;
    [SerializeField] private GameObject firePrefab;
    private GameObject _fire;
    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        Physics.Raycast(transform.position, Vector3.down, out hit);
        if (_charController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out hit))
            check = hit.distance;
    }



    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);


        if (_charController.isGrounded )
        {
            state = 0;
            vertSpeed = gravity;
            if (Input.GetButtonDown("Jump"))
            {
                vertSpeed =jumpSpeed;
                state = 1;
            }
        }
        else
        {
            if (state == 1)
            {
                vertSpeed += gravity * Time.deltaTime;
                if (Input.GetButtonDown("Jump"))
                {
                    vertSpeed = jumpSpeed;
                    state = 2;
                }
            }
            if (state == 2)
            {
                vertSpeed += gravity * Time.deltaTime;
                if (_charController.isGrounded)
                    state = 0;
            }
        }
        
        if(_charController.isGrounded&&(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.A)))
        {
            _fire = Instantiate(firePrefab )as GameObject;
            _fire.transform.position = transform.position;
        }

        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
        transform.Rotate(0, Input.GetAxis("Mouse X") * 9.0f, 0);

    }


}



