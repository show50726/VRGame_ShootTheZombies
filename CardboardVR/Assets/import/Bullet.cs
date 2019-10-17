using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 moveDir;
    public float moveSpeed;
    public GameObject hit_effect;
    public float LifeTime = 10f;
    public int type = 1;
    public float r = 0.1f;

    private float record = 0f;
    private float startY = 0f;
    private float startAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        startAngle = Mathf.Abs(moveDir.y) / moveDir.sqrMagnitude;
        //startAngle = -Camera.main.transform.eulerAngles.x;
        Debug.Log("angle: " + startAngle);
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
        if(type == 2)
        {
            transform.position += new Vector3(r * Mathf.Cos(record%360 * 10), 0, 0);
        }
        else if(type == 3)
        {
            transform.position = new Vector3(transform.position.x, startY + moveSpeed * 2 * startAngle * record - 0.5f * 9.8f * record * record, transform.position.z);
        }
        
    }

    private void LateUpdate()
    {
        transform.position += moveDir * Time.deltaTime * moveSpeed;

        if(transform.position.y <= 0 && record >= 0.5f)
        {
            StartCoroutine(_wait());
        }
        //Debug.Log(transform.position.y);
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
        Instantiate(hit_effect, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
