using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject joueur = GameObject.FindGameObjectWithTag("Player");
        Destroy(joueur);
    }
    public float speed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

