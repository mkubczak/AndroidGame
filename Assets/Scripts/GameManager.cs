using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private const float COIN_SCORE_AMOUNT = 5.0f;

    public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }
    private bool isGameStarted;
    private MotorPlayer motor;


    // UI
    public Text scoreText, coinText, modifierText;
    private float score, lastScore, coinScore, modifierScore;

    //Menu Dead
    public Animator deathMenuAnim;
    public Text deadScoreText, deadDiamondText;

    GameObject pausebutton;

  

    private void Awake()
    {
       
        Instance = this;
        modifierScore = 1;

        scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString("0");
        modifierText.text = "x" + modifierScore.ToString("0.0");

        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<MotorPlayer>();
        pausebutton = GameObject.Find("PauseButton");
        pausebutton.SetActive(false);
    }

       
    private void Update()
    {
        if (SwipeController.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            pausebutton.SetActive(true);
            motor.StartRunning();
        }

            if (isGameStarted)
          {
            score += (Time.deltaTime * modifierScore);


            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
                
            }
           
           
        } 
       
    }

    public void GetCoin()
    {
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = score.ToString("0");

    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("AndroidGame");
    }

    public void OnDeath()
    {
        IsDead = true;
        deadDiamondText.text = score.ToString("0");
        deadScoreText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
        pausebutton.SetActive(true);
    }
}