using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    public List<Skin> AllSkin;
    public int currentSkinIndex = 1;
    public PlayerMovement player;

    Skin currentSkin;

    private void Start()
    {
        /*//AllSkin = SkinInfoHolder.Instance.AllSkins;
        currentSkinIndex = SkinInfoHolder.Instance.currentSkinIndex;
        foreach(Skin s in AllSkin)
        {
            if(s.skinIndex == currentSkinIndex)
            {
                player.currentSkin = s;
                return;
            }
        }*/
    }
}
