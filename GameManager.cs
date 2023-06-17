using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider actionSlider;
    public int abar_speed;
    public int abar_maxValue;
    public int abar_value;
    public bool pause;
    public float time = 0.05f;


    public Slider coolDownSlider;
    float coolDownTime;
    public float coolDownSliderSpeed;
    public enum barState {ChangeDirection ,Attack }
    public barState currentState;

    public int coins;
    public Text coin_Text;

    public int score;
    public Text score_Text;

    public float gameOverScreenShowDelay;
    public Transform gameOverScreen;

    public bool HasSetHighestScore = false;
    private void Start()
    {
        actionSlider.maxValue = abar_maxValue;
        actionSlider.value = abar_maxValue / 2;
        abar_value = abar_maxValue / 2;

        StartCoroutine(ReduceActionBarValue());

        coolDownTime = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().changeStateCooldownTime;
        coolDownSlider.maxValue = coolDownTime;
        coolDownSlider.value = coolDownTime;

        coin_Text.text = coins.ToString();

        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        coin_Text.text = coins.ToString();

        score = 0;
        score_Text.text = score.ToString();
    }
    public void StartCoolDown()
    {
        StartCoroutine(ReduceCoolDownBarValue());
    }
    IEnumerator ReduceCoolDownBarValue()
    {
        yield return new WaitForSeconds(time / 2);
        if (coolDownSlider.value <= 0)
        {
            /*coolDownSlider.value = coolDownTime;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().startCoolDown = false;*/

            StartCoroutine(UpCoolDownBarValue());
        }
        else
        {
            coolDownSlider.value -= coolDownSliderSpeed;
            StartCoroutine(ReduceCoolDownBarValue());
        }
    }
    IEnumerator UpCoolDownBarValue()
    {
        yield return new WaitForSeconds(time / 2);
        if (coolDownSlider.value > coolDownTime)
        {
            coolDownSlider.value = coolDownTime;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().startCoolDown = false;
        }
        else
        {
            coolDownSlider.value += coolDownSliderSpeed;
            StartCoroutine(UpCoolDownBarValue());
        }
    }

    public void FixedUpdate()
    {
        actionSlider.value = abar_value;

        if (abar_value > abar_maxValue / 2)
        {
            currentState = barState.Attack;
        }
        else
        {
            currentState = barState.ChangeDirection;
        }
    }

    public void AddCoin(int number)
    {
        coins += number;
        coin_Text.text = coins.ToString();

        PlayerPrefs.SetInt("Coins", coins);

    }
    public void AddScore(int number)
    {
        score += number;
        score_Text.text = score.ToString();

        if (PlayerPrefs.HasKey("HighestScore"))
        {
            if(score > PlayerPrefs.GetInt("HighestScore"))
            {
                PlayerPrefs.SetInt("HighestScore", score);
                HasSetHighestScore = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighestScore", score);
        }
    }
    IEnumerator ReduceActionBarValue()
    {
        yield return new WaitForSeconds(time);
        if (abar_value >= abar_maxValue || abar_value <= 0)
        {
            abar_speed *= -1;
        }

        if (!pause)
        {
            abar_value += abar_speed;
        }
        StartCoroutine(ReduceActionBarValue());
    }
    public void gameOver()
    {
        pause = true;
        StartCoroutine(showGOwithDelay());
    }
    IEnumerator showGOwithDelay()
    {
        yield return new WaitForSeconds(gameOverScreenShowDelay);
        gameOverScreen.gameObject.SetActive(true);
    }
}
