using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Character : MonoBehaviour {
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    
    [SerializeField]
    private float speed;
    private bool lookRight;

    public Text sumCoins;
    public Text HpText;

    private int coins;
    private int hp = 3;

    private bool Live;

    [SerializeField]
    private AudioSource hpDownSound;

    [SerializeField]
    private Transform[] temasNoktasi;
    [SerializeField]
    private float temasCapi;
    [SerializeField]
    private LayerMask hangiZemin;

    private bool zeminde;
    private bool zipla;
    [SerializeField]
    private bool havadaKontrol;
    [SerializeField]
    private float ziplamaKuvveti;

    float timeJump = 1.5f;
    float timeDeath = 0.8f;
    public GameObject gameOver;

    public string NextLevelName;
    public string RestartLevelName;

    void Start () {
        lookRight = true;
        Live = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        HpWrite(hp);
    }

    private void DegerleriSifirla()
    {
        zipla = false;
    }

    private bool Zeminde()
    {
        if(myRigidbody.velocity.y<=0)
        {
            foreach (Transform nokta in temasNoktasi)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(nokta.position, temasCapi, hangiZemin);
                for(int i=0;i<colliders.Length;i++)
                {
                    if(colliders[i].gameObject!=gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(Live) { 
        float yatay = Input.GetAxis("Horizontal");
        zeminde = Zeminde();
        CharacterControll(yatay);
        TransformCharacter(yatay);
        DegerleriSifirla();
        } else
        {
            timeDeath -= Time.deltaTime;
            if(timeDeath<0)
            {
                myAnimator.SetFloat("karakterDeath", Mathf.Abs(0.0f));
                myAnimator.enabled = false;
            }
        }
    }

    private void CharacterControll(float yatay)
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            zipla = true;
        }

        myRigidbody.velocity = new Vector2(yatay * speed, myRigidbody.velocity.y);
        myAnimator.SetFloat("karakterHizi", Mathf.Abs(yatay));

        if (zeminde && zipla)
        {
            zipla = false;
            myRigidbody.AddForce(new Vector2(0, ziplamaKuvveti));
        }


    }

    private void TransformCharacter(float yatay)
    {
        if(yatay>0 && !lookRight || yatay <0 && lookRight)
        {
            lookRight = !lookRight;
            Vector3 yon = transform.localScale;
            yon.x *= -1;
            transform.localScale = yon;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "coin") {
            DestroyObject(other.gameObject);
            coins++;
            CoinsWrite(coins);
        }

        if(other.gameObject.tag=="cactus1")
        {
            HpControl();
            HpWrite(hp);
        }

        if(other.gameObject.tag=="gameover")
        {
            hp = 0;
            HpControl();
            HpWrite(hp);
        }

        if(other.gameObject.tag=="nextlevel")
        {
            SceneManager.LoadScene(NextLevelName, LoadSceneMode.Single);
        }
    }

    void CoinsWrite(int count) {
        sumCoins.text = count.ToString();
    }

    void HpWrite(int count)
    {
        HpText.text = count.ToString();
    }

    void HpControl()
    {
        if(hp>1)
        {
            hp--;
            hpDownSound.Play();
        } else
        {
            hp = 0;
            Live = false;
            myAnimator.SetFloat("karakterHizi", Mathf.Abs(0.0f));
            myAnimator.SetFloat("karakterDeath", Mathf.Abs(1f));
            gameOver.SetActive(true);
        }
    }

   public void MainMenuBtn()
    {
        SceneManager.LoadScene(RestartLevelName, LoadSceneMode.Single);
    }
}
