using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Core Player Data")]
    [SerializeField] private AgentData playerData;
    [SerializeField] private Camera sceneCamera;

    /* Core Player Data */
    private Rigidbody2D playerRigidBody;
    private Transform playerSpriteTransform;

    /* Weapon Data */
    private Transform weaponTransform;

    /* Movement Data */
    private Vector2 movement;
    private Vector2 mousePosition;
    private float speed;
    private float xPlayerScale;
    private float yWeaponScale;

    /* Dash Properties */
    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.2f;

    /* Finds the child with the specified name of the object this script is attached to. */
    private void Awake()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerSpriteTransform = transform.Find("SpritePlayer").GetComponent<Transform>();
        weaponTransform = transform.Find("Revolver (Pivot)").GetComponent<Transform>();
    }

    private void Start()
    {
        speed = playerData.MoveSpeed;
        xPlayerScale = playerSpriteTransform.localScale.x;
        yWeaponScale = weaponTransform.localScale.y;
    }

    private void Update()
    {
        ProcessInputs();
        AimWeapon();
    }

    private void FixedUpdate()
    {
        Move();   
    }

    private void ProcessInputs()
    {
        /* Gets input data from WASD or Arrow Keys. */
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        /* Converts to the mouse position to a world point position in the scene. */
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        /* Captures data from movement and stores values when Left-Shift or (exclusive) Right Mouse Button is pressed. */
        if ((Input.GetKeyDown(KeyCode.LeftShift) ^ Input.GetMouseButtonDown(1)) && canDash)
        {
            Vector2 directionOnDash = movement;
            StartCoroutine(Dash(directionOnDash));
        }
    }

    /* Player Movement. */
    private void Move()
    {        
        if (!isDashing)
        {
            playerRigidBody.velocity = movement.normalized * (speed * 100) * Time.deltaTime;
        }
    }

    /* Weapon Aiming and Sprite Control */
    private void AimWeapon()
    {
        Vector2 aimDirection = (mousePosition - (Vector2)transform.position).normalized;

        /* Calculates the angle between two points. */
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        /* Sets the angle of this object to face the mouse position. */
        weaponTransform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, aimAngle);

        /* Flips sprite based on the x position of the mouse. */
        if (aimDirection.x < 0)
        {
            playerSpriteTransform.localScale = new Vector3(-xPlayerScale, playerSpriteTransform.localScale.y, playerSpriteTransform.localScale.z);
            weaponTransform.localScale = new Vector3(weaponTransform.localScale.x, -yWeaponScale, weaponTransform.localScale.z);
        }
        else
        {
            playerSpriteTransform.localScale = new Vector3(xPlayerScale, playerSpriteTransform.localScale.y, playerSpriteTransform.localScale.z);
            weaponTransform.localScale = new Vector3(weaponTransform.localScale.x, yWeaponScale, weaponTransform.localScale.z);
        }
    }

    /* Dash Mechanic. Sets timer on Dash. */
    private IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        isDashing = true;
        playerRigidBody.AddForce(direction.normalized * playerData.DashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(playerData.DashCooldown);
        canDash = true;
    }
}
