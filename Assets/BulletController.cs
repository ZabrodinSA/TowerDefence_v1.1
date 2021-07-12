using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[AddComponentMenu("Control Script/BulletController")]
public class BulletController : MonoBehaviour
{
    public GameObject target; 
    public GameObject visualDestructionEffect; 
    public GameObject visualCreationEffect;    
    public float speed = 1.0f;
    public float removalDistance = 1.0f;  
    private bool aliveTarget;
    private CapsuleController capsuleController;
    private Quaternion quaternion = new Quaternion(0, 0, 0, 0);

    void Start()
    {
        if (target.GetComponent<CapsuleController>() == null)    //проверяем есть ли компонент CapsuleController у target
        {
            Debug.Log("target don`t have CapsuleController component!");
            throw new ArgumentNullException("target don`t have CapsuleController component!");
        }
        if (visualDestructionEffect.GetComponent<CapsuleController>() == null)    //проверяем есть ли компонент CapsuleController у target
        {
            Debug.Log("target don`t have CapsuleController component!");
            throw new ArgumentNullException("target don`t have CapsuleController component!");
        }

    }

    void Update()
    {
        
        transform.rotation = quaternion;        //приводим вращение пули к "0" и направляем её к цели
        transform.Translate((target.transform.position-transform.position).normalized * speed*Time.deltaTime); 
        
        capsuleController = target.GetComponent<CapsuleController>();
        aliveTarget = capsuleController.alive;
        
        //проверяем долетела ли пуля или уничтожилась ли цель
        if ((target.transform.position - transform.position).magnitude < removalDistance || !aliveTarget||target==null)
        {
            if (visualDestructionEffect!=null) Instantiate(visualDestructionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        } 
            
        Debug.DrawRay(transform.position, target.transform.position - transform.position);
    }
}
