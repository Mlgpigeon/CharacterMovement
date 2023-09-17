using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class AimController : MonoBehaviour
{
    public InputChecker input;
    
    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;
    [HideInInspector] public Animator _animator;
    private Mouse mouse;
    public GameObject hand;
    public GameObject ball;
    [HideInInspector] GameObject currentPokeball;
    public GameObject pokeballPrefab;
    public CinemachineVirtualCamera aimShootCamera;
    private Ray ray;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform spawnPokeball;
    public float shootTimeout = 1f;
    private float _shootTimeoutDelta;
    


    void Start()
    {
        input = GetComponent<InputChecker>();
        _animator = GetComponent<Animator>();
        mouse = Mouse.current;
        currentPokeball = ball.transform.GetChild(0).gameObject;

    }
    void Update()
    {
        if (input.aim)
        {
            //Calculates the raycast of the camera to aim
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f,Screen.height/2f);
            ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                debugTransform.position = raycastHit.point;
            }
            //Makes a ball appear on the player's hand
            currentPokeball.transform.SetParent(hand.transform);
            currentPokeball.transform.position = hand.transform.position;
            currentPokeball.transform.rotation = hand.transform.rotation;
            
            _animator.SetBool("Aiming", true);
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);

            //Allow time for the camera to blend before enabling the UI
            StartCoroutine(ShowReticle());
            if (mouse.leftButton.wasPressedThisFrame && _shootTimeoutDelta <= 0.0f)
            {
                StartCoroutine(ThrowBall());
                _shootTimeoutDelta = shootTimeout;
            }
            // shoot timeout
            if (_shootTimeoutDelta >= 0.0f)
            {
                _shootTimeoutDelta -= Time.deltaTime;
            }
            
        }
        else if(!input.aim)
        {
            
            
            currentPokeball.transform.SetParent(ball.transform);
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimReticle.SetActive(false);
            _animator.SetBool("Aiming", false);
        }
    }
    
    IEnumerator ThrowBall()
    {
        
        _animator.SetTrigger("Fire");
        //Calculate direction from throwing point to crosshair pointed point
        Vector3 aimDirection = (debugTransform.position - spawnPokeball.position).normalized;
        float angle = 5f;
        Vector3 axis = Vector3.Cross(aimDirection, Vector3.up);
        if (axis == Vector3.zero) axis = Vector3.right;
        aimDirection = Quaternion.AngleAxis(angle, axis) * aimDirection;
        GameObject projectile = Instantiate(pokeballPrefab,spawnPokeball.position, Quaternion.LookRotation(aimDirection,Vector3.up));
        currentPokeball.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        currentPokeball.SetActive(true);
    }
    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }
}
