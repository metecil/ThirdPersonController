using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public CharacterController controller;
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    // Jumping parameters
    public float jumpForce = 8f;
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;
    private int jumpCount = 0;
    public int maxJumps = 2;  

    // Dash parameters
    private bool isDashing = false;
    private Vector3 dashDirection;
    public float dashSpeed = 15f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    
    
    public Transform cameraTarget;

    void Update() {
        
        if (isDashing) {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            return;
        }

        // --- Horizontal Movement ---
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput) * moveSpeed;
        
        
        if (controller.isGrounded) {
            jumpCount = 0;  // Reset jump count when on the ground
            if (verticalVelocity < 0)
                verticalVelocity = -2f;  
            
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps) {
                verticalVelocity = jumpForce;
                jumpCount++;
            }
        } else {
            // Allow a double jump
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps) {
                verticalVelocity = jumpForce;
                jumpCount++;
            }
            verticalVelocity += gravity * Time.deltaTime;
        }
        
        // Combine horizontal movement with vertical velocity
        Vector3 velocity = moveDirection + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
        
        
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);

        
        // Left Shift as the dash button
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) {
            
            Vector3 inputDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;
            dashDirection = (inputDirection.sqrMagnitude > 0) ? inputDirection : transform.forward;
            StartCoroutine(Dash());
        }
    }
    
    void LateUpdate() {
        
        if (cameraTarget != null) {
            cameraTarget.position = transform.position;
            cameraTarget.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
    
    IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        float startTime = Time.time;
        while (Time.time < startTime + dashTime) {
            
            yield return null;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    
    
    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Ground")) {
            Debug.Log("Player hit ground: " + hit.gameObject.name);
        }
    }
}
