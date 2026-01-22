using UnityEngine;

public class Camera_1 : MonoBehaviour
{
    public Transform player;
    public float MouseSensitivity = 2f;
    float cameraVerticalAngle = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float InputX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float InputY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        cameraVerticalAngle -= InputY;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalAngle;

        player.Rotate(Vector3.up * InputX);
    }
}