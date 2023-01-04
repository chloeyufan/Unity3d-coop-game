using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    public Vector2 controlThrow;
    public int playerIndex;

    PlayerInput playerInput;
    [SerializeField] float speed =1f;

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
