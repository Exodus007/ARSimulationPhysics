
using System;
using UnityEngine;

public class PlanetSimulationBase : MonoBehaviour
{
    protected static string planetName = "Earth";

    private Collider collider3d;
    protected Rigidbody rb;
    private PlanetUIManager planetUIManager => PlanetUIManager.Instance;
    private void OnEnable()
    {
        planetUIManager.onPlanetChanged += OnPlanetChanged;
    }
    private void OnDisable()
    {
        planetUIManager.onPlanetChanged -= OnPlanetChanged;
    }
    protected virtual void OnPlanetChanged(int planetIndex)
    {
        switch (planetIndex)
        {
            case 0:
                planetName = "Earth";
                break;
            case 1:
                planetName = "Moon";
                break;
            case 2:
                planetName = "Mars";
                break;
        }

        Start();
    }
    protected void Awake()
    {
        Initialization();
    }

    protected void Start()
    {
        Debug.Log(planetName);
        SetGravity(planetName);
        SetBounce(planetName);
    }
    private void Initialization()
    {
        collider3d = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void SetGravity(string planet)
    {
        switch (planet)
        {
            case "Earth":
                Physics.gravity = new Vector3(0, -9.81f, 0);
                break;
            case "Moon":
                Physics.gravity = new Vector3(0, -1.625f, 0);
                break;
            case "Mars":
                Physics.gravity = new Vector3(0, -3.72f, 0);
                break;
            default:
                Physics.gravity = new Vector3(0, -9.81f, 0);
                break;
        }
    }

    private void SetBounce(string planet)
    {
        PhysicsMaterial material = new PhysicsMaterial();
        switch (planet)
        {
            case "Earth":
                material.bounciness = 0.6f;
                break;
            case "Moon":
                material.bounciness = 0.8f;
                break;
            case "Mars":
                material.bounciness = 0.5f;
                break;
            default:
                material.bounciness = 0.6f;
                break;
        }
        collider3d.material = material;
    }
}
