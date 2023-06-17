using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinInfoHolder : MonoBehaviour
{
    #region GlobalCrap
    //For making this a global Script
    public static SkinInfoHolder Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public int currentSkinIndex;
    public List<Skin> AllSkins;
    public List<Skin> OwnedSkin;

    public void Start()
    {
        SyncPlayerPrefs();
    }
    public void SyncPlayerPrefs()
    {
        
        foreach (Skin s in AllSkins)
        {
            if (!PlayerPrefs.HasKey("S_Index" + s.skin_name))
            {
                if(s.owned == true)
                {
                    PlayerPrefs.SetInt("S_Index" + s.skin_name, 1);
                    OwnedSkin.Add(s);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("S_Index" + s.skin_name) == 1)
                {
                    Debug.Log(s.skin_name);
                    OwnedSkin.Add(s);
                }
            }
        }
    }
}
