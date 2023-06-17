using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkin", menuName = "Skin")]
public class Skin : ScriptableObject
{
    public int skinIndex;

    public Sprite sprite;
    public Color color;

    //public GameObject BGParticleEffect;

    public GameObject bullet;
    public GameObject burstEffect;

    public AudioClip attackSFX;
    public AudioClip coinPickup;

    public int cost;
    public string skin_name;
    public bool owned = false;
}
