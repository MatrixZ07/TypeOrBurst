using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShadowScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    [SerializeField] 
    private ParticleSystem ps;

    private float sinValue = 1;
    //bestimmt Frequenz der Sin-Funktion. Wie schnell von -1 nach +1 gewechselt wird.
    public float frequency = 1f;

    // Start is called before the first frame update
    void Start()
    {
        text=gameObject.GetComponentInChildren<TextMeshProUGUI>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    //Steuert den Dissolve Effekt des Shadow-Gegners über die Sinus-Funktion. 
    //Setzt zugleich TextMeshPro Color-Alpha auf entsprechenden Wert
    void Update()
    {
        sinValue = SinFunc();
        //Debug.Log(sinValue + " ist ermittelte Sine Time");
        float alpha = SinToAlpha(sinValue);
        //Debug.Log(alpha + " ist ermittelter Alpha");
        //Alpha-Wert des zugehörigen TextMeshPro auf den Wert der Sinusfunktion setzen.
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

        spriteRenderer.material.SetFloat("_SinusTime", sinValue);
        if (sinValue <= -0.5)
        {
            ps.Stop();
            ps.Clear();
        }
        else {
            if(ps.isStopped)
            ps.Play();
        }
    }

    public float SinFunc() {
        return (Mathf.Sin(Time.time * frequency));
    }

    //Wandelt Sin-Wert von einem Intervall [1;-1] in Intervall zwischen [0;1] um
    public float SinToAlpha(float value) {
        return (value + 1) / 2;
    }
}
