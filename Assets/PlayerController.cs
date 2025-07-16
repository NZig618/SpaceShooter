using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 moveDirection;

    [SerializeField]
    private InputActionReference movement;

    // Update is called once per frame
    void Update()
    {
        moveDirection = movement.action.ReadValue<Vector2>().normalized;
        rb.linearVelocity = new Vector2 (moveDirection.x * moveSpeed, moveDirection.y * moveSpeed); 
    }

}
