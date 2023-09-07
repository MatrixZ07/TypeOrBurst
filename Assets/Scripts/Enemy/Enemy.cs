using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int damage = 30;

	private SpriteRenderer spriteRenderer;

	// Start is called before the first frame update
	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		SetSpriteRendererMaterial(GetComponent<ColorPicker>().GetRandomMaterial());
	}

	public void HandleExplosion()
	{
		GetComponent<EdgeCollider2D>().enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<ParticleScript>().PlayExplosion();
	}

	private void SetSpriteRendererMaterial(Material material)
	{
		spriteRenderer.material = material;
	}

	public Material GetSpriteRendererMaterial()
	{
		return spriteRenderer.material;
	}
}
