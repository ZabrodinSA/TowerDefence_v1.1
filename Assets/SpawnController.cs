using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[AddComponentMenu("Control Script/SpawnController")]
public class SpawnController : MonoBehaviour
{
    public float spawnPeriod = 1;           
    private float spawnTime = 0.0f;     
    public GameObject spawnObject;
    private uint spawnIndex = 0;
    public uint maximumSpawn = 10;  
    public string tagFinish = "finish_one";
    public Vector3[] finishLocation;
    void Start()
    {
        GameObject[] finishes =  GameObject.FindGameObjectsWithTag(tagFinish); //достаём объекты с тегом TagFinish
        finishLocation = new Vector3[finishes.Length];                //создаём массив векторов с нужным количеством полей
        if (spawnObject.GetComponent<NavMeshAgent>()==null)    //проверяем есть ли компонент NavMeshAgent у spawnObject
        {
            Debug.Log("spawnObject don`t have NavMeshAgent component!");
            throw new ArgumentNullException("spawnObject don`t have NavMeshAgent component!");
        }
        for (int i = 0; i < finishes.Length; i++)       // записываем положения объектов с тэгом TagFinish
        {
            GameObject finish = (GameObject)finishes[i];
            finishLocation[i] = finish.transform.position;
        }
    }

    void Update()
    {
        if (Time.time > spawnTime && spawnIndex<maximumSpawn)    //если в данном периоде не было создано объекта и не превышенно их количество создаём
        {                                                        //и по очереди направляем к объектам с тэгом tagFinish
            GameObject spawnedObject=Instantiate(spawnObject, transform.position, transform.rotation);   
            NavMeshAgent navMeshAgent = spawnedObject.GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(finishLocation[spawnIndex%finishLocation.Length]);
            spawnIndex++;
            spawnTime += spawnPeriod;
        }
    }
}
