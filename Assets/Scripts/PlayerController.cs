using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private InputAction move;
    private float sideBound = 4.25f;
    private float frontBound = -.25f;
    private float backBound = -8.75f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = move.ReadValue<Vector2>();

        transform.Translate(new Vector3(movement.x, 0f, movement.y) * speed * Time.deltaTime);
        if (transform.position.x > sideBound)
        {
            transform.position = new Vector3(sideBound, transform.position.y, transform.position.z);
        } else if (transform.position.x < -sideBound)
        {
            transform.position = new Vector3(-sideBound, transform.position.y, transform.position.z);
        }
        if (transform.position.z < backBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, backBound);
        } else if (transform.position.z > frontBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, frontBound);
        }
    }
}
