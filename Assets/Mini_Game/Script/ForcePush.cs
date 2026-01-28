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

        foreach(Collider pushedObjec in colliders)
        {
            if (pushedObjec.CompareTag("Box"))
            {
                Rigidbody pushedBody = pushedObjec.GetComponent<Rigidbody>();

                pushedBody.AddExplosionForce(pushAmount, Vector3.up, pushRadius); 
            }
        }
    }
}
