using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour {

    NavMeshAgent agent;
    bool selected = false;
    bool ready = false;
    Vector3 destination;
    

	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
	}

    void Update()
    {
        if(selected && ready)
        {
            ready = false;
            agent.SetDestination(destination);
        }
    }

    void Select(int x)
    {
        selected = true;
    }
    void Deselect(int x)
    {
        selected = false;
    }
    bool SelCheck(int x)
    {
        return (selected);
    }
    void Destination (Vector3 d)
    {
        destination = d;
        ready = true;
    }
}
