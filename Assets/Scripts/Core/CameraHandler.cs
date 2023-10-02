using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private bool attachedToSpaceship = true;
    private Spaceshuttle spaceship;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
    private void Start()
    {
        spaceship = GameManager.instance.GetCurrentSpaceship();
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedToSpaceship)
        {
            mainCamera.transform.position = new Vector3(spaceship.transform.position.x, spaceship.transform.position.y, mainCamera.transform.position.z);
        }
    }
}
