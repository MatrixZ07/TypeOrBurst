using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 30;
    public ParticleSystem trail;
	public ParticleSystem explosion;


	// Start is called before the first frame update
	void Awake()
    {
        //trail.Play();
        trail.Play();
		AssignMaterialAndColors();
	}

    // Update is called once per frame
    void Update()
    {
		ParticleSystem.MainModule trailMainModule = trail.main;
		trailMainModule.startRotationZ = new ParticleSystem.MinMaxCurve(gameObject.transform.rotation.z * -2);
	}

	public void HandleExplosion()
	{
		gameObject.GetComponent<EdgeCollider2D>().enabled = false;
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		trail.Pause();
		trail.Clear();
		explosion.gameObject.SetActive(true);
		explosion.Play();
	}


	private void AssignMaterialAndColors()
	{
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.material = gameObject.GetComponent<ColorPicker>().GetRandomMaterial();

		Color color1 = spriteRenderer.material.GetColor("Color1");
		Color color2 = spriteRenderer.material.GetColor("Color2");

		ParticleSystem.MainModule trailMainModule = trail.main;
		trailMainModule.startColor = new ParticleSystem.MinMaxGradient(color1, color2);

		ParticleSystem.ColorOverLifetimeModule explosionColorOverLifetimeModule = explosion.colorOverLifetime;
		explosionColorOverLifetimeModule.color = new ParticleSystem.MinMaxGradient(color1, color2);
	}
}
