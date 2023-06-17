using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public Skin currentSkin;
    public List<Skin> AllSkin;
    public List<Skin> OwnedSkin;

    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        AllSkin = SkinInfoHolder.Instance.AllSkins;
        OwnedSkin = SkinInfoHolder.Instance.OwnedSkin;

        ChangePlayerSkin(currentSkin);

        foreach (Skin s in SkinInfoHolder.Instance.OwnedSkin)
        {
            if(s.skinIndex == SkinInfoHolder.Instance.currentSkinIndex)
            {
                currentSkin = s;
                return;
            }
        }
        ChangePlayerSkin(currentSkin);
    }


    // Update is called once per frame
    public void ChangePlayerSkin(Skin s)
    {
        player.currentSkin = s;
        player.ApplySkin();
    }
}
