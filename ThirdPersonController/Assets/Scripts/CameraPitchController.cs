using UnityEngine;
using Unity.Cinemachine;

public class CameraPitchController : MonoBehaviour {
    [SerializeField] private CinemachineCamera freeLookCamera; // Assign your CinemachineCamera here
    public float pitchSpeed = 50f;   // How fast the pitch changes (degrees per second)
    public float minPitch = -35f;    // Lower pitch limit (e.g., looking down)
    public float maxPitch = 60f;     // Upper pitch limit (e.g., looking up)

    void Update() {
        float mouseY = Input.GetAxis("Mouse Y");
        // Get the current local pitch (x axis) of the camera.
        float currentPitch = freeLookCamera.transform.localEulerAngles.x;
        // Convert from 0-360 to -180 to 180 range
        if (currentPitch > 180f)
            currentPitch -= 360f;

        // Update pitch based on mouse input (invert sign if needed)
        currentPitch -= mouseY * pitchSpeed * Time.deltaTime;
        // Clamp the pitch to your defined limits
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        // Preserve the other local rotation values (y and z)
        Vector3 localEuler = freeLookCamera.transform.localEulerAngles;
        freeLookCamera.transform.localRotation = Quaternion.Euler(currentPitch, localEuler.y, localEuler.z);

        
    }
}
