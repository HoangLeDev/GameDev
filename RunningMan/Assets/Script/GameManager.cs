using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform startingPoint;
    public float lerpSpeed;
    public int score;
    public bool isGameActive;
    public bool is5Score;
    public bool is10Score;
    public Text scoreText;
    public Text additionScoreText;
    public GameObject StartPanel;
    public GameObject GameUIPanel;
    private SpawnObstacle spawnOb;
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnOb = GameObject.Find("SpawnObstacle").GetComponent<SpawnObstacle>();
        isGameActive=false;
    }

    void Update()
    {
        StartCoroutine(DisplayAdditionScore());
        scoreText.text = "Score: "+ score;
    }

    IEnumerator PlayIntro()
    {
        if(isGameActive)
        {
            Vector3 startPos = playerControllerScript.transform.position;
            Vector3 endPos = startingPoint.position;
            float journeyLength = Vector3.Distance(startPos, endPos);
            float startTime = Time.time;
            float distanceCovered = (Time.time - startTime) * lerpSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiply",0.5f);
            while (fractionOfJourney < 1)
            {
                distanceCovered = (Time.time - startTime) * lerpSpeed;
                fractionOfJourney = distanceCovered / journeyLength;
                playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
                yield return null;
            }
        }
    }

    IEnumerator DisplayAdditionScore()
    {
        if(is10Score)
        {
            additionScoreText.text ="+10";
            additionScoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.3f);
            additionScoreText.gameObject.SetActive(false);
            is10Score=false;
        }
        else if(is5Score)
        {
            additionScoreText.text ="+5";
            additionScoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.3f);
            additionScoreText.gameObject.SetActive(false);
            is5Score=false;
        }
    }

    public void StartGame()
    {
        isGameActive=true;
        StartPanel.SetActive(false);
        GameUIPanel.SetActive(true);
        StartCoroutine(PlayIntro());
        spawnOb.StartSpawn();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        #if(UNITY_EDITOR)
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif        
    }

}
