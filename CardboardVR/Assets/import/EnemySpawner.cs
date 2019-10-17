using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int limit = 3;
    public float spawnDis = 10f;
    public GameObject[] EnemyType;
    public GameObject[] ItemType;
    public float time_threshold = 100f;
    public int upper_bound = 10;
    public float spawnPeriod = 5f;

    private int amount = 0;
    private float time_record = 0;
    public float _time_record = 0;
    private bool itemexist = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void decreaseEnemy()
    {
        amount--;
    }

    public void getItem()
    {
        itemexist = false;
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
                if(spawnPeriod - 0.2f > 0)
                {
                    spawnPeriod -= 0.2f;
                }
            }
        }

        if(itemexist == false && (int)_time_record % 29 == 0 && ItemType.Length > 0)
        {
            int type = Random.Range(0, ItemType.Length);
            float angle = Random.Range(0f, 360f);
            float x = spawnDis * Mathf.Cos(angle);
            float y = spawnDis * Mathf.Sin(angle);
            Vector3 pos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + y);
            Instantiate(ItemType[type], pos, transform.rotation);
            itemexist = true;
        }

        if (amount < limit && _time_record >= spawnPeriod)
        {
            Debug.Log("Spawn");
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
