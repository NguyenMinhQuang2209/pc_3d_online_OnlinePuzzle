using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    private PlayerInputActions action;
    public PlayerInputActions.OnFootActions onFoot;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        action = new PlayerInputActions();
        onFoot = action.onFoot;
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            playerMovement = GetComponent<PlayerMovement>();
            onFoot.Jump.performed += ctx => playerMovement.Jump();
        }
    }
    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        playerMovement.Movement(onFoot.Movement.ReadValue<Vector2>());
        playerMovement.Rotate(onFoot.Mouse.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
