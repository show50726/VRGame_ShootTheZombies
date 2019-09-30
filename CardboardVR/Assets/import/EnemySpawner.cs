﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int limit = 3;
    public float spawnDis = 10f;
    public GameObject[] EnemyType;
    public float time_threshold = 100f;
    public int upper_bound = 10;
    public float spawnPeriod = 5f;

    private int amount = 0;
    private float time_record = 0;
    private float _time_record = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time_record += Time.deltaTime;
        _time_record += Time.deltaTime;

        if(limit < upper_bound)
        {
            if(time_record >= time_threshold)
            {
                limit++;
                time_threshold *= 1.5f;
                time_record = 0;
            }
        }

        if (amount < limit && _time_record >= spawnPeriod)
        {
            float angle = Random.Range(0f, 360f);
            float x = spawnDis * Mathf.Cos(angle);
            float y = spawnDis * Mathf.Sin(angle);
            int type = Random.Range(0, EnemyType.Length);
            Vector3 pos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y);
            Instantiate(EnemyType[type], pos, transform.rotation);
            amount++;
            _time_record -= spawnPeriod;
        }


    }
}
