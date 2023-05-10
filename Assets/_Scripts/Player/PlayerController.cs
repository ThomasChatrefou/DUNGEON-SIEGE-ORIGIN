using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;

    protected CharacterController controller;
    protected PlayerAction playerInput;
    private Vector3 playerVelocity;
    private IWeaponUser weaponUser;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = new PlayerAction();
        weaponUser = GetComponent<IWeaponUser>();


        playerInput.Player.Move.started += OnMoveStarted;
        playerInput.Player.Move.canceled += OnMoveCanceled;
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

        Vector2 movement = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

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
