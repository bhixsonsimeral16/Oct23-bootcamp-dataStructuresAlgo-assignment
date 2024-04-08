using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _seedButtonUI;
    [SerializeField] private Transform _seedsUIHolder;
    [SerializeField] private TMP_Text _txtStatus;

    [Header("Shop-BUY")]
    [SerializeField] private Transform _buySeedsHolder;
    [SerializeField] private SeedsBuyUIElement _buySeedsUIElement;

    [Header("Shop-SELL")]
    [SerializeField] private Transform _sellHarvestHolder;
    [SerializeField] private SellHarvestUIElement _sellHarvestUIElement;
    private List<SellHarvestUIElement> _sellHarvestUIElementList = new();

    public static UIManager _instance { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    public void UpdateStatus(string text)
    {
        _txtStatus.SetText(text);
    }

    public void InitializePlantUIs(PlantTypeScriptableObject[] _plantTypes)
    {
        foreach (var item in _plantTypes)
        {
            GameObject seedButton = Instantiate(_seedButtonUI, _seedsUIHolder);

            seedButton.GetComponent<Image>().sprite = item._seedSprite;

            seedButton.GetComponent<Button>().onClick.AddListener(() => { 
                Planter._instance.ChoosePlant(item._plantTypeName); 
            });

            seedButton.GetComponent<UpdateSeedsUI>().SetSeedName(item._plantTypeName);



            SeedsBuyUIElement buySeedUIElement = Instantiate(_buySeedsUIElement, _buySeedsHolder);
            buySeedUIElement.SetElement(item._plantTypeName, item._pricePerSeed, item._seedSprite);
            buySeedUIElement.GetButton().onClick.AddListener(() =>
            {
                GameManager._instance.GetShop().BuySeed(item._plantTypeName, item._pricePerSeed);
            });

        }
    }

    public void ShowTotalHarvest()
    {
        // Clear harvest UI elements
        foreach(var item in _sellHarvestUIElementList)
        {
            if(item != null && item.gameObject != null)
            {
                Destroy(item.gameObject);
            }
        }
        _sellHarvestUIElementList.Clear();

        //Assignment 2
        foreach (var item in Harvester._instance.GetCollectedHarvest())
        {
            SellHarvestUIElement sellHarvestUIElement = Instantiate(_sellHarvestUIElement, _sellHarvestHolder);
            sellHarvestUIElement.SetElement(
                item,
                item._name,
                item._time,
                Planter._instance.GetPlantResourseByName(item._name)._pricePerHarvest,
                item._amount,
                Planter._instance.GetPlantResourseByName(item._name)._harvestSprite
            );

            _sellHarvestUIElementList.Add(sellHarvestUIElement);
        }
    }

    
}
