using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    private Vector3 velocity;
    
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = -0.4f;
    private bool isGrounded;

    public float interactDistance = 1f;
    private ObjectScript lastChair;
    private bool isSitting = false;

    private float x;
    private float z;

    public Transform cameraObject;
    private Animator animator;
    private bool isRunning;

    public List<InteractableData> inventory = new List<InteractableData>();

    void Awake()
	{
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
    }

    void Update()
    {
        Actions();
        CameraHolderAnimator();
    }

    void Interact(ObjectScript objectScript)
	{
        if (objectScript.data.type == Type.chair)
        {
            if (isSitting)
            {
                Debug.Log("Standing up");

                Transform cameraHolder = gameObject.transform.GetChild(0);

                cameraObject.parent = cameraHolder;
                cameraObject.position = cameraHolder.position;
                cameraObject.GetComponent<CameraConroller>().StandUp();

                cameraObject.GetComponent<CameraConroller>().isSitting = false;
                isSitting = false;
            }
            else
            {
                Debug.Log("Sitting down");

                Transform cameraPlace = objectScript.transform.GetChild(0).transform;
                Transform playerPlace = objectScript.transform.GetChild(1).transform;

                cameraObject.parent = cameraPlace;
                cameraObject.position = cameraPlace.position;
                cameraObject.GetComponent<CameraConroller>().SitDown();

                cameraObject.GetComponent<CameraConroller>().isSitting = true;
                isSitting = true;

                gameObject.transform.position = playerPlace.position;
                gameObject.transform.rotation = playerPlace.rotation;

                lastChair = objectScript;

                inventory.Add(objectScript.data);
            }
        }
        else if (objectScript.data.type == Type.item)
        {
            Debug.Log("Using item");
            inventory.Add(objectScript.data);
        }
        else if (objectScript.data.type == Type.interactable)
        {
            Debug.Log("Interacting");
            inventory.Add(objectScript.data);
        }
    }

    void CameraHolderAnimator()
	{
        if (move.magnitude != 0 && isRunning == false && isSitting == false)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            animator.speed = move.magnitude;
        }
        else if (move.magnitude != 0 && isRunning == true && isSitting == false)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            animator.speed = 1f;
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.speed = 1f;
        }
    }

    void Actions()
	{
		if (isSitting == false)
		{
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            move = transform.right * x + transform.forward * z;

            float run = 1f;
            isRunning = false;
            if (Input.GetAxis("Run") > 0 && Mathf.Abs(x) < 0.5 && z > 0.5)
            {
                run = 1f + Input.GetAxis("Run");
                isRunning = true;
            }

            controller.Move(move * speed * run * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, interactDistance))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.TryGetComponent<ObjectScript>(out ObjectScript objectScript))
                    {
                        Interact(objectScript);
                    }
                }
            }
        }
        else 
        {
            if (Input.GetButtonDown("Jump"))
			{
                Interact(lastChair);
			}
        }

		if (Input.GetButtonDown("Fire2"))
		{
            Debug.Log("Inventory:");
            for (int i = 0; i < inventory.Count; i++)
            {
                Debug.Log(inventory[i].type.ToString());
            }
        }
    }    
}
