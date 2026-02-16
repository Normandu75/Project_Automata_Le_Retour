using System;
using Unity.VisualScripting;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Teleportation OtherPortal;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        float zPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

        if (zPosition < 0)
        {
            Teleport(other.transform);           
        }

    }

    private void Teleport(Transform objectToTeleport)
    {
        // Position
        Vector3 localPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(objectToTeleport.position);
        localPosition = new Vector3(-localPosition.x, localPosition.y, -localPosition.z);
        objectToTeleport.position = OtherPortal.transform.localToWorldMatrix.MultiplyPoint3x4(localPosition);

        // Rotation
        Quaternion difference = OtherPortal.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        objectToTeleport.rotation = difference * objectToTeleport.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
      other.gameObject.layer = 7;  
    }

        private void OnTriggerExit(Collider other)
    {
      other.gameObject.layer = 6;  
    }
}
