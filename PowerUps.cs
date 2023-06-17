using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    [Header("References")]
    public PlayerMovement player;

    [Header("Shield")]
    public int shield_number;
    public bool isShieldOn = false;
    public Transform shield_Transform;
    public float shield_Duration;
    public Text shield_Quantity_Text;
    public AudioClip shield_sfx;

    [Header("Dash")]
    public int dash_number;
    public int dash_speed;
    public Transform dash_Effect;
    public float dash_Duration;
    float original_dash_speed;
    public Text dash_Quantity_Text;
    public AudioClip dash_sfx;

    [Header("Grenade")]
    public int grenade_number;
    public GameObject grenade_prefab;
    public float grenade_DetoriateTime;
    public Text grenade_Quantity_Text;
    public AudioClip grenade_sfx;

    [Header("PowerUpButton")]
    public Animator anim_PowerUpButton;
    bool isPowerUpbuttonOpen = false;


    [Header("Sound Stuff")]
    AudioSource audioS;


    private void Start()
    {
        audioS = gameObject.GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("GrenadeQuantity"))
        {
            grenade_number = PlayerPrefs.GetInt("GrenadeQuantity");
        }
        else
        {
            grenade_number = 0;
        }

        if (PlayerPrefs.HasKey("DashQuantity"))
        {
            dash_number = PlayerPrefs.GetInt("DashQuantity");
        }
        else
        {
            dash_number = 0;
        }

        if (PlayerPrefs.HasKey("ShieldQuantity"))
        {
            shield_number = PlayerPrefs.GetInt("ShieldQuantity");
        }
        else
        {
            shield_number = 0;
        }
    }
    private void FixedUpdate()
    {
        dash_Quantity_Text.text = dash_number.ToString();
        shield_Quantity_Text.text = shield_number.ToString();
        grenade_Quantity_Text.text = grenade_number.ToString();
    }

    public void PowerUpbutton_OpenClose()
    {
        if (isPowerUpbuttonOpen)
        {
            anim_PowerUpButton.SetTrigger("Closed");
            isPowerUpbuttonOpen = false;


            //For Slow and Stuff
            player.gm.pause = false;

            player.Stop = false;

            player.postProcessing_slowedDown.SetActive(false);

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (enemy.TryGetComponent<Enemy>(out Enemy e))
                {
                    enemy.GetComponent<Enemy>().slow = false;
                }
                else if (enemy.TryGetComponent<Bomber_Enemy>(out Bomber_Enemy f))
                {
                    enemy.GetComponent<Bomber_Enemy>().slow = false;
                }
            }

        } 
        else if (!isPowerUpbuttonOpen)
        {
            anim_PowerUpButton.SetTrigger("Clicked");
            isPowerUpbuttonOpen = true;

            //For Slow and Stuff
            player.gm.pause = true;

            player.Stop = true;

            player.postProcessing_slowedDown.SetActive(true);

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (enemy.TryGetComponent<Enemy>(out Enemy e))
                {
                    enemy.GetComponent<Enemy>().slow = true;
                }
                else if (enemy.TryGetComponent<Bomber_Enemy>(out Bomber_Enemy f))
                {
                    enemy.GetComponent<Bomber_Enemy>().slow = true;
                }
            }
        }
    }

    #region ShieldStuff
    public void equip_Shield()
    {
        if (shield_number > 0)
        {
            shield_Transform.gameObject.SetActive(true);
            shield_number--;

            isShieldOn = true;

            PlayerPrefs.SetInt("ShieldQuantity", shield_number);

            audioS.clip = shield_sfx;
            audioS.Play();

            StartCoroutine(unequip_Shield_withDelay());

            PowerUpbutton_OpenClose();
        }
    }
    IEnumerator unequip_Shield_withDelay()
    {
        yield return new WaitForSeconds(shield_Duration);
        unEquip_Shield();
    }
    public void unEquip_Shield()
    {
        shield_Transform.gameObject.SetActive(false);
        isShieldOn = false;
    }
    #endregion

    #region DashStuff
    public void equip_Dash()
    {
        if(dash_number > 0)
        {
            PowerUpbutton_OpenClose();
            dash_Effect.gameObject.SetActive(true);
            dash_number--;


            PlayerPrefs.SetInt("DashQuantity", dash_number);

            original_dash_speed = player.speed;
            player.speed = dash_speed;

            audioS.clip = dash_sfx;
            audioS.Play();

            StartCoroutine(unequip_Dash_withDelay());
        }
    }
    IEnumerator unequip_Dash_withDelay()
    {
        yield return new WaitForSeconds(dash_Duration);
        unEquip_Dash();
    }
    public void unEquip_Dash()
    {
        player.speed = original_dash_speed;
        dash_Effect.gameObject.SetActive(false);
    }
    #endregion

    #region GrenadeStuff
    public void drop_Grenade()
    {
        if(grenade_number > 0)
        {
            PowerUpbutton_OpenClose();

            Instantiate(grenade_prefab, player.transform.position, player.transform.rotation);
            grenade_number--;

            PlayerPrefs.SetInt("GrenadeQuantity", grenade_number);


            audioS.clip = grenade_sfx;
            audioS.Play();
        }
    }
    #endregion

    #region Testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            equip_Shield();
        } else if (Input.GetKeyDown(KeyCode.O))
        {
            equip_Dash();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            drop_Grenade();
        }
    }
    #endregion
}
