using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float LifeTime = 1f;
    private float record = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        record += Time.deltaTime;
        if(record >= LifeTime)
        {
            Destroy(gameObject);
        }
    }
}
