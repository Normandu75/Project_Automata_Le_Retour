using UnityEngine;

public class Portal_V2 : MonoBehaviour
{    
    public Portal_V2 OtherPortal;
    public Camera PortalCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OtherPortal.PortalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponent<MeshRenderer>().sharedMaterial.mainTexture = OtherPortal.PortalCamera.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookerPosition = OtherPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        PortalCamera.transform.localPosition = lookerPosition;

        Quaternion difference = transform.rotation * Quaternion.Inverse(OtherPortal.transform.rotation * Quaternion.Euler(180, 180, 0));
        PortalCamera.transform.rotation = difference * Camera.main.transform.rotation;

        PortalCamera.nearClipPlane = lookerPosition.magnitude;
    }
}
