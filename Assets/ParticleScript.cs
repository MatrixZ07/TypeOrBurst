using UnityEngine;


public class ParticleScript : MonoBehaviour
{
	public ParticleSystem trail;
	public ParticleSystem explosion;

	ParticleSystem.MainModule trailMainModule;
	ParticleSystem.ColorOverLifetimeModule explosionColorOverLifetimeModule;

	void Awake()
    {
		trailMainModule = trail.main;
		explosionColorOverLifetimeModule = explosion.colorOverLifetime;
		
		trail.Play();
		SetColorFromMaterial(gameObject.GetComponent<SpriteRenderer>().material);
	}

	void Update()
	{
		ParticleSystem.MainModule trailMainModule = trail.main;
		trailMainModule.startRotationZ = new ParticleSystem.MinMaxCurve(gameObject.transform.rotation.z * -2);
	}

	public void PlayExplosion()
	{
		trail.Pause();
		trail.Clear();
		explosion.gameObject.SetActive(true);
		explosion.Play();
	}

	private void SetColorFromMaterial(Material material)
	{
		Color color1 = material.GetColor("Color1");
		Color color2 = material.GetColor("Color2");
		trailMainModule.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
		explosionColorOverLifetimeModule.color = new ParticleSystem.MinMaxGradient(color1, color2);
	}
}
