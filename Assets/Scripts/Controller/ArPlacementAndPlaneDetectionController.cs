using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArPlacementAndPlaneDetectionController : MonoBehaviour
{
    private ARPlaneManager m_ArPlaneManager;
    private ArPlacementManager m_ArPlacementManager;


    private void Awake()
    {
        m_ArPlaneManager = GetComponent<ARPlaneManager>();
        m_ArPlacementManager = GetComponent<ArPlacementManager>();
    }


    private void SetArPlacementAndPlaneDetection(bool isEnable)
    {
        m_ArPlaneManager.enabled = isEnable;
        m_ArPlacementManager.enabled = isEnable;
        SetAllPlanesActiveAndDisactive(isEnable);
    }

    private void SetAllPlanesActiveAndDisactive(bool isActive)
    {
        foreach(var plane in m_ArPlaneManager.trackables)
        {
            plane.gameObject.SetActive(isActive);
        }
    }
}
