using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 controlThrow;
    public int playerIndex;

    PlayerInput playerInput;
    playerController playerController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.playerIndex;
    }

    private void OnMove(InputValue value)
    {
        controlThrow = value.Get<Vector2>();
    }
}
