using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    private GameObject[] finderGO;
    private Finder[] finderSC;

    public int iNowTotalUnitType = 2;
    public int iSelectedUnitType = 0;

    // Use this for initialization
    void Start () {
        finderGO = GameObject.FindGameObjectsWithTag("SelectableUnit");
        //print(finderGO.Length);
        finderSC = new Finder[finderGO.Length];
        //print(finderSC.Length);
        for (int i = 0; i < finderSC.Length; i++) {
            finderSC[i] = finderGO[i].GetComponent<Finder>();
            //print(finderSC[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(finderSC.Length);
        //if(finderSC.Length == 0)
        //{
        //    (Finder) finderSC[0].
        //}
        
        if (Input.GetButtonUp("Fire1"))
        {
            //print("Prev = " + iSelectedUnitType);
            iSelectedUnitType++;
            iSelectedUnitType = iSelectedUnitType % (iNowTotalUnitType + 1);
            //print("Next = " + iSelectedUnitType);
            for (int i = 0; i < finderSC.Length; i++)
            {
                finderSC[i].SetSelectedWorldUnitType(iSelectedUnitType);
            }
        }
    }

    public int GetSelectedUnitType()
    {
        return iSelectedUnitType;
    }
    
    public void SetSelectedUnitType(int _unitType)
    {
        iSelectedUnitType = _unitType;
    }
}
