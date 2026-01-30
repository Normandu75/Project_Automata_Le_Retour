using UnityEngine;

public class Camera_1 : MonoBehaviour
{
    public Transform player;
    public float MouseSensitivity = 2f;
    float cameraVerticalAngle = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        OnplicationFocus(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Get mouse input
        float InputX = Input.GetAxis("Mouse X") * MouseSensitivity;

        float InputY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        //Get camera rotation
        cameraVerticalAngle -= InputY;

        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -90f, 90f);

        transform.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);

        //Get player rotation
        player.Rotate(Vector3.up * InputX);
    }

    void OnplicationFocus(bool focus)
    {
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