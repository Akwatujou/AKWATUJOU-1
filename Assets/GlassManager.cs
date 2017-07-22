using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlassManager : MonoBehaviour
{
	public Material material;
	private Material referenceMaterial;
	private Material actualMaterial;

	public Image reference;
	public Image actual;

	public GameObject ingredientPrefab;
	public Transform SpawnA;
	public Transform SpawnB;

	private List<Color> referenceColors = new List<Color>();
	private List<Color> actualColors = new List<Color>();

	public List<Color> availableColors = new List<Color>();
	public float rngDecrease;
	private TetrisRandomGenerator<Color> rng;

	private void Awake()
	{
		rng = new TetrisRandomGenerator<Color>(rngDecrease);
		foreach (Color color in availableColors)
		{
			rng.Add(color);
		}
	}

	void Start()
	{
		referenceMaterial = new Material(material);
		actualMaterial = new Material(material);

		reference.material = referenceMaterial;
		actual.material = actualMaterial;

		CreateReference(2);
	}

	void Update()
	{
		int count = referenceColors.Count;

		var fullActualColors = new List<Color>(actualColors);
		while (fullActualColors.Count < count)
		{
			fullActualColors.Add(new Color(0, 0, 0, 0));
		}

		referenceMaterial.SetFloat("_ColorCount", count);
		actualMaterial.SetFloat("_ColorCount", count);

		if (count > 0)
		{
			referenceMaterial.SetColorArray("_Colors", referenceColors);
			actualMaterial.SetColorArray("_Colors", fullActualColors);
		}
	}

	public void CreateReference(int count)
	{
		referenceColors.Clear();
		actualColors.Clear();

		float baseZ = 0f;
		while (referenceColors.Count < count)
		{
			Color color = rng.Draw();
			referenceColors.Add(color);

			var clone = Instantiate<GameObject>(ingredientPrefab);

			float ratio = Random.value;
			var position = Vector3.Lerp(SpawnA.position, SpawnB.position, ratio);
			position.z += baseZ;
			clone.transform.position = position;
			baseZ = position.z;

			var ingredient = clone.GetComponent<Ingredient>();
			ingredient.glassManager = this;
			ingredient.SetColor(color);
		}
	}

	public void Collect(Color color)
	{
		actualColors.Add(color);

		// TODO full
	}
}
