using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private int NumberOfMarbles = 480;
	[SerializeField] private int NumberOfColors = 6;
    [SerializeField] private Material mat0, mat1, mat2, mat3, mat4, mat5;
    [SerializeField] private GameObject marble;
	[SerializeField] private GameObject tub;

    void Start()
    {
        AddMarbles(NumberOfMarbles);
		AddTubs(NumberOfColors);
    }

	private void AddTubs(int numberOfColors) 
	{
		//TODO: needs to add tubs evenly based on screen size, placed based on number of colors to sort
		Instantiate(tub, new Vector3(-4.5f, 0, 8.5f), Quaternion.identity);
		Instantiate(tub, new Vector3(-4.5f, 0, -8.5f), Quaternion.identity);
		Instantiate(tub, new Vector3(-4.5f, 0, 0), Quaternion.identity);
		Instantiate(tub, new Vector3(4.5f, 0, -8.5f), Quaternion.identity);
		Instantiate(tub, new Vector3(4.5f, 0, 0), Quaternion.identity);
		Instantiate(tub, new Vector3(4.5f, 0, 8.5f), Quaternion.identity);
	}
    
	private void AddMarbles(int NumberOfMarbles)
	{
		int n = 0;
		for (int i = 0; i < NumberOfMarbles; i++)
		{
			int numberOfEachColor = NumberOfMarbles / NumberOfColors;
			if (i % numberOfEachColor == 0) n++; //TODO: this divides unevenly. Say 5 colors and 9 mables. 4 of them will be the last color.
            Vector3 spawnPosition = new(Random.Range(-5f, 5f), 5, Random.Range(-10f, 10f));
            SetMaterial(marble, n);
            Instantiate(marble, spawnPosition, Quaternion.identity);
		}
	}

    private void SetMaterial(GameObject marble, int n)
	{
        Renderer renderer = marble.GetComponent<Renderer>();
		switch(n)
		{
			case 1: renderer.material = mat0;
			break;
			case 2: renderer.material = mat1;
			break;
			case 3: renderer.material = mat2;
			break;
			case 4: renderer.material = mat3;
			break;
			case 5: renderer.material = mat4;
			break;
			default: renderer.material = mat5;
			break;
		}
	}
}