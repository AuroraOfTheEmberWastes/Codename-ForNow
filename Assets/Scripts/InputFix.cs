using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputFix : MonoBehaviour
{
    void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.neverAutoSwitchControlSchemes = true;
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            var runner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            if (runner != null)
            {
                runner.OnViewRequestedInterrupt();
            }
        }
    }
}
