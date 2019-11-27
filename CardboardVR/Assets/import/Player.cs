using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [Header("Player Info")]
    public GameObject[] bullet;
    public int bulletType = 0;
    public int hp = 100;
    public int maxhp = 100;
    public float moveSpeed = 3f;
    /*
    public Text hpText;
    */
    public GameObject GameOverUI;
    public bool isDead = false;
    public Text ScoreText;

    [Header("Spell Info")]
    public GameObject spellFX;
    public int spellN = 10;
    public float spellPeriod = 0.5f;
    public float spellTime = 3f;
    
    private int score = 0;
    private AudioSource _as;
    private EnemySpawner es;
    private float ft, lt;
    private bool isStart = false;
    private Vector3 moveVec;
    //private CardboardHead head;

    public void changeBulletType(int type)
    {
        bulletType = type;
    }

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
        es = GetComponent<EnemySpawner>();
        //head = Camera.main.GetComponent<StereoController>().Head;
        GameOverUI.SetActive(false);
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveVec = Vector3.zero;
        if(Vector3.Dot(Camera.main.transform.forward, Vector3.down) >= Mathf.Sqrt(2) / 2 && isStart)
        {
            Vector3 dir = Camera.main.transform.forward;
            dir.y = 0;
            //transform.Translate(moveSpeed * Time.deltaTime * dir);
            moveVec += moveSpeed * Time.deltaTime * dir;
        }

        if (Vector3.Dot(Camera.main.transform.right, Vector3.down) >= Mathf.Sqrt(2) / 2 && isStart)
        {
            Vector3 dir = Camera.main.transform.right;
            dir.y = 0;
            //transform.Translate(moveSpeed * Time.deltaTime * dir);
            moveVec += moveSpeed * Time.deltaTime * dir;
        }

        if (Vector3.Dot(-Camera.main.transform.right, Vector3.down) >= Mathf.Sqrt(2) / 2 && isStart)
        {
            Vector3 dir = -Camera.main.transform.right;
            dir.y = 0;
            //transform.Translate(moveSpeed * Time.deltaTime * dir);
            moveVec += moveSpeed * Time.deltaTime * dir;
        }

        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            ft = Time.time;
            launch();
            isStart = true;
        }
        
        if(Input.GetMouseButtonUp(0) && !isDead)
        {
            lt = Time.time;
            if (lt - ft > spellTime)
            {
                spell();
            }
            lt = 0;
            ft = 0;
        }


        if (Input.GetMouseButtonDown(0) && isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
    }


    private void LateUpdate()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveVec);
    }
    public void launch()
    {
        _as.Play();
        GameObject obj;
        obj = Instantiate(bullet[bulletType], transform.position + Camera.main.transform.forward, transform.rotation);
        obj.GetComponent<Bullet>().moveDir = Camera.main.transform.forward;
    }

    public void spell()
    {
        StartCoroutine(_spell(0));
        StartCoroutine(_spell(1));
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
        isStart = false;
    }

    IEnumerator _spell(int spellType)
    {
        if(spellType == 0)
        {
            for(int i = 0; i < spellN; i++)
            {
                float angle = Random.Range(0f, 360f);
                float x = es.spawnDis * Mathf.Cos(angle);
                float y = es.spawnDis * Mathf.Sin(angle);
                Vector3 pos = new Vector3(transform.position.x + x, 0, transform.position.z + y);
                Instantiate(spellFX, pos, transform.rotation);
                yield return new WaitForSeconds(spellPeriod);
            }
        }
        else
        {
            yield return new WaitForSeconds(spellPeriod);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject obj in enemies)
            {
                Instantiate(spellFX, obj.transform.position, transform.rotation);
                obj.GetComponent<EnemyMovement>().toDead();
            }
        }
    }
}
