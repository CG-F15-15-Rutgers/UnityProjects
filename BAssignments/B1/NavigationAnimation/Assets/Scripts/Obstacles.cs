using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour
{


    GameObject obs;
    bool selected = false;
    CharacterController controller;


    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        RaycastHit hitPos;
        Ray click = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(click, out hitPos))
        {
            if (hitPos.transform.tag == "Obstacle" && Input.GetMouseButtonDown(0))
            {
                controller = (CharacterController)hitPos.transform.GetComponent<CharacterController>();
                selected = true;
            }
            if (hitPos.transform.tag != "Obstacle" && Input.GetMouseButtonDown(0))
            {
                selected = false;
            }

            if (selected)
            {
                float moveH = Input.GetAxis("Horizontal");
                float moveV = Input.GetAxis("Vertical");
                Vector3 move = new Vector3(2f*moveH*Time.deltaTime, 0f, 2f*moveV*Time.deltaTime);

                controller.Move(move);
            }
            if (!controller.isGrounded)
            {
                Vector3 grounder = new Vector3(0f, -1f, 0);
                controller.Move(grounder);
            }
        }
    }
}
