using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    
    [Header("Показатели персонажа")]
    public float speed = 12f;
    public float gravity = -9.81f;
    private float x;
    private float z;
    private Vector3 velocity;
    private Vector3 move;

    [Header("Проверка земли")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = -0.4f;
    private bool isGrounded;

    [Header("Взаимодействие")]
    public GameObject talkPanel;
    public float interactDistance = 1f;
    private ObjectScript lastChair;
    private ObjectScript lastNPC;
    private bool isSitting = false;
    private bool canMove = true;
    private bool isTalking = false;


    [Header("Камера")]
    public Transform cameraObject;
    private Animator animator;

    [Header("Инвентарь")]
    public List<Interactable> inventory = new List<Interactable>();

    void Awake()
	{
        talkPanel.SetActive(false);
        
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
    }

    void Update()
    {
        cameraObject.GetComponent<CameraConroller>().canMove = canMove;
        
        Inputs();
        CameraHolderAnimator();
    }

    void Interact(ObjectScript objectScript)
	{
        if (objectScript.data.type == Type.chair)
        {
            /*if (isSitting)
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
            }*/
        }
        else if (objectScript.data.type == Type.npc)
        {
            isTalking = true;
            canMove = false;
            lastNPC = objectScript;

            talkPanel.SetActive(true);
            talkPanel.GetComponent<Image>().sprite = lastNPC.data.artwork;

            Debug.Log("Я разговариваю с " + objectScript.data.name);
        }
        else if (objectScript.data.type == Type.button)
        {
            objectScript.ButtonPressed();
        }
    }

    void CameraHolderAnimator()
	{
        if (move.magnitude != 0 && isSitting == false)
        {
            animator.SetBool("isWalking", true);
            animator.speed = move.magnitude;
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.speed = 1f;
        }
    }
    
    void Inputs()
	{
        // Проверка нахождения на земле
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        if (canMove)
        {
            // Ходьба
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
        
        // Гравитация
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isTalking)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Я согласился с " + lastNPC.data.name);
                lastNPC = null;
                isTalking = false;
                canMove = true;
                talkPanel.GetComponent<Image>().sprite = null;
                talkPanel.SetActive(false);
            }
            
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Я отказался от предложения " + lastNPC.data.name);
                lastNPC = null;
                isTalking = false;
                canMove = true;
                talkPanel.GetComponent<Image>().sprite = null;
                talkPanel.SetActive(false);
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Взаимодействую...");
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
}    
