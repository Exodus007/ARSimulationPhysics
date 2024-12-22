using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : PlanetSimulationBase
{
    enum Planets { Earth, Moon, Mars }
    [SerializeField] private Planets planet;


    private Transform cachedTransform;
    private Vector3 cachedPosition;
    private void CachedData()
    {
        cachedTransform = transform;
        cachedPosition = transform.localPosition;
    }

    private new void Awake()
    {
        base.Awake();
        CachedData();
    }


    protected override void OnPlanetChanged(int planetIndex)
    {
        base.OnPlanetChanged(planetIndex);
        ResetData();
        if (!IsInvoking(nameof(OnRewake)))
            Invoke(nameof(OnRewake), 0.1f);
    }


    private void ResetData()
    {
        rb.linearVelocity = Vector3.zero;
        transform.localPosition = cachedPosition;
        transform.localScale = cachedTransform.localScale;
        transform.parent.gameObject.SetActive(false);
        Debug.Log("Cached data: " + transform.localPosition);
    }

    private void OnRewake()
    {
        transform.parent.gameObject.SetActive(true);
    }
}
