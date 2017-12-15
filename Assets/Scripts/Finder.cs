using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Finder : MonoBehaviour {

    const float RayCastMaxDistance = 100.0f;

    private Transform innerDestination;
    private int iWorldSelectedUnitType = 0;

    public Transform destination;
    public int iMyUnitType = 0;
    public bool bSelected = true;


    // Use this for initialization
    void Start () {
        innerDestination = destination;        
    }
	
	// Update is called once per frame
	void Update () {
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
        
        if (bSelected == false || iMyUnitType != iWorldSelectedUnitType)
        {
            transform.GetComponent<NavMeshAgent>().ResetPath();
            return;
        }

        transform.GetComponent<NavMeshAgent>().SetDestination(innerDestination.position);
        //transform.GetComponent<NavMeshAgent>().ResetPath(); // it does indeed reset the path, it stops following
        //transform.GetComponent<NavMeshAgent>().Stop(false);

        if (bSelected == false)
        {
            transform.GetComponent<NavMeshAgent>().ResetPath(); // it does indeed reset the path, it stops following
        }
    }
    
    public void SetSelectedWorldUnitType(int _iWorldUnitType)
    {        
        iWorldSelectedUnitType = _iWorldUnitType;
    }

    public void SetSelectedUnit(bool bSelected)
    {
        this.bSelected = bSelected;
    }
}