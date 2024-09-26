using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    private int colorId;
    public int ColorId 
    {
        get { return colorId; }
        set { colorId = value; }
    }

    void Update()
    {
        if (transform.position.y < 0) 
		{
			transform.position = new(Random.Range(-5f, 5f), 5, Random.Range(-10f, 10f));
		}
    }

    public void MoveMarble(Vector3 worldMousePosition)
    {
        transform.position = worldMousePosition;
    }
}