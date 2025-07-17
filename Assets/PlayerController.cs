using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 moveDirection;

    [SerializeField]
    private InputActionReference movement, pointerPosition, attack;

    // Update is called once per frame
    void Update()
    {
        moveDirection = movement.action.ReadValue<Vector2>().normalized;
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        OrientMouse();
    }

    void OrientMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(pointerPosition.action.ReadValue<Vector2>());
        Vector2 orientation = mousePos - rb.position;
        rb.rotation = Mathf.Atan2(orientation.y, orientation.x) * Mathf.Rad2Deg - 90f;
    }

}
