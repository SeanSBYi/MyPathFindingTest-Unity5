using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectUnit : MonoBehaviour {

    public GameObject selectedUnit;
    public List<GameObject> selectedUnits = new List<GameObject>();
    public bool isDragging;
    public List<GameObject> UnitsOnScreenSpace = new List<GameObject>();
    public List<GameObject> UnitInDrag = new List<GameObject>();

    private RaycastHit hit;
    private Vector3 MouseDownPoint, CurrentDownPoint;
    private float BoxWidth, BoxHeight, BoxTop, BoxLeft;
    private Vector2 BoxStart, BoxFinish;

    private void OnGUI()
    {
        if(isDragging)
        {
            GUI.Box(new Rect(BoxLeft, BoxTop, BoxWidth, BoxHeight), "");
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if(hit.transform.tag != "SelectableUnit")
                {
                    if (CheckIfMouseIsDragging())
                    {
                        isDragging = true;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            PutUnitsFromDragIntoSelectedUnits();
            isDragging = false;
        }

        //
        if (selectedUnit == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.tag == "SelectableUnit")
                    {
                        selectedUnit = hit.transform.gameObject;
                        selectedUnit.transform.FindChild("Marker").gameObject.SetActive(true);
                        for (int i = 0; i < selectedUnits.Count; i++)
                        {
                            selectedUnits[i].transform.FindChild("Marker").gameObject.SetActive(false);
                        }
                        selectedUnits.Clear();
                    }
                }
                if (hit.transform.tag == "Ground")
                {
                    for (int i = 0; i < selectedUnits.Count; i++)
                    {
                        selectedUnits[i].transform.FindChild("Marker").gameObject.SetActive(false);
                    }
                    selectedUnits.Clear();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.tag == "SelectableUnit")
                    {
                        selectedUnit.transform.FindChild("Marker").gameObject.SetActive(false);
                        selectedUnit = null;
                        selectedUnit = hit.transform.gameObject;
                        selectedUnit.transform.FindChild("Marker").gameObject.SetActive(true);
                    }
                }
                if (hit.transform.tag == "Ground")
                {
                    selectedUnit.transform.FindChild("Marker").gameObject.SetActive(false);
                    selectedUnit = null;
                }
            }
        }

        //
        if(Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //if (selectedUnit != null)
                //{
                //    selectedUnits.Add(selectedUnit);
                //    selectedUnit = null;
                //}
                //selectedUnits.Add(hit.transform.gameObject);
                //for(int i = 0; i < selectedUnits.Count; i++)
                //{
                //    selectedUnits[i].transform.FindChild("Marker").gameObject.SetActive(true);
                //}
                if (hit.transform.tag == "SelectableUnit")
                {
                    if (selectedUnit != null)
                    {
                        selectedUnits.Add(selectedUnit);
                        selectedUnit = null;
                    }
                    selectedUnits.Add(hit.transform.gameObject);
                    for (int i = 0; i < selectedUnits.Count; i++)
                    {
                        selectedUnits[i].transform.FindChild("Marker").gameObject.SetActive(true);
                    }
                }
            }
        }

        //
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            CurrentDownPoint = hit.point;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                MouseDownPoint = hit.point;
            }
        }
        if(isDragging)
        {
            BoxWidth = Camera.main.WorldToScreenPoint(MouseDownPoint).x - Camera.main.WorldToScreenPoint(CurrentDownPoint).x;
            BoxHeight = Camera.main.WorldToScreenPoint(MouseDownPoint).y - Camera.main.WorldToScreenPoint(CurrentDownPoint).y;
            BoxLeft = Input.mousePosition.x;
            BoxTop = (Screen.height - Input.mousePosition.y) - BoxHeight;

            if (BoxWidth > 0f && BoxHeight <0f)
            {
                BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else if (BoxWidth > 0f && BoxHeight > 0f)
            {
                BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + BoxHeight);
            }
            else if (BoxWidth < 0f && BoxHeight < 0f)
            {
                BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y);
            }
            else if (BoxWidth < 0f && BoxHeight > 0f)
            {
                BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y + BoxHeight);
            }
            BoxFinish = new Vector2(BoxStart.x + Unsigned(BoxWidth), BoxStart.y - Unsigned(BoxHeight));
        }
    }

    float Unsigned(float val)
    {
        if (val < 0f)
            val *= -1;
        return val;
    }

    private bool CheckIfMouseIsDragging()
    {
        if(CurrentDownPoint.x - 2 >= MouseDownPoint.x || CurrentDownPoint.y - 2 >= MouseDownPoint.y || CurrentDownPoint.z - 2 >= MouseDownPoint.z ||
           CurrentDownPoint.x < MouseDownPoint.x - 2 || CurrentDownPoint.y < MouseDownPoint.y - 2 || CurrentDownPoint.z < MouseDownPoint.z - 2 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UnitWithinScreenSpace(Vector2 UnitScreenPos)
    {
        if ((UnitScreenPos.x > Screen.width && UnitScreenPos.y < Screen.height) && (UnitScreenPos.x > 0f && UnitScreenPos.y < 0f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UnitWithinDrag(Vector2 UnitScreenPos)
    {
        if ((UnitScreenPos.x > BoxStart.x && UnitScreenPos.y < BoxStart.y) && (UnitScreenPos.x < BoxFinish.x && UnitScreenPos.y > BoxFinish.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PutUnitsFromDragIntoSelectedUnits()
    {
        if(UnitInDrag.Count > 0)
        {
            for(int i = 0; i <UnitInDrag.Count; i++)
            {
                if(!selectedUnits.Contains(UnitInDrag[i]))
                {
                    selectedUnits.Add(UnitInDrag[i]);
                }
            }
        }
        UnitInDrag.Clear();
    }
}
