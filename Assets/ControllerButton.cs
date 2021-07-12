using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class ControllerButton : MonoBehaviour
{
    public GameObject clickObject;
    private GameObject floor;
    private NavMeshSurface navigationFloor;
    
    public void CreateObject ()
    {
        floor = GameObject.FindGameObjectWithTag("Floor");
        navigationFloor = floor.GetComponent<NavMeshSurface>();
        navigationFloor.BuildNavMesh();
        //Instantiate(clickObject, transform.position, transform.rotation);

    }
}
