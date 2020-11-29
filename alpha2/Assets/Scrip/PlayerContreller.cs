using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerContreller : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 10;
    private Rigidbody playerRb;
    public float jumpForce = 400;
    private float gravityForce = 4;
    private float zRange = -3f;
    private GameObject mCamera;
    private Animator playeranim;
    public bool isOnGround = false;
    public bool faceRight = true;
    public bool gameOver = false;
    public bool canShoot= true;
    public bool canMove = true;
    private int numberOfB = 5;
    public GameObject shoutLoc;
    public GameObject bouletPrefab;
    public GameObject leftbouletPrefab;
    public GameObject ship;
    public GameObject onSceneShip;
    public Text numberDisplay;
    public Text numberofEnemy;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mCamera = GameObject.Find("cameraControl");
        shoutLoc = GameObject.Find("shootLocation");
        playeranim = GetComponent<Animator>();
        gameOver = false;
        if (Physics.gravity.y > -30) {
            Physics.gravity *= gravityForce;
        } 
    }

    // Update is called once per frame
    void Update()
    { 
        Vector3 characterScale = transform.localScale;
        mCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (mCamera.transform.position.y < -4.02) {
            mCamera.transform.position = new Vector3(transform.position.x, -4,transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround == true && gameOver == false && canMove == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playeranim.SetTrigger("jump_t");
            isOnGround = false;
        }
        if (transform.position.z < zRange || transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (Input.GetAxis("Horizontal") > 0 && gameOver == false && canMove == true)
        {
            transform.localEulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
            transform.Translate(0f, 0f, Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            faceRight = true;
            playeranim.SetBool("moving_b", true);
        }
        else if (Input.GetAxis("Horizontal") < 0 && gameOver == false && canMove == true)
        {
            transform.localEulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
            transform.Translate(0f, 0f, -Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            faceRight = false;
            playeranim.SetBool("moving_b", true);
        }
        else {
            playeranim.SetBool("moving_b", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && gameOver == false && canShoot == true && numberOfB >0) {
            playeranim.SetTrigger("shoot_t");
            if (faceRight == true) { 
                Instantiate(bouletPrefab, shoutLoc.transform.position, bouletPrefab.transform.rotation);
            }
            else
            {
                Instantiate(leftbouletPrefab, shoutLoc.transform.position, leftbouletPrefab.transform.rotation);
            }
            numberOfB -= 1;
            canShoot = false;
            StartCoroutine(shotback(0.6f));
        }
        if (Input.GetKeyDown(KeyCode.R) && numberOfB <5 && isOnGround == true)
        {
            canShoot = false;
            canMove = false;
            playeranim.SetTrigger("reload_t");
            StartCoroutine(Cooldown(1f));
        }
        if (numberOfB > 0) {
            numberDisplay.text = numberOfB.ToString();
        }
        if (numberOfB == 0) {
            numberDisplay.text = "0 Press R" ;
        }
        numberofEnemy.text = "Number of Enemy: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
        canMove = true;
        numberOfB += (5 - numberOfB);
    }
    IEnumerator shotback(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("world"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("underGround"))
        {
            gameOver = true;
            SceneManager.LoadScene("Gameover");
        }
        if (collision.gameObject.CompareTag("ship"))
        {
            Instantiate(ship, ship.transform.position, ship.transform.rotation);
            Destroy(gameObject);
            Destroy(onSceneShip);
        }
    }
}
