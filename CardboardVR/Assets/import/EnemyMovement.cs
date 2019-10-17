using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public int maxhp = 100;
    public int hp = 100;
    public bool isDead = false;
    public Animator anim;
    private AudioSource _as;
    void Start()
    {
        _as = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void got_hit()
    {
        _as.Play();

        hp -= 30;
        
        if(hp <= 0 && !isDead)
        {
            isDead = true;
            Dead();
        }
    }
    
    public void Dead()
    {
        anim.SetBool("isDead", true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().addScore(10);
        GameObject.FindGameObjectWithTag("Player").GetComponent<EnemySpawner>().decreaseEnemy();
        StartCoroutine(_wait(5f));
    }

    IEnumerator _wait(float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }

}
