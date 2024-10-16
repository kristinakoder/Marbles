using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    private Marble selectedMarble = null;
    private Camera mainCamera;
    [SerializeField] IntVariable shakeSensitivity;
    private InputControls input;
    private Vector3 screenPos;

    private Vector3 WorldPos
    {
        get
        {
            Ray ray = mainCamera.ScreenPointToRay(screenPos);
            Plane plane = new(Vector3.up, Vector3.zero);
            plane.Raycast(ray, out float distance);
            return ray.GetPoint(distance - 4);
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
                if(selectedMarble != null) 
                {
                    Rigidbody rb = selectedMarble.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            return selectedMarble != null;
        }
    }
    
    void Awake()
    {
        shakeSensitivity.i = 50;
        mainCamera = Camera.main;
        input = new InputControls();
        StartCoroutine(ResetByShake());
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

    private IEnumerator ResetByShake()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(1);
            if (input.Interacting.Shake.ReadValue<Vector3>().sqrMagnitude >= shakeSensitivity.i) SceneManager.LoadScene("Game");
        }
    }

    private void OnEnable()
    {
        InputSystem.EnableDevice(Accelerometer.current);
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}