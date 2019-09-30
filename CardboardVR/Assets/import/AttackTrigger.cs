using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    AudioSource _as;
    public GameObject hiteffect;
    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Hit");
            _as.Play();
            other.GetComponent<Player>().hit();
            if(hiteffect != null)
            {
                StartCoroutine(hitVFX());
            }
        }
    }

    IEnumerator hitVFX()
    {
        GameObject obj;
        obj = Instantiate(hiteffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        Destroy(obj);
    }
}
