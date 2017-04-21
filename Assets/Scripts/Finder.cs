using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Finder : MonoBehaviour {

    const float RayCastMaxDistance = 100.0f;

    private Transform innerDestination;
    private bool bSelected = true;

    public Transform destination;

    // Use this for initialization
    void Start () {
        innerDestination = destination;
    }
	
	// Update is called once per frame
	void Update () {
        //if(bSelected == false)
        //{
        //    return;
        //}
        transform.GetComponent<NavMeshAgent>().SetDestination(innerDestination.position);
        //transform.GetComponent<NavMeshAgent>().ResetPath(); // it does indeed reset the path, it stops following
        //transform.GetComponent<NavMeshAgent>().Stop(false);

        if (Input.GetButtonUp("Fire2"))
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

        if (bSelected == false)
        {
            transform.GetComponent<NavMeshAgent>().ResetPath(); // it does indeed reset the path, it stops following
        }
    }

    public void SetSelectedUnit(bool bSelected)
    {
        this.bSelected = bSelected;
    }
}