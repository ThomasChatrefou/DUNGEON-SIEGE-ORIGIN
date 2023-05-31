using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;

    protected PlayerAction playerInput;
    protected IPlayerMovement playerMovement;
    private IWeaponUser weaponUser;
    private Vector2 direction;


    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _playerDeathChannel;
    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _exitLevelChannel;

    private void Awake()
    {
        playerInput = new PlayerAction();
        playerMovement = GetComponent<IPlayerMovement>();
        weaponUser = GetComponent<IWeaponUser>();
    }

    private void OnEnable()
    {
        EnableInputs();

        _playerDeathChannel.OnEventTrigger += DisableInputs;
        _exitLevelChannel.OnEventTrigger += DisableInputs;
    }

    private void OnDisable()
    {
        DisableInputs();

        _playerDeathChannel.OnEventTrigger -= DisableInputs;
        _exitLevelChannel.OnEventTrigger -= DisableInputs;
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

    private void EnableInputs()
    {
        playerInput.Enable();
        playerInput.Player.Move.started += OnMoveStarted;
        playerInput.Player.Move.canceled += OnMoveCanceled;
    }

    private void DisableInputs()
    {
        playerInput.Player.Move.started -= OnMoveStarted;
        playerInput.Player.Move.canceled -= OnMoveCanceled;
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
