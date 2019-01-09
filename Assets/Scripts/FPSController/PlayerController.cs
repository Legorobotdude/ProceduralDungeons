using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private float jump_force = 500f;
    [SerializeField] private float tetherStrength = 10f;
    [SerializeField] private float jetpackStrength = 10f;

    
    private Player player;
    private PlayerMotor motor;
    private CapsuleCollider playerCollider;
    private Rigidbody playerRigidbody;
    private Vector3 movementInput;
    // Use this for initialization
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;

        //Calculate movement vel as 3d vector
        float _xMovement = Input.GetAxisRaw("Horizontal");
        float _zMovement = Input.GetAxisRaw("Vertical");
        //float _yMovement = Input.GetAxisRaw("Jetpack");
        //Calculate movement

        //movementInput = new Vector3(_xMovement, _yMovement, _zMovement);
        movementInput = new Vector3(_xMovement, 0f, _zMovement);

        Vector3 _movHorizontal = transform.right * _xMovement;
        Vector3 _movVertical = transform.forward * _zMovement;

        Vector3 _velocity;

        //Check for sprinting, can only sprint if on the ground

        if (Input.GetKey(KeyCode.LeftShift))
        {

            _velocity = (_movHorizontal + _movVertical).normalized * runSpeed;
        }
        else
        {
            _velocity = (_movHorizontal + _movVertical).normalized * speed;
        }

        //motor.Move(_velocity);

        //Move the character for horizontal mouse movement
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
        motor.Rotate(_rotation);


        //Move the camera up and down for vertical mouse movement
        float _xRot = Input.GetAxisRaw("Mouse Y");
        float _cameraRotation = _xRot * lookSensitivity;
        motor.RotateCamera(_cameraRotation);

        


        

    }

    public void SetHitAttraction(Vector3 hit){

        playerRigidbody.AddForce(Vector3.Normalize(hit - playerRigidbody.position) * tetherStrength*Time.deltaTime);
    
    }

    void FixedUpdate(){
        playerRigidbody.AddForce(Vector3.Normalize(player.camera.transform.TransformVector(movementInput)) * jetpackStrength);
    }

    //Check if we are on the ground
    private bool IsGrounded()
    {
        float distanceToGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.25f);
    }
}
