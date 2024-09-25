using System.Collections;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Marble selectedMarble = null;
    private Camera mainCamera;

    private InputControls input;
    private Vector3 screenPos;

    private Vector3 WorldPos
    {
        get
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new(Vector3.up, Vector3.zero);
            plane.Raycast(ray, out float distance);
            return ray.GetPoint(distance - 5);
        }
    }

    private bool HitMarble
    {
        get
        {
            Ray ray = mainCamera.ScreenPointToRay(screenPos);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                selectedMarble = hit.collider.GetComponent<Marble>();
            }
            return selectedMarble != null;
        }
    }
    
    void Awake()
    {
        mainCamera = Camera.main;
        input = new InputControls();
        input.Interacting.Position.performed += ctx => { screenPos = ctx.ReadValue<Vector2>(); };
        input.Interacting.Press.performed += _ => 
        { 
            screenPos = input.Interacting.Position.ReadValue<Vector2>();
            if(HitMarble) StartCoroutine(MoveMarble()); 
        };
        input.Interacting.Press.canceled += _ => selectedMarble = null;
    }

    private IEnumerator MoveMarble()
    {
        while(selectedMarble != null)
        {
            selectedMarble.MoveMarble(WorldPos);
            yield return null;
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}