using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Harvester : MonoBehaviour
{
    [SerializeField] public Harvest _harvest;
    [SerializeField] public Seed _seed;

    // Harvest Analytics
    private Dictionary<string, int> _harvests = new Dictionary<string, int>();

    // Harvest to sell
    // Assignment 2 - Data structure to hold collected harvests
    private List<CollectedHarvest> _collectedHarvests = new List<CollectedHarvest>();

    public static Harvester _instance;
       
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    // Assignment 2
    public List<CollectedHarvest> GetCollectedHarvest()
    {
        return _collectedHarvests;
    }

    // Assignment 2
    public void RemoveHarvest(CollectedHarvest harvest)
    {
        _collectedHarvests.Remove(harvest);
    }

    // Assignment 2 - CollectHarvest method to collect the harvest when picked up
    public void CollectHarvest(string plantName, int harvestAmount)
    {
        var time = System.DateTime.Now.ToString("HH:mm:ss");
        var harvest = new CollectedHarvest(plantName, time, harvestAmount);
        _collectedHarvests.Add(harvest);
    }
    

    public void ShowHarvest(string plantName, int harvestAmount, int seedAmount, Vector2 position)
    {
        // initiate a harvest with random amount
        Harvest harvest = Instantiate(_harvest, position + Vector2.up + Vector2.right, Quaternion.identity);
        harvest.SetHarvest(plantName, harvestAmount);
        
        // initiate one seed object
        Seed seed = Instantiate(_seed, position + Vector2.up + Vector2.left, Quaternion.identity);
        seed.SetSeed(plantName, seedAmount);
    }

    //Assignment 3
    public void SortHarvestByAmount()
    {
        // Sort the collected harvest using Quick sort
        //_collectedHarvests.OrderByDescending(harvest => harvest._amount).ToList();

        _collectedHarvests = QuickSort(_collectedHarvests, 0, _collectedHarvests.Count - 1);
    }

    //Assignment 3
    private List<CollectedHarvest> QuickSort(List<CollectedHarvest> harvests, int low, int high)
    {
        var pivot = harvests[low];
        var i = low;
        var j = high;

        while (i <= j)
        {
            // Sort in descending order
            while(harvests[i]._amount > pivot._amount)
            {
                i++;
            }
            while(harvests[j]._amount < pivot._amount)
            {
                j--;
            }

            if(i <= j)
            {
                var temp = harvests[i];
                harvests[i] = harvests[j];
                harvests[j] = temp;
                i++;
                j--;
            }
        }

        if(low < j)
            QuickSort(harvests, low, j);

        if(i < high)
            QuickSort(harvests, i, high);

        return harvests;
    }

}

// For Assignment 2, this holds a collected harvest object
[System.Serializable]
public struct CollectedHarvest
{
    public string _name;
    public string _time;
    public int _amount;
    
    public CollectedHarvest(string name, string time, int amount)
    {
        _name = name;
        _time = time;
        _amount = amount;
    }
}