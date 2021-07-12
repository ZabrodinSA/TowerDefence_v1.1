using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Control Script/MuzzleFlashController")]
public class MuzzleFlashController : MonoBehaviour
{
    public float timeLive = 1.0f;
    private SpriteRenderer[] spriteRenderers;
    private Light light;

    

    void Start()
    {
        if (gameObject.GetComponentsInChildren<SpriteRenderer>() != null)    //проверяем есть ли компонент SpriteRenderer и Light у gameObject
        {
            spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        }

        if (gameObject.GetComponentInChildren<Light>() != null)
        {
            light = gameObject.GetComponentInChildren<Light>();
        }

    }

    
    void Update()
    {
        if (spriteRenderers != null)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)        //уничтожаем визуальные эффекты
            {
                Destroy(spriteRenderers[i], timeLive * 0.075f);
            }
        }
        if (light != null)
        {
            Destroy(light, timeLive * 0.075f);
        }
        Destroy(gameObject, timeLive);                              //после уничтожаем весь gameOnject
    }
}
