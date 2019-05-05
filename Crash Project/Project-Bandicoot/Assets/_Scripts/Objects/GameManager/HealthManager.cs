using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;

    public Crash player;

    public float invinceablityLength;
    private float invinceablityCounter;


    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public GameObject deatheffect;
    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

    // Start is called before the first frame update
    void Start()
    {
        

        respawnPoint = player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(invinceablityCounter > 0)
        {
            invinceablityCounter -= Time.deltaTime;
        }

        if (isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0)
            {
                isFadeFromBlack = true;
            }
        }


    }

   

    public void HurtPlayer(int damnage, Vector3 direction)
    {
        if (invinceablityCounter <= 0)
        {



            currentHealth -= damnage;

            if (currentHealth <= 0)
            {
                Respawn();
            }

            else
            {
                player.KnockBack(direction);

                invinceablityCounter = invinceablityLength;
            }
        }
    }

    public void HealPlayer (int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
    }

    public void Respawn()
    {
        
        if (!isRespawning)
        {


            StartCoroutine("respawncol");
        }
    }

    public IEnumerator respawncol()
    {
        isRespawning = true;

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeFromBlack = true;

        isRespawning = false;


        player.gameObject.SetActive(true);
        player.transform.position = respawnPoint;
        currentHealth = 1;
    }
}
