using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using TMPro;

public  class ColorPicker :MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public EdgeCollider2D enemyCollider;

    public Material[] materials;

    public ParticleSystem trail;
    public ParticleSystem explosionParticles;
    ParticleSystem.MinMaxCurve initialStartRotation;

    private void Start()
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.material = GetRandomMaterial();

        Color color1 = spriteRenderer.material.GetColor("Color1");
        Color color2 = spriteRenderer.material.GetColor("Color2");

        ParticleSystem.MainModule ma = trail.main;
        ma.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
        initialStartRotation = ma.startRotationZ;

        ParticleSystem.ColorOverLifetimeModule cm = explosionParticles.colorOverLifetime;
        cm.color = new ParticleSystem.MinMaxGradient(color1, color2);

    }
    private void Update()
    {
        //Rotation der gespawnten Partikel
        ParticleSystem.MainModule ma = trail.main;
        ma.startRotationZ =  new ParticleSystem.MinMaxCurve(gameObject.transform.rotation.z *-2);
    }
    public void HandleExplosion() {
        enemyCollider.enabled = false;
        spriteRenderer.enabled = false;
        trail.Pause();
        trail.Clear();
        explosionParticles.gameObject.SetActive(true);
        explosionParticles.Play();
    }

    public Material GetRandomMaterial() {
        int randomInt = Random.Range(0, materials.Length);
        Debug.Log("Random Material Index: " + randomInt);
        return materials[randomInt];
    }
    
}
