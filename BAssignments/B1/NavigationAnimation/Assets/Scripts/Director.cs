using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {

    GameObject unit;
    GameObject[] unitArr;
    int selCount = 0;
    bool checker = false;
    RaycastHit lastHit;
    Vector3 dummy = new Vector3(0, 0, 0);

	
    void Start()
    {
        unitArr = new GameObject[GameObject.FindGameObjectsWithTag("Agent").Length];
        lastHit.point = dummy;
    }

	// Update is called once per frame
	void Update () {
        RaycastHit hitPos;
        Ray click = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(click, out hitPos))
        {
            if(hitPos.transform.tag == "Agent" && Input.GetMouseButtonDown(0))
            {
                unit = hitPos.transform.gameObject;

                //check to see if unit already selected
                for(int a = 0; a <= selCount; a++)
                {
                    if (unit.Equals(unitArr[a])) checker = true;
                }
                //adds unit to array, "selects" it
                if (!checker)
                {
                    unit.SendMessage("Select", 1);
                    Debug.Log(selCount);
                    unitArr[selCount] = unit;
                    selCount++; checker = false;
                }
            }
            //use right-click to move!
            if(Input.GetMouseButtonDown(1))
            {
                for (int a = 0; a < selCount; a++)
                {
                    unitArr[a].SendMessage("Destination", hitPos.point);
                    //if double-click same point, run!
                    if (lastHit.point == hitPos.point)
                    {
                        unitArr[a].SendMessage("RunDude", 1);
                    }
                }
                lastHit.point = hitPos.point;
            }
            //If L-click anything but an agent, deselects ALL agents
            if(hitPos.transform.tag != "Agent" && Input.GetMouseButtonDown(0))
            {
                if(unitArr[0] != null)
                for (int a = 0; a < selCount; a++) 
                    unitArr[a].SendMessage("Deselect", 1);
                System.Array.Clear(unitArr, 0, selCount);
                selCount = 0;
            }
        }
    }
}
