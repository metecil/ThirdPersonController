using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    public Transform cameraTransform; // Assign your Cinemachine FreeLook Camera's transform here.
    public float speed = 5f;

    void Update() {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Determine camera-forward direction, ignoring the y component.
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        // Determine right direction relative to the camera.
        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        // Combine input with the camera directions
        Vector3 move = forward * vertical + right * horizontal;

        // Move the player
        controller.Move(move * speed * Time.deltaTime);
    }
}

