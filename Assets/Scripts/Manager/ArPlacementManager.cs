using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using System;
public class ArPlacementManager : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject m_Surface;
    [SerializeField] private GameObject ballsHolder;
    private XROrigin arSessionOrigin;
    private ARRaycastManager m_ArRaycastManager;

    private static List<ARRaycastHit> m_ArRaycastHit = new List<ARRaycastHit>();

    private PlanetUIManager planetUIManager => PlanetUIManager.Instance;
    private void Awake()
    {
        m_ArRaycastManager = GetComponent<ARRaycastManager>();
        arSessionOrigin = GetComponent<XROrigin>();
    }

    private void OnEnable()
    {
        planetUIManager.onScaleArSession += ScaleXrSession;
        planetUIManager.onPlanetChanged += OnPlanedChanged;
    }

    private void OnPlanedChanged(int obj)
    {
        ballsHolder.transform.position = m_Surface.transform.position;
        ballsHolder.SetActive(true);
    }

    private void OnDisable()
    {
        planetUIManager.onScaleArSession -= ScaleXrSession;
        planetUIManager.onPlanetChanged -= OnPlanedChanged;
    }
    private void Update()
    {
        CastRayFromAR();
    }
    private void CastRayFromAR()
    {
        Vector3 centerOfScreen = new Vector3(Screen.width/2, Screen.height/2);
        Ray ray = arCamera.ScreenPointToRay(centerOfScreen);
        if (m_ArRaycastManager != null && m_ArRaycastManager.Raycast(ray, m_ArRaycastHit, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = m_ArRaycastHit[0].pose;
            Vector3 posaitionToBePlaced = hitPose.position;
            m_Surface.transform.position = posaitionToBePlaced;
            m_Surface.SetActive(true);
        }
    }
    private void ScaleXrSession(float value)
    {
        arSessionOrigin.transform.localScale = Vector2.one / value;
    }
}
