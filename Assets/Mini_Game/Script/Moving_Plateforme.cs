using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Moving_Plateforme : MonoBehaviour
{
    [SerializeField]
    GameObject CheckpointA;
    [SerializeField]
    GameObject CheckpointB;
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    float delay = 1.0f;
    [SerializeField]
    GameObject Plateforme;
    [SerializeField]
    bool isMoving = true;
    private bool alreadyMoving = false;
    private Vector3 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Plateforme.transform.position = CheckpointA.transform.position;
        targetPosition = CheckpointB.transform.position;
        StartCoroutine(MovePlateforme());
    }

    IEnumerator MovePlateforme()
    {
        while (true)
        {
            while ((targetPosition - Plateforme.transform.position).sqrMagnitude > 0.01f)
            {
                Plateforme.transform.position = Vector3.MoveTowards(Plateforme.transform.position, targetPosition, speed * Time.deltaTime);

                yield return null;
            }

            targetPosition = targetPosition == CheckpointA.transform.position ? CheckpointB.transform.position : CheckpointA.transform.position;
            
            yield return new WaitForSeconds(delay);
        }
    }
}
