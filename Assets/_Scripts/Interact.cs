using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Marble selectedMarble;
    private Camera mainCamera;
    [SerializeField] private Material mat0;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetSelection();
        }

        if (selectedMarble != null && Input.GetMouseButton(0))
        {
            MoveMarble();
        }
        
        //how to change the color of a material. Use in meny where players can choose colors.
        if (Input.GetMouseButtonDown(1))
        {
            mat0.color = new Color(1,0,1);
        }
    }

    private void GetSelection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            selectedMarble = hit.collider.GetComponent<Marble>();
        }
    }

    private void MoveMarble()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 wordlMousePos = ray.GetPoint(distance - 5);
            selectedMarble.MoveMarble(wordlMousePos);
        }
    }
}