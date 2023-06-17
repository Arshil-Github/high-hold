using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinBlock : MonoBehaviour
{
    public Skin skin;
    public GameObject Locked;
    public GameObject unEquippied;
    public GameObject Equippied;
    public GameObject BuyPanel;

    public Image buttonImage;
    public Text cost;

    bool unlocked = false;
    SkinManager sk;
    public void Start()
    {
        sk = GameObject.FindObjectOfType<SkinManager>();
        ChangeState();

        buttonImage.sprite = skin.sprite;
        cost.text = "Coins : " + skin.cost.ToString();

        if (skin.owned == true && !PlayerPrefs.HasKey("S_Index" + skin.name))
        {
            PlayerPrefs.SetInt("S_index" + sk.name, 1);
            SkinInfoHolder.Instance.SyncPlayerPrefs();
        }

    }
    public void ChangeState()
    {
        if(skin == sk.currentSkin)
        {
            //On Quippuied
            isEquippied();
        }
        else
        {
            foreach (Skin s in sk.OwnedSkin)
            {
                if (s == skin)
                {
                    //Show Unccupied
                    isNotEquippied();
                    return;
                }
                else
                {
                    //Show Locked
                    isLocked();
                }
            }
        }
    }
    public void isEquippied()
    {
        Locked.SetActive(false);
        Equippied.SetActive(true);
        unEquippied.SetActive(false);
        unlocked = true;
    }
    public void isNotEquippied()
    {
        Locked.SetActive(false);
        Equippied.SetActive(false);
        unEquippied.SetActive(true);
        unlocked = true;
    }
    public void isLocked()
    {
        Locked.SetActive(true);
        Equippied.SetActive(false);
        unEquippied.SetActive(false);
    }
    public void Clicked()
    {
        foreach (Skin s in sk.OwnedSkin)
        {
            if (s == skin)
            {
                //Show Unccupied
                sk.currentSkin = skin;

                foreach(SkinBlock p in GameObject.FindObjectsOfType<SkinBlock>())
                {
                    p.ChangeState();
                }


                sk.ChangePlayerSkin(s);

                SkinInfoHolder.Instance.currentSkinIndex = s.skinIndex;
                return;
            }
            else
            {
                BuyPanel.SetActive(true);
            }
        }
        ChangeState();
    }
    public void BuySkin()
    {
        if(PlayerPrefs.GetInt("Coins") >= skin.cost)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - skin.cost);
            sk.OwnedSkin.Add(skin);
            SkinInfoHolder.Instance.OwnedSkin.Add(skin);

            skin.owned = true;
            PlayerPrefs.SetInt("S_index" + skin.skin_name, 1);

            Clicked();
        }
    }
    public void ClosebuyPanel()
    {
        BuyPanel.SetActive(false);
    }
}
