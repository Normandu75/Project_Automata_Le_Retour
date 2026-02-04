using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject objectToSpawn;

    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // make sure only to stick to the first target you hit
        if (targetHit) 
            return;
        else
            targetHit = true;

        // make sure projectile sticks to surface
        rb.isKinematic = true;

        // Get contact point
        ContactPoint contact = collision.GetContact(0);

        //Get position of the contact point
        Vector3 pos = contact.point;

        // spawn designated object at contact point
        Instantiate(objectToSpawn, pos, Quaternion.identity);

        // make sure projectile moves with target
        transform.SetParent(collision.transform);
    }
}