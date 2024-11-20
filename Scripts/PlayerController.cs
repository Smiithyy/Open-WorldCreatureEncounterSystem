using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private LayerMask grassLayer;
    [SerializeField] private int StepsInGrass;
    [SerializeField] private int minStepsToEncounter;
    [SerializeField] private int maxStepsToEncounter;

    private PlayerControls playerControls;
    private Rigidbody rb;
    private Vector3 movement;
    private bool movingInGrass;
    private float stepTimer;
    private int stepsToEncounter;
    private PartyManager partyManager;


    private const string IS_WALK_PARAM = "IsWalk";
    private const string BATTLE_SCENE = "BattleScene";
    private const float TIME_PER_STEP = 0.5f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        CalculateStepsToNextEncounter();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();

        if (partyManager.GetPosition() != Vector3.zero)
        {
            transform.position = partyManager.GetPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = playerControls.Player.Move.ReadValue<Vector2>().x;
        float z = playerControls.Player.Move.ReadValue<Vector2>().y;

        movement = new Vector3(x, 0, z).normalized;

        anim.SetBool(IS_WALK_PARAM, movement!=Vector3.zero);

        if (x != 0 && x < 0)
        {
            playerSprite.flipX = true;
        }

        if (x != 0 && x > 0)
        {
            playerSprite.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1, grassLayer);
        movingInGrass = colliders.Length != 0 && movement != Vector3.zero;

        if(movingInGrass == true)
        {
            stepTimer += Time.fixedDeltaTime;
            if(stepTimer > TIME_PER_STEP)
            {
                StepsInGrass++;
                stepTimer = 0;

                if(StepsInGrass >= stepsToEncounter)
                {
                    partyManager.SetPosition(transform.position);
                    SceneManager.LoadScene(BATTLE_SCENE);
                }

                // check to see if we have reached an encounter
                // change scene to battle
            }
        }
    }

    private void CalculateStepsToNextEncounter()
    {
        stepsToEncounter = Random.Range(minStepsToEncounter, maxStepsToEncounter);
    }
}
