using UnityEngine;

public class PlayerController : MonoBehaviour {
    public CharacterController controller;
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    
    // Jumping parameters
    public float jumpForce = 8f;
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;
    
    // Camera target for your Cinemachine camera
    public Transform cameraTarget;

    void Update() {
        // --- Horizontal Movement ---
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput) * moveSpeed;
        
        // --- Jumping & Gravity ---
        if (controller.isGrounded) {
            // Apply a small negative force to keep the controller grounded reliably.
            if (verticalVelocity < 0)
                verticalVelocity = -2f;
            
            if (Input.GetButtonDown("Jump")) {
                verticalVelocity = jumpForce;
            }
        } else {
            verticalVelocity += gravity * Time.deltaTime;
        }
        
        // Combine horizontal movement with vertical (jump/gravity)
        Vector3 velocity = moveDirection + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
        
        // --- Player Rotation (Horizontal) ---
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);
    }
    
    void LateUpdate() {
        // --- Update Camera Target in LateUpdate ---
        if (cameraTarget != null) {
            cameraTarget.position = transform.position;
            // Force the camera target's rotation to match the player's yaw.
            cameraTarget.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
    
    // (Optional) Debug callback to log collisions with ground objects.
    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Ground")) {
            Debug.Log("Player hit ground: " + hit.gameObject.name);
        }
    }
}
