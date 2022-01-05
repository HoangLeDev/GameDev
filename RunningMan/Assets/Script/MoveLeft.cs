using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float moveSpeed=5;
    GameManager gMgr;
    PlayerController playerControllerScript;
    // Update is called once per frame
    void Start()
    {
        moveSpeed = 15;
        playerControllerScript= FindObjectOfType<PlayerController>();
        gMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
        ////OR can use bellow: Get Object Player from Hierarchy, then get component PlayerController
        //playerControllerScript= GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if(gMgr.isGameActive && playerControllerScript.gameOver == false)
        {
            if(playerControllerScript.doubSpeed)
            {
                transform.Translate(Vector3.left*Time.deltaTime*moveSpeed*1.5f);
            }
            else
            {
                transform.Translate(Vector3.left*Time.deltaTime*moveSpeed);
            }
        }
        if((gameObject.CompareTag("Obstacles")||gameObject.CompareTag("DoubleCrate"))&&transform.position.x<-4)
        {
            Destroy(gameObject);
            playerControllerScript.playerAudio.PlayOneShot(playerControllerScript.getScoreSound,0.4f);
            if(gameObject.CompareTag("DoubleCrate"))
            {
                gMgr.score+=10;
                gMgr.is10Score=true;
            }
            else 
            {
                gMgr.score +=5;
                gMgr.is5Score=true;
            }
        }
    }
}
