using UnityEngine;

public class Portal_Gun : MonoBehaviour
{
    public Portal_V2 Red;   
    public Portal_V2 Blue; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer != 8) return;

                if (Input.GetMouseButtonDown(0))
                {
                    Red.transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, -90, 0);
                    Red.transform.position = hit.point + Red.transform.right * 0.1f;
                }
                else
                {
                    Blue.transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, -90, 0);
                    Blue.transform.position = hit.point + Blue.transform.right * 0.1f;                    
                }
            }
        }
    }
}
