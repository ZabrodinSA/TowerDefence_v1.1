using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[AddComponentMenu("Control Script/TurellController")]
public class TuretController : MonoBehaviour
{
    public float viewRadius = 5;  
    //public float speedRotation = 1.0f;
    public GameObject muzzleFlash;
    public float shotPeriod = 1.0f;  
    public GameObject shotObject;
    public Vector3[] targetLocation; 
    private GameObject currentTarget; 
    public string tagTarget = "Target";
    private float shutCounter = 0;  
    void Start()
    {
        if (shotObject.GetComponent<BulletController>() == null)    //проверяем есть ли компонент NavMeshAgent у spawnObject
        {
            Debug.Log("shotObject don`t have BulletController component!");
            throw new ArgumentNullException("shotObject don`t have BulletController component!");
        }
    }
        
    void Update()
    {
        if (currentTarget == null) //проверяем есть ли цель
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(tagTarget); //если нет то ищем первую в радиусе видимости
            targetLocation = new Vector3[targets.Length];
            for (int i = 0; i < targetLocation.Length; i++)
            {
                if ((targets[i].transform.position - transform.position).magnitude < viewRadius)   
                {
                    currentTarget = targets[i];
                    break;
                }
            }
        }
        else
        {
            if ((currentTarget.transform.position - transform.position).magnitude > viewRadius) currentTarget = null; //если текущая цель вне радиуса видимости обнуляем её
            else // иначе поворачиваемся на цель и стреляем
            {
                transform.LookAt(currentTarget.transform);
                Quaternion quaternion = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                transform.rotation = quaternion;
                Debug.DrawRay(transform.position, transform.forward);
                Debug.DrawRay(transform.position, currentTarget.transform.position - transform.position);

                if (shutCounter > shotPeriod)
                {
                    Shot();
                    shutCounter = 0;
                }
            }
        }
        shutCounter += Time.deltaTime;
    }

    private void Shot()
    {
        GameObject bullet = Instantiate(shotObject, transform.position+transform.forward*1.5f, transform.rotation);
        BulletController bulletController=bullet.GetComponent<BulletController>();
        bulletController.target = currentTarget;
        GameObject muzzleflash = Instantiate(muzzleFlash, transform.position + transform.forward * 2.5f, transform.rotation);
    }
}
