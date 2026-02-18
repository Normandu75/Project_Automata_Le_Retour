using UnityEngine;

public class ForcePush : MonoBehaviour
{
    public float pushAmount;
    public float pushRadius;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoPush();
            Debug.Log("Push executed!");
        }
    }

    private void DoPush()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pushRadius);

        foreach (Collider pushedObject in colliders)
        {
            if (pushedObject.CompareTag("Box"))
            {
                Rigidbody pushedBody = pushedObject.GetComponent<Rigidbody>();
                if (pushedBody != null)
                {
                    pushedBody.AddExplosionForce(pushAmount, transform.position, pushRadius);
                }
            }
        }
    }
}
