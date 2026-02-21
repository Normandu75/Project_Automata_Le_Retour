using UnityEngine;

public class Portal_V2 : MonoBehaviour
{    
    public Portal_V2 OtherPortal;
    public Camera PortalCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create a new RenderTexture for the portal camera and assign it to the material of the portal's mesh renderer
        OtherPortal.PortalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = OtherPortal.PortalCamera.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        //Position
        Vector3 lookerPosition = OtherPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
        PortalCamera.transform.localPosition = lookerPosition;

        //Rotation
        Quaternion difference = transform.rotation * Quaternion.Inverse(OtherPortal.transform.rotation * Quaternion.Euler(0, 180, 0));
        PortalCamera.transform.rotation = difference * Camera.main.transform.rotation;

        //clipping
        PortalCamera.nearClipPlane = lookerPosition.magnitude;
    }
}
