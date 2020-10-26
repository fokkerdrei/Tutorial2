using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private bool facingRight = true;
    private Rigidbody2D rd2d;

    public float speed;

    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    public Text scoreText;
    public Text winText;
    public Text livesText;
    public Text loseText;
    private int score;
    private int lives;

    Animator anim;

    


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score = 0;
        SetScoreText ();
        winText.text = "";
        lives = 3;
        SetLivesText ();
        loseText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("Coin"))
        {
            other.gameObject.SetActive (false);
            score = score + 1;
            SetScoreText ();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            lives = lives - 1;
            SetLivesText ();
        }
        if (score == 4)
        {
            transform.position = new Vector3(40.0f, -0.2f, 0f);
            lives = 3;
        }
    }

    void SetScoreText ()
    {
        scoreText.text = "Score: " + score.ToString ();
        if (score == 8)
        {
            winText.text = "You Win! Created by Connor Nolen";
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
        }
    }

    void SetLivesText ()
    {
        livesText.text = "Lives: " + lives.ToString ();
        if (lives <= 0)
        {
            loseText.text = "You Lose!";
            gameObject.SetActive (false);
        }
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
          Application.Quit();
        }
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
