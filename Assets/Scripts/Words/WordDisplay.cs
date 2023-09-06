using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*K�mmert sich um das Anzeigen von W�rtern �ber die TextMesh-Komponente
# -Entfernen von Buchstaben
  -Entfernen von W�rtern /Zerst�rung des GameObjects
*/

//Schlechte Aufteilung.
public class WordDisplay : MonoBehaviour
{

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
        StartCoroutine(WaitASecond());
    }

    IEnumerator WaitASecond()
    {
		FindObjectOfType<AudioManager>().Play("EnemyExplode");
		GetComponentInParent<Enemy>().HandleExplosion();
        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
    }
}
