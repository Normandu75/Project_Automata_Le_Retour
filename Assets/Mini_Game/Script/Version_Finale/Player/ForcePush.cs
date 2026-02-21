using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float pushAmount; 
    public float pushRadius;

    private void Update()
    {
        if (Input .GetMouseButtonDown(0))
        {
            // Call the DoPush method when the left mouse button is clicked
            DoPush(); 
        }
    }

    private void DoPush()
    {
        // Find all colliders within the push radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, pushRadius); 

        foreach(Collider pushedObject in colliders)
        {
            // Check if the object has the "Box" tag before applying the push force
            if (pushedObject.CompareTag("Box"))
            {
                Rigidbody pushedBody = pushedObject.GetComponent<Rigidbody>();

                pushedBody.AddExplosionForce(pushAmount, transform.position, pushRadius); 
            }
        }
    }
}
