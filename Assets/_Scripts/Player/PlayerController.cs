using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;

    protected PlayerAction playerInput;
    protected PlayerMovement playerMovement;
    private Vector3 playerVelocity;
    private IWeaponUser weaponUser;

    private void Awake()
    {
        playerInput = new PlayerAction();
        playerMovement = GetComponent<PlayerMovement>();
        weaponUser = GetComponent<IWeaponUser>();


        playerInput.Player.Move.started += OnMoveStarted;
        playerInput.Player.Move.canceled += OnMoveCanceled;
    }

    private void Start()
    {
        if (weaponUser != null)
        {
            weaponUser.StartWeaponUse();
        }
    }

    private void OnMoveStarted(InputAction.CallbackContext ctx)
    {
        if (weaponUser != null)
        {
            weaponUser.StopWeaponUse();
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        if (weaponUser != null)
        {
            weaponUser.StartWeaponUse();
        }
    }

    private void Update()
    {
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 direction = playerInput.Player.Move.ReadValue<Vector2>();
        direction.Normalize();

        playerMovement.Move(direction, playerSpeed);

/*        Vector3 move = new Vector3(direction.x, 0, direction.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        controller.Move(playerVelocity * Time.deltaTime);
*/    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public void SetPlayerSpeed(float _speed)
    {
        playerSpeed = _speed;
    }
}
