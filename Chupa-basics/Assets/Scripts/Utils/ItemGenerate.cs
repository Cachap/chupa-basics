using UnityEngine;

public class ItemGenerate : MonoBehaviour
{
    [SerializeField] Transform[] pointsSpawn;
	[SerializeField] Item[] items;

	private void Start()
	{
		for (int i = 0; i < pointsSpawn.Length; i++)
		{
			int rangomIndex = Random.Range(0, items.Length);
			Instantiate(items[rangomIndex].gameObject, pointsSpawn[i]);
		}
	}
}
