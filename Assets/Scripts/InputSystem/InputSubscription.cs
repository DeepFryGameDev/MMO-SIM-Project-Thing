using UnityEngine;
using UnityEngine.InputSystem;

public class InputSubscription : MonoBehaviour
{
    // Variables:

    public Vector3 moveInput { get; private set; } = Vector3.zero;
    public bool sprintInput { get; private set; } = false;
    public bool actionInput { get; private set; } = false;
    public bool commandMenuInput { get; private set; } = false;
    public bool whistleInput { get; private set; } = false;

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

        _Input.PlayerInput.CommandMenuInput.started += SetAction;
        _Input.PlayerInput.CommandMenuInput.canceled += SetAction;

        _Input.PlayerInput.WhistleInput.started += SetAction;
        _Input.PlayerInput.WhistleInput.canceled += SetAction;
    }

    private void OnDisable() // unsubscribe to inputs
    {
        _Input.PlayerInput.MovementInput.performed -= SetMovement;
        _Input.PlayerInput.MovementInput.canceled -= SetMovement;

        _Input.PlayerInput.ActionInput.started -= SetAction;
        _Input.PlayerInput.ActionInput.canceled -= SetAction;

        _Input.PlayerInput.SprintInput.started -= SetAction;
        _Input.PlayerInput.SprintInput.canceled -= SetAction;

        _Input.PlayerInput.CommandMenuInput.started -= SetAction;
        _Input.PlayerInput.CommandMenuInput.canceled -= SetAction;

        _Input.PlayerInput.WhistleInput.started -= SetAction;
        _Input.PlayerInput.WhistleInput.canceled -= SetAction;

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
        // For keys that should be pressed and released
        actionInput = _Input.PlayerInput.ActionInput.WasPressedThisFrame();
        commandMenuInput = _Input.PlayerInput.CommandMenuInput.WasPressedThisFrame();
        whistleInput = _Input.PlayerInput.WhistleInput.WasPressedThisFrame();

        // For keys that should be held down
        sprintInput = _Input.PlayerInput.SprintInput.IsPressed();
    }
}
