using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public int value = 1;  // The value of the collectible, you can change this based on your needs
    public FishSpawner FishList;
    private List <GameObject>AllFish;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook"))
        {
            // Add to the player's score or inventory
            GameManager.instance.AddScore(value);
        }
    }

    private void Start()
    {
      AllFish=FishList.allFishPrefabs;
    }
// Fish data structure
[System.Serializable]
public class Fish
{
    public string fishName;
    public Sprite fishSprite;
    public bool isUnlocked;
}

// Main catalog manager
public class FishCatalogManager : MonoBehaviour
{
    [Header("Fish Database")]
    public List<Fish> allFish = new List<Fish>();
    
    [Header("UI References")]
    public Transform catalogGrid; // Parent object for catalog entries
    public GameObject catalogEntryPrefab; // Prefab for each fish entry
    
    private Dictionary<string, Fish> fishDictionary = new Dictionary<string, Fish>();
    private List<CatalogEntry> catalogEntries = new List<CatalogEntry>();
    
    void Start()
    {
        InitializeCatalog();
        LoadUnlockedFish();
        UpdateCatalogUI();
    }
    
    void InitializeCatalog()
    {
        // Build dictionary for quick lookup
        foreach (Fish fish in allFish)
        {
            if (!fishDictionary.ContainsKey(fish.fishName))
            {
                fishDictionary.Add(fish.fishName, fish);
            }
        }
        
        // Create UI entries for all fish
        foreach (Fish fish in allFish)
        {
            GameObject entryObj = Instantiate(catalogEntryPrefab, catalogGrid);
            CatalogEntry entry = entryObj.GetComponent<CatalogEntry>();
            
            if (entry != null)
            {
                entry.Initialize(fish);
                catalogEntries.Add(entry);
            }
        }
    }
    
    // Call this when player catches a fish
    public void UnlockFish(GameObject caughtFishObject)
    {
        // Get fish name from the caught fish object
        FishIdentifier fishId = caughtFishObject.GetComponent<FishIdentifier>();
        
        if (fishId == null)
        {
            print("Caught fish has no FishIdentifier component!");
            return;
        }
        
        UnlockFish(fishId.fishName);
    }
    
    public void UnlockFish(string fishName)
    {
        if (fishDictionary.ContainsKey(fishName))
        {
            Fish fish = fishDictionary[fishName];
            
            if (!fish.isUnlocked)
            {
                fish.isUnlocked = true;
                SaveUnlockedFish();
                UpdateCatalogUI();
                print ($"New fish unlocked: {fishName}!");
            }
        }
        else
        {
            print ($"Fish '{fishName}' not found in catalog!");
        }
    }
    
    void UpdateCatalogUI()
    {
        foreach (CatalogEntry entry in catalogEntries)
        {
            entry.UpdateDisplay();
        }
    }
    
    // Save/Load system using PlayerPrefs
    void SaveUnlockedFish()
    {
        foreach (Fish fish in allFish)
        {
            PlayerPrefs.SetInt($"Fish_{fish.fishName}_Unlocked", fish.isUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }
    
    void LoadUnlockedFish()
    {
        foreach (Fish fish in allFish)
        {
            fish.isUnlocked = PlayerPrefs.GetInt($"Fish_{fish.fishName}_Unlocked", 0) == 1;
        }
    }
    
    public int GetUnlockedCount()
    {
        int count = 0;
        foreach (Fish fish in allFish)
        {
            if (fish.isUnlocked) count++;
        }
        return count;
    }
    
    public int GetTotalFishCount()
    {
        return allFish.Count;
    }
}

// Individual catalog entry component
public class CatalogEntry : MonoBehaviour
{
    public Image fishImage;
    public Text fishNameText; // Optional: display fish name
    
    private Fish fishData;
    
    public void Initialize(Fish fish)
    {
        fishData = fish;
        fishImage.sprite = fish.fishSprite;
        
        if (fishNameText != null)
        {
            fishNameText.text = fish.fishName;
        }
        
        UpdateDisplay();
    }
    
    public void UpdateDisplay()
    {
        if (fishData.isUnlocked)
        {
            // Show fish in full color (white tint)
            fishImage.color = Color.white;
            
            if (fishNameText != null)
            {
                fishNameText.color = Color.gray;
                }
        else
        
            // Show fish as silhouette (black)
            fishImage.color = Color.black;
            
            if (fishNameText != null)
            
                fishNameText.text = "???";}
                fishNameText.color = Color.gray;
            }
   
    // Component to attach to fish GameObjects
    public class FishIdentifier : MonoBehaviour
    {
        public string fishName;
        }
    }
}