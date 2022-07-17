using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*Kümmert sich um das Anzeigen von Wörtern über die TextMesh-Komponente
# -Entfernen von Buchstaben
  -Entfernen von Wörtern /Zerstörung des GameObjects
*/
public class WordDisplay : MonoBehaviour
{
    public SpriteRenderer enemySpriteRenderer;

    public TextMeshProUGUI text;
    public Word word;

    
    public ColorPicker particleHandler;
    public void SetWord(string word)
    {
        text.text = word;
    }

    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red;
    }
    public void ResetColor() {
        text.color = new Color32(4, 190, 5,255);
    }

    public void RemoveWord()
    {
        FindObjectOfType<AudioManager>().Play("EnemyExplode");
        //Wort aus Liste entfernen
        StartCoroutine(WaitASecond());
    }

    IEnumerator WaitASecond() {
        particleHandler.HandleExplosion();
        //GameObject.Instantiate<ParticleSystem>(explosion,transform.parent);
        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
    }
}
