using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject[] bullet;
    public int bulletType = 0;
    public int hp = 100;
    public int maxhp = 100;
    /*
    public Text hpText;
    */
    public GameObject GameOverUI;
    public bool isDead = false;
    public Text ScoreText;
    
    private int score = 0;
    private AudioSource _as;
    //private CardboardHead head;

    public void changeBulletType(int type)
    {
        bulletType = type;
    }

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
        //head = Camera.main.GetComponent<StereoController>().Head;
        GameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            //Debug.Log("!!!");
            launch();
        }

        if(Input.GetMouseButtonDown(0) && isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }

    public void launch()
    {
        _as.Play();
        GameObject obj;
        obj = Instantiate(bullet[bulletType], transform.position + Camera.main.transform.forward, transform.rotation);
        obj.GetComponent<Bullet>().moveDir = Camera.main.transform.forward;
    }


    public void hit()
    {
        hp -= 10;
        hp = hp < 0 ? 0 : hp;
        //hpText.text = "HP : " + hp;
        if(hp <= 0)
        {
            dead();
        }
    }

    public void addScore(int x)
    {
        score += x;
        ScoreText.text = "Points : " + score;
    }

    public void dead()
    {
        isDead = true;
        //ScoreText.text = "Your Score : " + score;
        GameOverUI.SetActive(true);
    }
}
