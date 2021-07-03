using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovement : MonoBehaviour
{
    public float dashTime = 0f;
    public float startDashTime = 0.1f;
    public float dashCooldownTime = 2f;
    private float dashCooldown = 0f;
    public float dashSpeed = 100f;
    public Vector3 dashDirection;

    private Vector3 moveDirection;
    private float speed = 5f;

    private new Camera camera;

    private Vector3 lastMousePosition;

    private bool canMove = true;
    private float timeUntilMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        if (dashTime > 0f)
        {
            transform.position += dashDirection * Time.fixedDeltaTime * dashSpeed;
            dashTime -= Time.deltaTime;
        }
        else
        {
            transform.position += moveDirection * Time.fixedDeltaTime * speed;
        }
        dashCooldown -= Time.deltaTime;
    }

    public void Dash()
    {
        if (!canMove)
            return;

        if (dashCooldown < 0f)
        {
            dashCooldown = dashCooldownTime;
            dashDirection = moveDirection;
            dashTime = startDashTime;
        }
    }

    public void SetDirection(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        timeUntilMove -= Time.deltaTime;
        if (timeUntilMove <= 0f)
        {
            canMove = true;
        }

        HandleRotion();
    }


    //Private methods
    private void HandleRotion()
    {
        if (!canMove)
            return;

        //early return
        if (lastMousePosition.Equals(Input.mousePosition))
        {
            return;
        }

        lastMousePosition = Input.mousePosition;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    public void FreezeMovement(float time)
    {
        timeUntilMove = time;
        canMove = false;
    }
}
