using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem dirtFlame;
    public int jumpHeight=8;
    public float gravityModifier;
    private bool isOnGround;
    public bool gameOver;
    private Animator playerAni;
    public AudioClip jumpSound;
    public AudioClip getScoreSound;
    public AudioClip crashSound;
    public AudioSource playerAudio;
    private bool dobJump;
    public float powerDash;
    public bool doubSpeed;
    public GameObject GameOverPanel;
    public GameObject playerButton;
    GameManager gMgr;

    // Start is called before the first frame update
    void Start()
    {   
        playerRb = GetComponent<Rigidbody>();
        playerAni = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
        gravityModifier=1;
        Physics.gravity *= gravityModifier;
        dobJump=false;
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    public void GameOver()
    {
        if(gameOver==true)
        {
            gMgr.isGameActive=false;
            playerAni.SetBool("Death_b",true);
            playerAni.SetInteger("DeathType_int",1);
            dirtParticle.Stop();
            dirtFlame.Stop();
            GameOverPanel.SetActive(true);
            playerButton.SetActive(false);
        }
    }
    public void PlayerJump()
    {
        //Normal Jump
        if(isOnGround&&gameOver!=true)
        {
            playerRb.AddForce(Vector3.up*jumpHeight, ForceMode.Impulse);
            isOnGround=false;
            playerAudio.PlayOneShot(jumpSound , 0.4f);
            dirtParticle.Stop();
            dobJump=true;
        }
        //Double Jump
        else if(dobJump && !isOnGround)
        {
            dobJump=false;
            playerAni.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.up*jumpHeight*0.5f, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound , 0.4f);
        }
    }
    public void PlayerDasHold()
    {
        StartCoroutine(FlameDisplay());
        doubSpeed=true;
        playerAni.SetFloat("Speed_Multiply",2f);
    }

    public void PlayerDashRelease()
    {
        doubSpeed=false;
        playerAni.SetFloat("Speed_Multiply",1f);
    }

    IEnumerator FlameDisplay()
    {
        if(!dirtFlame.isPlaying)
        {
            dirtFlame.Play();
            yield return new WaitForSeconds(0.5f);
            dirtFlame.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if(collision.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Game Over!");
            if(!explosionParticle.isPlaying)
            {
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSound ,0.8f);
            }
            gameOver=true;
        }
    }
}
