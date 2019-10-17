using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Player p;
    public int type = 1;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Debug.Log("Item get!");
            p.changeBulletType(type);
            p.gameObject.GetComponent<EnemySpawner>().getItem();
            GameObject obj = Instantiate(effect, transform.position, transform.rotation);
            obj.AddComponent<Effect>();
            Destroy(gameObject);
        }
    }
}
