using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Finder : MonoBehaviour {

    public Transform destination;

    const float RayCastMaxDistance = 100.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetComponent<NavMeshAgent>().destination = destination.position;

        if (Input.GetButtonUp("Fire1"))
        {
            Vector2 clickPos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
            RaycastHit hitInfo;

            // Using Raycast
            if (Physics.Raycast(ray, out hitInfo, RayCastMaxDistance, (1 << LayerMask.NameToLayer("Ground"))))
            {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    destination.transform.position = hitInfo.point;
                }
            }
        }
    }
}