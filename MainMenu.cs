using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Transform infoPanel;
    public Transform mainMenuPanel;
    public Transform shopPanel;
    public Transform skinPanel;
    public Transform player;
    public Text coins;
    public Text HighestScore;


    [Header("ShopStuff")]
    public int grenadePrice;
    public int dashPrice;
    public int shieldPrice;
    public Text grenadeText;
    public Text dashText;
    public Text shieldText;

    public Text text_grenadePrice;
    public Text text_dashPrice;
    public Text text_shieldPrice;
    public void FixedUpdate()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
        }
        if (PlayerPrefs.HasKey("HighestScore"))
        {
            HighestScore.text = PlayerPrefs.GetInt("HighestScore").ToString();
        }
        if (PlayerPrefs.HasKey("GrenadeQuantity"))
        {
            grenadeText.text = PlayerPrefs.GetInt("GrenadeQuantity").ToString();
        }
        if (PlayerPrefs.HasKey("DashQuantity"))
        {
            dashText.text = PlayerPrefs.GetInt("DashQuantity").ToString();
        }
        if (PlayerPrefs.HasKey("ShieldQuantity"))
        {
            shieldText.text = PlayerPrefs.GetInt("ShieldQuantity").ToString();
        }

        text_dashPrice.text = dashPrice.ToString();
        text_grenadePrice.text = grenadePrice.ToString();
        text_shieldPrice.text = shieldPrice.ToString();
    }
    public void Play()
    {
        FindObjectOfType<levelloader>().LoadNextLevel(1);
    }
    public void Shop()
    {
        shopPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }
    public void Skin()
    {
        skinPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Info()
    {
        infoPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        infoPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
        skinPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);
    }
    #region ShopStuff
    public void BuyGrenade()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            if (PlayerPrefs.GetInt("Coins") >= grenadePrice)
            {
                if (PlayerPrefs.HasKey("GrenadeQuantity"))
                {
                    PlayerPrefs.SetInt("GrenadeQuantity", PlayerPrefs.GetInt("GrenadeQuantity") + 1);
                    grenadeText.text = PlayerPrefs.GetInt("GrenadeQuantity").ToString();
                }
                else
                {
                    PlayerPrefs.SetInt("GrenadeQuantity", 1);

                }
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - grenadePrice);
                coins.text = PlayerPrefs.GetInt("Coins").ToString();
            }
        }
    }
    public void BuyDash()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            if (PlayerPrefs.GetInt("Coins") >= grenadePrice)
            {
                if (PlayerPrefs.HasKey("DashQuantity"))
                {
                    PlayerPrefs.SetInt("DashQuantity", PlayerPrefs.GetInt("DashQuantity") + 1);
                    dashText.text = PlayerPrefs.GetInt("DashQuantity").ToString();
                }
                else
                {
                    PlayerPrefs.SetInt("DashQuantity", 1);

                }
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - dashPrice);
                coins.text = PlayerPrefs.GetInt("Coins").ToString();
            }
        }
    }
    public void BuyShield()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            if (PlayerPrefs.GetInt("Coins") >= grenadePrice)
            {
                if (PlayerPrefs.HasKey("ShieldQuantity"))
                {
                    PlayerPrefs.SetInt("ShieldQuantity", PlayerPrefs.GetInt("ShieldQuantity") + 1);
                    shieldText.text = PlayerPrefs.GetInt("ShieldQuantity").ToString();
                }
                else
                {
                    PlayerPrefs.SetInt("ShieldQuantity", 1);

                }
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - shieldPrice);
                coins.text = PlayerPrefs.GetInt("Coins").ToString();
            }
        }
    }
    #endregion
}
