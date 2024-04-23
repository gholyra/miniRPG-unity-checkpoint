using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Health), typeof(Detector))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity = 10;
    [SerializeField] private float rotationvelocity = 10;
    [SerializeField, Range(1, 5)] private float velocityMultiplier;
    [SerializeField] private int damagePower;

    private float initialVelocity;

    private CharacterController characterController;
    private Detector detector;

    private Vector3 moveDirection;


    private void Awake()
    {
        initialVelocity = velocity;
        characterController = GetComponent<CharacterController>();
        GetComponent<Health>().OnDie += HandleDeath;
        detector = GetComponent<Detector>();
    }

    private void Start()
    {
        GameManager.Instance.inputManager.OnParry += HandleParry;
        GameManager.Instance.inputManager.OnRun += HandleRunVelocity;
    }

    private void FixedUpdate()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        Vector2 inputData = GameManager.Instance.inputManager.MoveDirection;
        moveDirection.x = inputData.x;
        moveDirection.z = inputData.y;
        Vector3 cameraRelativeMovement =
           ConvertMoveDirectionToCameraSpace(moveDirection);

        characterController.SimpleMove(cameraRelativeMovement *
                                 velocity);
        RotatePlayerAccordingToInput(cameraRelativeMovement);
    }

    private void RotatePlayerAccordingToInput(Vector3 cameraRelativeMovement)
    {
        Vector3 pointToLookAt;
        pointToLookAt.x = cameraRelativeMovement.x;
        pointToLookAt.y = 0;
        pointToLookAt.z = cameraRelativeMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(pointToLookAt);

            transform.rotation =
                Quaternion.Slerp(currentRotation,
                                 targetRotation,
                                 rotationvelocity * Time.deltaTime);
        }

    }

    private Vector3 ConvertMoveDirectionToCameraSpace(Vector3 moveDirection)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 cameraForwardZProduct = cameraForward * moveDirection.z;
        Vector3 cameraRightXProduct = cameraRight * moveDirection.x;

        Vector3 directionToMovePlayer =
            cameraForwardZProduct + cameraRightXProduct;

        return directionToMovePlayer;
    }

    private void HandleParry(bool isBlocking)
    {
        print("Estou bloqueando");
    }

    private void HandleRunVelocity(bool isRunning)
    {
        if (isRunning)
        {
            velocity *= velocityMultiplier;
        }
        else
        {
            velocity = initialVelocity;
        }
    }

    private void HandleDeath()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        GameManager.Instance.inputManager.DisableGameplayInput();
    }

    private void Attack()
    {
        print("GIVING DAMAGE");
        Collider[] enemiesColliders = detector.GetCollidersInDetectAreaSphere();
        print(enemiesColliders.Length);
        foreach(Collider collider in enemiesColliders)
        {
            print("CHECKING ENEMIES");
            if(collider.TryGetComponent(out Health enemyHealth))
            {
                print("SENDING DAMAGE");
                enemyHealth.TakeDamage(damagePower);
            }
        }
    }
}
