using UnityEngine;

public class Camera_1 : MonoBehaviour
{
    public Transform player;
    public float MouseSensitivity = 2f;
    float cameraVerticalAngle = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Call OnApplicationFocus to set the initial cursor state
        OnApplicationFocus(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Get mouse input
        float InputX = Input.GetAxis("Mouse X") * MouseSensitivity;

        float InputY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        //Get camera rotation
        cameraVerticalAngle -= InputY;

        // Clamp the camera's vertical angle to prevent flipping
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -90f, 90f);

        // Apply camera rotation
        transform.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);

        //Get player rotation
        player.Rotate(Vector3.up * InputX);
    }

    void OnApplicationFocus(bool focus)
    {
        // Lock or unlock (visibility too) the cursor based on application focus
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;

            Cursor.visible = true;
        }
    }    
}