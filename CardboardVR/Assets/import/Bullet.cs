using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 moveDir;
    public float moveSpeed;
    public GameObject hit_effect;
    public float LifeTime = 10f;
    

    private float record = 0f;
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

    private void FixedUpdate()
    {
        transform.position += moveDir * Time.deltaTime * moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMovement>().got_hit();
            Debug.Log("hit enemy");
            StartCoroutine(_wait());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyMovement>().got_hit();
            Debug.Log("hit enemy");
            StartCoroutine(_wait());
        }
    }

    IEnumerator _wait()
    {
        GameObject obj;
        obj = Instantiate(hit_effect, transform.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }
}
