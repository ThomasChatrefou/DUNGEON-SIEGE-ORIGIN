using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;

    protected PlayerAction playerInput;
    protected IPlayerMovement playerMovement;
    private IWeaponUser weaponUser;
    private Vector2 direction;

    private void Awake()
    {
        playerInput = new PlayerAction();
        playerMovement = GetComponent<IPlayerMovement>();
        weaponUser = GetComponent<IWeaponUser>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Move.started += OnMoveStarted;
        playerInput.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        playerInput.Player.Move.started -= OnMoveStarted;
        playerInput.Player.Move.canceled -= OnMoveCanceled;
        playerInput.Disable();
    }

    private void Start()
    {
        if (weaponUser != null)
        {
            weaponUser.StartWeaponUse();
        }
    }

    private void Update()
    {
        direction = playerInput.Player.Move.ReadValue<Vector2>();
        playerMovement.Move(direction.normalized, playerSpeed);
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
        if (!isActiveAndEnabled)
        {
            return;
        }

        if (weaponUser != null)
        {
            weaponUser.StartWeaponUse();
        }
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
