using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[AddComponentMenu("Control Script/CapsuleController")]
public class CapsuleController : MonoBehaviour
{
    NavMeshAgent agent;
    public float distanceRemuve = 0.5f;
    public Vector3 destination = new Vector3(0, 0, 0);
    public bool alive = true;
    
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - agent.destination).magnitude<distanceRemuve) //уничтожаем цель если она достигла финиша
        {
            alive = false;
            Destroy(gameObject);
        }
    }


}
