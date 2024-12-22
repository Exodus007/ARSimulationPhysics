using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetUIManager : GenericSingleton<PlanetUIManager>
{
    [SerializeField] private Button planetChangerBtn;
    [SerializeField] private Material surfaceMaterial;
    [SerializeField] private List<Color> planetColor;
    [SerializeField] private Slider scaleArSession;

    private TextMeshProUGUI planetChangedTxt;
    public Action<int> onPlanetChanged; 
    public Action<float> onScaleArSession;

    private void Start()
    {
        planetChangedTxt = planetChangerBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        planetChangerBtn.onClick.RemoveAllListeners();
        planetChangerBtn.onClick.AddListener(onPlanetChangerClick);
        scaleArSession.onValueChanged.AddListener(OnSliderValueChanged);
        UpdateSurfaceVisual(0);
    }


    private void onPlanetChangerClick()
    {
        int planetIndex = ++PlanetsName.planetChangedIndex;
        if(planetIndex >= PlanetsName.planetsNameList.Count)//Base Case
        {
            planetIndex = 0;
            PlanetsName.planetChangedIndex = planetIndex;
            
        }
        planetChangedTxt.text = PlanetsName.planetsNameList[planetIndex];
        UpdateSurfaceVisual(planetIndex);
        onPlanetChanged?.Invoke(planetIndex);
    }

    private void UpdateSurfaceVisual(int visualIndex)
    {
        surfaceMaterial.color = planetColor[visualIndex];
    }

    private void OnSliderValueChanged(float value)
    {
        onScaleArSession?.Invoke(value);
    }
}
