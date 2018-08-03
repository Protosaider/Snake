using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour {

    //Операции с чем-то
    #region Handling
    [SerializeField]
    private float walkSpeed = 5.0f;
    [SerializeField]
    private float rotationSpeed = 450.0f;
    [SerializeField]
    private float runSpeed = 8.0f;

    private float nextFireTime;
    private float fireRate = 0.2f;
    #endregion

    #region System
    private Quaternion targetRotation;
    #endregion

    #region Components
    private CharacterController controller;
    private Camera cam;
    public Gun gun;
    #endregion

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        //RotatePlayer();
        //Rotate();
        //LookAtCursor();
        //MoveCam();
        //Move();
        //MoveAndRotate();
    }

    private void RotatePlayer()
    {
        // Get the position of the mouse
        Vector3 mouseLocation = Input.mousePosition;
        // Keep mouse location along the X,Z plane.
        mouseLocation.z = mouseLocation.y;
        mouseLocation.y = 0;

        Vector3 transPos = cam.WorldToScreenPoint(transform.position);
        transPos.y = 0;

        Vector3 inputRotation = mouseLocation - transPos;
        //transform.rotation = Quaternion.LookRotation(inputRotation);
        Debug.DrawLine(cam.ScreenToWorldPoint(transPos), cam.ScreenToWorldPoint(mouseLocation), Color.red, 2.0f);
        print(transPos);
    }

    private void LookAtCursor()
    {
        // The position of the middle of the screen.
        Vector3 screenMiddle = new Vector3(Screen.width * 0.5f, 0.0f, Screen.height * 0.5f);
        
        // Get the position of the mouse
        Vector3 mouseLocation = Input.mousePosition;

        // Keep mouse location along the X,Z plane.
        mouseLocation.z = mouseLocation.y;
        mouseLocation.y = 0;

        // Rotates the player to face the mouse relative to the center of the screen.
        Vector3 inputRotation = mouseLocation - screenMiddle;
        transform.rotation = Quaternion.LookRotation(inputRotation);
        print("LookRotation = " + inputRotation);
    }

    private void Move()
    {
        // Direction of Movement relative to the player's rotation.
        Vector3 input = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        //Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        print("input = " + input);
        Vector3 motion = input;
        motion += Vector3.up * -8.0f; //Gravity

        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1.0f; //diagonal movement
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;

        controller.Move(motion * Time.deltaTime);
    }

    private void MoveCam()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Vector3 motion = input;
        //motion += Vector3.up * -8.0f; //Gravity

        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1.0f; //diagonal movement
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;

        cam.transform.Translate(motion * Time.deltaTime);
    }

    private void Rotate()
    {
        // Vector3 input = new Vector3(Input.GetAxis("Mouse X"), 0.0f, 0.0f);
        float input = Input.GetAxis("Mouse X");

        Vector3 mousePos = Input.mousePosition;
        Vector3 scrToWrld = cam.ScreenToWorldPoint(mousePos);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float dist;

        plane.Raycast(ray, out dist);
        transform.LookAt(ray.GetPoint(dist));

        //Debug.DrawRay(transform.position, scrToWrld, Color.red, 5.0f);
        Debug.DrawRay(transform.position, ray.GetPoint(dist), Color.red, 5.0f);

        //transform.Rotate(transform.up, input * 4.0f);

        //print("Mouse X = " + input);
        //print("mousePos = " + mousePos);
        print("scrToWrld = " + scrToWrld);
        print("cam.transform.position = " + cam.transform.position);
    }

    private void MoveAndRotate()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0.0f, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

        Vector3 motion = input;
        motion += Vector3.up * -8.0f; //Gravity

        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1.0f; //diagonal movement
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;

        controller.Move(motion * Time.deltaTime);

        if ((Time.time > nextFireTime) && Input.GetButton("Fire1"))
        {
            gun.Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
}
