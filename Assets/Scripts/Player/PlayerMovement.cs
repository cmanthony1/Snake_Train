using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input System Reference")]
    [SerializeField] private InputActionReference movementInput;
    [SerializeField] private InputActionReference pointerPosInput;

    [Header("Core Player Data")]
    [SerializeField] private AgentData playerData;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private SpriteRenderer sprend;

    [Header("Weapon Data")]
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private Transform weaponTransform;
    
    private Vector2 movement;
    private Vector2 mousePosition;
    private float speed;
    private float yWeaponScale;

    private void Start()
    {
        speed = playerData.moveSpeed;
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
        movement = movementInput.action.ReadValue<Vector2>();

        /* Converts to the mouse position to a world point position in the scene. */
        mousePosition = sceneCamera.ScreenToWorldPoint(pointerPosInput.action.ReadValue<Vector2>());
    }

    private void Move()
    {        
        playerRigidBody.velocity = movement.normalized * (speed * 100) * Time.deltaTime;
    }

    private void AimWeapon()
    {
        /* Converts to the mouse position to a world point position in the scene. */
        mousePosition = sceneCamera.ScreenToWorldPoint(pointerPosInput.action.ReadValue<Vector2>());

        /* Gets the direction of the where the mouse is (left or right). */
        Vector2 aimDirection = (mousePosition - (Vector2)transform.position).normalized;

        /* Calculates the angle between two points. */
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        /* Sets the angle of this object to face the mouse position. */
        weaponTransform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, aimAngle);

        /* Flips sprite based on the x position of the mouse. */
        if (aimDirection.x < 0)
        {
            sprend.flipX = true;
            weaponTransform.localScale = new Vector3(transform.localScale.x, -yWeaponScale, transform.localScale.z);
        }
        else if (aimDirection.x >= 0)
        {
            sprend.flipX = false;
            weaponTransform.localScale = new Vector3(transform.localScale.x, yWeaponScale, transform.localScale.z);
        }
    }
}
