using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledSGTimeWrapper : MonoBehaviour
{
    //Um BubbleWobble-Effekt in Menüsequenzen zu aktivieren
    //Ermöglicht den Zugriff auf unscaledTime in ShaderGraph
        //Nachteil: ShaderGraph-Preview nicht funktional, wenn Property connected

    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("_UnscaledTime", Time.unscaledTime);
    }
}
