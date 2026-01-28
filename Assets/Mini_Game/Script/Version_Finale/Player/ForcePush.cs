using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float pushAmount; 
    public float pushRadius;

    private void Update()
    {
        if (Input .GetMouseButtonDown(0))
        {
            DoPush(); 
        }
    }

    private void DoPush()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pushRadius); 

        foreach(Collider pushedObject in colliders)
        {
            if (pushedObject.CompareTag("Box"))
            {
                Rigidbody pushedBody = pushedObject.GetComponent<Rigidbody>();

                pushedBody.AddExplosionForce(pushAmount, transform.position, pushRadius); 
            }
        }
    }
}
