using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Healthbar healthbar;
    public GameManager gameManager;
    public SpriteRenderer playerSprite;
   
    private static int health = 100;
    public static int Health { get => health; }
	public static bool IsPlayerDead { get => (Health <= 0) ? true : false; }
	int hitCount=0;

    //Referenz auf Highscore um an IsPlayerDead Aufruf zu senden, ob Highscore erreicht.

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerSprite = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        RevivePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected. Trying to increase hitcount");
        hitCount++;
        Debug.Log("Player Hit " + hitCount + "times");
        //Ziehe Leben ab
        int damage= collision.gameObject.GetComponent<EnemyManager>().damage;
        TakeDamage(damage);
        //Lebt Spieler noch?
        gameManager.IsPlayerDead(IsPlayerDead);
        //Spiele Healthbar-Animation
    }
    private void TakeDamage(int damage) {
        health -= damage;
        healthbar.SetHealth(health);
        if (IsPlayerDead)
        {
            playerSprite.enabled = false;
            GetComponent<ParticleSystem>().Play();
            FindObjectOfType<AudioManager>().Play("BubbleBurst");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("PlayerHit");
        }
        Debug.Log("Player lost " + damage + " health. Has now " + health);
    }
    public bool PlayerDead()
    {
        
        if (health <= 0)
        {
            return true;
        }
        else {
            return false;
        } 
    }
    
    public void RevivePlayer() {
        hitCount = 0;
        health = 100;
        healthbar.SetMaxHealth(health);
        try
        {
            playerSprite.enabled = true;
        }
        catch (MissingComponentException e) {
            Debug.Log(e);
        }
    }
}
