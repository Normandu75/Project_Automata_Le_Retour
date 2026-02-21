using System.Collections;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Transform APos; //create APos Object on Portal 1
    public Transform BPos; //create APos Object on Portal 2
    public PlayerMovement PM;
    //private float delayTime = 5f;

    private void OnTriggerEnter(Collider col)
    {

        if (col.CompareTag("Portal A"))
        {
            PM = GetComponent<PlayerMovement>();
            PM.enabled = false; 
            transform.position = BPos.transform.position;
            transform.rotation = new Quaternion(transform.rotation.x, BPos.rotation.y, transform.rotation.z, transform.rotation.w); 
            PM.enabled = true; 
            
        }

        if (col.CompareTag("Portal B"))
        {
            PM = GetComponent<PlayerMovement>();
            PM.enabled = false; 
            transform.position = APos.transform.position;
            transform.rotation = new Quaternion(transform.rotation.x, APos.rotation.y, transform.rotation.z, transform.rotation.w); 
            PM.enabled = true; 
        }
    }
}
