using UnityEngine;

public class PlayerController : MonoBehaviour {
    public CharacterController controller;
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f; // Controls horizontal rotation sensitivity

    // Assign this in the Inspector with your CameraTarget GameObject.
    public Transform cameraTarget;

    void Update() {
        // --- Movement ---
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Use player's forward/right for movement
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // --- Player Rotation ---
        // Rotate the player based on horizontal mouse movement (mouse X).
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);

        // --- Update Camera Target ---
        // Ensure the camera target follows the player's position and rotation.
        if (cameraTarget != null) {
            cameraTarget.position = transform.position;
            cameraTarget.rotation = transform.rotation;
        }
    }
}
