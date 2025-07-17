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
    public float gunHeat;
    public float cooloown = 0.25f;

    [SerializeField]
    private InputActionReference movement, pointerPosition, attack;

    // Update is called once per frame
    void Update()
    {
        moveDirection = movement.action.ReadValue<Vector2>().normalized;
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        OrientMouse();

        if (gunHeat > 0) {
            gunHeat -= Time.deltaTime;
        }

        if (attack.action.ReadValue<float>() == 1 && gunHeat <= 0)
        {
            gunHeat = cooloown;
            Fire();
        }
    }

    void OrientMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(pointerPosition.action.ReadValue<Vector2>());
        Vector2 orientation = mousePos - rb.position;
        rb.rotation = Mathf.Atan2(orientation.y, orientation.x) * Mathf.Rad2Deg - 90f;
    }

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
}
