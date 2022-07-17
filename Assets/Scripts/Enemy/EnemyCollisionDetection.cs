using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetection : MonoBehaviour
{
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(0, 6);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider,true);
        }
        if (collision.gameObject.tag == "Player") {
            WordDisplay display = gameObject.transform.GetComponentInChildren<WordDisplay>();
            FindObjectOfType<WordManager>().RemoveDestroyedWord(display);
            Destroy(gameObject);
        }
    }

}
