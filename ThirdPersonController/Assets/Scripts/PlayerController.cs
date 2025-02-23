using UnityEngine;

public class PlayerController : MonoBehaviour {
    public CharacterController controller;
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    
    // Jumping parameters
    public float jumpForce = 8f;
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;
    
    // Optional: camera target for your Cinemachine camera
    public Transform cameraTarget;

    void Update() {
        // --- Horizontal Movement ---
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection *= moveSpeed;
        
        // --- Jumping & Gravity ---
        if (controller.isGrounded) {
            // If grounded and jump button pressed, set upward velocity
            if (Input.GetButtonDown("Jump")) {
                verticalVelocity = jumpForce;
            } else {
                verticalVelocity = 0f;
            }
        } else {
            // Apply gravity over time if airborne
            verticalVelocity += gravity * Time.deltaTime;
        }
        
        // Combine horizontal movement with vertical (jump/gravity)
        Vector3 velocity = moveDirection + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
        
        // --- Player Rotation (Horizontal) ---
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);
        
        // --- Update Camera Target (if used) ---
        if (cameraTarget != null) {
            cameraTarget.position = transform.position;
            // Keep camera target's rotation aligned with the player (only yaw)
            cameraTarget.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
    
    // (Optional) Debug callback to check ground contacts with the CharacterController.
    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Ground")) {
            Debug.Log("Player hit ground: " + hit.gameObject.name);
        }
    }
}
