using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Transform APos; //create APos Object on Portal 1
    public Transform BPos; //create APos Object on Portal 2

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Portal 1"))
        {
            PlayerMovementTutorial PM = GetComponent<PlayerMovementTutorial>();
            PM.enabled = false; 
            transform.position = BPos.transform.position;
            transform.rotation = new Quaternion(transform.rotation.x, BPos.rotation.y, transform.rotation.z, transform.rotation.w); 
            PM.enabled = true; 
        }

        if (col.CompareTag("Portal 2"))
        {
            PlayerMovementTutorial PM = GetComponent<PlayerMovementTutorial>();
            PM.enabled = false; 
            transform.position = APos.transform.position;
            transform.rotation = new Quaternion(transform.rotation.x, APos.rotation.y, transform.rotation.z, transform.rotation.w); 
            PM.enabled = true; 
        }
    }
}
