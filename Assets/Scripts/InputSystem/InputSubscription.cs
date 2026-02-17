using UnityEngine;
using UnityEngine.InputSystem;

public class InputSubscription : MonoBehaviour
{
    // Variables:

    public Vector3 moveInput { get; private set; } = Vector3.zero;
    public bool sprintInput { get; private set; } = false;
    public bool actionInput { get; private set; } = false;

    InputMap _Input = null;

    private void OnEnable() // subscripe to inputs
    {
        _Input = new InputMap();

        _Input.PlayerInput.Enable();

        _Input.PlayerInput.MovementInput.performed += SetMovement;
        _Input.PlayerInput.MovementInput.canceled += SetMovement;

        _Input.PlayerInput.ActionInput.started += SetAction;
        _Input.PlayerInput.ActionInput.canceled += SetAction;

        _Input.PlayerInput.SprintInput.started += SetAction;
        _Input.PlayerInput.SprintInput.canceled += SetAction;
    }

    private void OnDisable() // unsubscribe to inputs
    {
        _Input.PlayerInput.MovementInput.performed -= SetMovement;
        _Input.PlayerInput.MovementInput.canceled -= SetMovement;

        _Input.PlayerInput.ActionInput.started -= SetAction;
        _Input.PlayerInput.ActionInput.canceled -= SetAction;

        _Input.PlayerInput.SprintInput.started -= SetAction;
        _Input.PlayerInput.SprintInput.canceled -= SetAction;

        _Input.PlayerInput.Disable();
    }

    // Perform context callback reading
    void SetMovement(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector3>();
    }

    void SetAction(InputAction.CallbackContext ctx)
    {
        actionInput = ctx.started;
    }

    void Update()
    {
        actionInput = _Input.PlayerInput.ActionInput.WasPressedThisFrame();
        sprintInput = _Input.PlayerInput.SprintInput.IsPressed();
    }
}
