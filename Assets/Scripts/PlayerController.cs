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
    private bool isTalking = false;
    
    [HideInInspector]
    public bool canMove = true;


    [Header("Камера")]
    public GameObject cameraObject;
    private Animator animator;

    [Header("Инвентарь")]
    public List<Interactable> inventory = new List<Interactable>();
    
    public static PlayerController instance;

    void Awake()
	{
        if (instance == null){

            instance = this;
            //DontDestroyOnLoad(this.gameObject);
            talkPanel.SetActive(false);
            
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
            
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
        } else {
            Destroy(this);
        }
    }

    void Update()
    {
        cameraObject.GetComponent<CameraConroller>().canMove = canMove;
        
        Inputs();
        CameraHolderAnimator();
    }

    public void ElevatorTeleport(Transform spawnPos)
    {
        gameObject.transform.position = spawnPos.position;
        gameObject.transform.eulerAngles = spawnPos.rotation.eulerAngles;
        
        if (cameraObject.TryGetComponent<CameraConroller>(out CameraConroller cameraController))
        {
            cameraController.ResetCamera();
        }

    }

    void Interact(ObjectScript objectScript)
	{
        if (objectScript.data.type == Type.chair)
        {
            Debug.Log("Сажусь...");
        }
        else if (objectScript.data.type == Type.npc)
        {
            isTalking = true;
            canMove = false;
            lastNPC = objectScript;

            talkPanel.SetActive(true);
            
            // Анимация начала разговора
            talkPanel.GetComponent<Animation>().Play("Talking");
        }
        else if (objectScript.data.type == Type.button)
        {
            objectScript.ButtonPressed();
        }
    }

    public void Talk()
    {
        // Выводится черный экран с именем греха, потом черный экран плавно заменяется картиной греха и появляются описания кнопок
        talkPanel.GetComponent<Image>().sprite = lastNPC.data.artwork;
        Debug.Log("Я разговариваю с " + lastNPC.data.name);
    }

    public void PositiveTalk()
    {
        
    }

    public void NegativeTalk()
    {
        
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

                lastNPC.TalkIsDone();
                
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
