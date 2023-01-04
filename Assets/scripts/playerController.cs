using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{

    Vector2 controlThrow;
    Rigidbody2D myRigidbody2D;
    PlayerInput playerInput;
    Vector2 OriginPos;

    //InputHandler inputHandler;
    
    //Color red = new Color32(245, 94, 110, 225);

    GameObject hints;
    GameObject rScore;
    GameObject bScore;


    int redScore = 0;
    int blueScore = 0;
    int playerTag;
    bool move = false;

    [SerializeField] float ballSpeed = 10f;

    private void Awake()
    {
        hints=GameObject.FindGameObjectWithTag("hints");
        rScore = GameObject.FindGameObjectWithTag("red");
        bScore = GameObject.FindGameObjectWithTag("blue");
        //inputHandler = FindObjectOfType<InputHandler>();
        playerInput = GetComponent<PlayerInput>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        playerTag = playerInput.playerIndex;
        OriginPos = transform.position;
        //Debug.Log(playerCount);
        if (playerTag==1)
        {
            transform.position = new Vector2(4, 0);
            transform.rotation= Quaternion.Euler(0f, 0f, 180f);
            hints.SetActive(false);
            changeSpriteBySprite();
            OriginPos = transform.position;
            StartCoroutine(Start());
            //GetComponent<SpriteRenderer>().color = red;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Move();
            FlipSprite();
        }

        if (redScore==10||blueScore==10)
        {
            move = false;
            transform.position = OriginPos;
            StartCoroutine(Restart());
        }
    }



    public void Move()
    {
        Vector2 playerVelocity = new Vector2(controlThrow.x , controlThrow.y)* Time.deltaTime*ballSpeed;
         myRigidbody2D.velocity += playerVelocity;
        //transform.Translate(playerVelocity);
    }

    private void OnMove(InputValue value)
    {
        controlThrow = value.Get<Vector2>();
    }

    IEnumerator Restart()
    {      
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        move = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="walls"&& playerTag==1)
        {
            blueScore++;
            bScore.GetComponent<Text>().text = blueScore.ToString();
            //Debug.Log("BlueScore is"+blueScore);
        }
        if (collision.gameObject.tag == "walls" && playerTag == 0)
        {
            redScore++;
            rScore.GetComponent<Text>().text = redScore.ToString();
            //Debug.Log("RedScore is"+redScore);
        }
    }

    private void FlipSprite()
    {
        if (controlThrow.x!=0||controlThrow.y!=0)
        {
            float angle = Mathf.Atan2( (controlThrow.y - 0), (controlThrow.x - 0)) * 180 / Mathf.PI;           
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    void changeSpriteBySprite()
    {
        Sprite spriteB = Resources.Load<Sprite>("zombie");
        GetComponent<SpriteRenderer>().sprite = spriteB;
    }
}
