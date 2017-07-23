using UnityEngine;

public class Ingredient : MonoBehaviour
{
	public GlassManager glassManager;

	private bool triggered = false;
	private Color color;

	public void SetColor(Color newColor)
	{
		color = newColor;
		GetComponent<MeshRenderer>().material.color = color;
	}

	void OnTriggerEnter(Collider col)
	{
		if (triggered) return;

		glassManager.Collect(color);

		// TODO remove animation?
	}
}
