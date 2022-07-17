using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int damage = 30;
    public ParticleSystem trail;
    // Start is called before the first frame update
    void Awake()
    {
        //trail.Play();
        trail.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
