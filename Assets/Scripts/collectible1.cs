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
            CatalogEntry.FishIdentifier fishId = caughtFishObject.GetComponent<CatalogEntry.FishIdentifier>();
        
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
            
                if (fishNameText != null) fishNameText.text = "???";
            
            }
            fishNameText.color = Color.gray;
        }
   
        // Component to attach to fish GameObjects
        public class FishIdentifier : MonoBehaviour
        {
            public string fishName;
        
        }
    }
} 

// Fish data structure
[System.Serializable]
public class Fish
{
    public string fishName;
    public Sprite fishSprite;
    public bool isUnlocked;
}

// Main catalog manager with wheel scrolling
public class FishCatalogManager : MonoBehaviour
{
    [Header("Fish Database")]
    public List<Fish> allFish = new List<Fish>();
    
    [Header("Wheel Settings")]
    public Transform wheelCenter; // Center point of the wheel
    public float radius = 300f; // Radius of the wheel
    public float fishSpacing = 80f; // Degrees between each fish
    public float scrollSpeed = 2f;
    public Image centerFishDisplay; // Large display of the centered fish
    
    [Header("UI References")]
    public GameObject fishIconPrefab; // Prefab for fish icons on the wheel
    
    private Dictionary<string, Fish> fishDictionary = new Dictionary<string, Fish>();
    private List<WheelFishIcon> wheelIcons = new List<WheelFishIcon>();
    private float currentAngle = 0f;
    private int centerFishIndex = 0;
    private float targetAngle = 0f;
    
    void Update()
    {
        HandleScrollInput();
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
    }
    
    void CreateWheelIcons()
    {
        for (int i = 0; i < allFish.Count; i++)
        {
            GameObject iconObj = Instantiate(fishIconPrefab, wheelCenter);
            WheelFishIcon icon = iconObj.GetComponent<WheelFishIcon>();
            
            if (icon != null)
            {
                icon.Initialize(allFish[i], i);
                wheelIcons.Add(icon);
            }
        }
    }
    
    void HandleScrollInput()
    {
        
        // Touch input for mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float swipe = touch.deltaPosition.y;
                if (Mathf.Abs(swipe) > 5f)
                {
                    ScrollWheel(swipe > 0 ? 1 : -1);
                }
            }
        }
    
        void ScrollWheel(int direction)
        {
            centerFishIndex += direction;
        
            // Wrap around
            if (centerFishIndex >= allFish.Count)
                centerFishIndex = 0;
            if (centerFishIndex < 0)
                centerFishIndex = allFish.Count - 1;
        
            targetAngle = centerFishIndex * fishSpacing;
        }
    
        void SmoothScrollToTarget()
        {
            // Smooth lerp to target angle
            currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * scrollSpeed);
        }
    
        void UpdateWheel()
        {
            for (int i = 0; i < wheelIcons.Count; i++)
            {
                float angle = (i * fishSpacing - currentAngle) * Mathf.Deg2Rad;
            
                // Calculate position on wheel (vertical wheel)
                float x = 0;
                float y = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
            
                Vector3 position = new Vector3(x, y, 0);
                wheelIcons[i].transform.localPosition = position;
            
                // Scale based on distance from center (fish at center is larger)
                float distanceFromCenter = Mathf.Abs(angle);
                float scale = Mathf.Lerp(1.2f, 0.5f, distanceFromCenter / Mathf.PI);
                wheelIcons[i].transform.localScale = Vector3.one * scale;
            }
        }
    }
        
    
    // Individual fish icon on the wheel
    public class WheelFishIcon : MonoBehaviour
    {
        public Image FishImage;
        public Text fishNameText; // Optional
    
        private Fish fishData;
        private int fishIndex;
        private CanvasGroup canvasGroup;
    
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
    
        public void Initialize(Fish fish, int index)
        {
            fishData = fish;
            fishIndex = index;
            FishImage.sprite = fish.fishSprite;
        
            if (fishNameText != null)
            {
                fishNameText.text = fish.fishName;
            }

            void SetAlpha(float alpha)
            {
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = alpha;
                }
            }
        }

        // Component to attach to fish GameObjects
        public class FishIdentifier : MonoBehaviour
        {
            public string fishName;
        }
    }
}