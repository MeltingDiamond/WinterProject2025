using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    public new Camera camera;
    [SerializeField] private Transform _caughtFishSign;
    public static Transform CaughtFishSign;

    public GameObject zone0bg; // Top most background
    
    public int maxAmountInZone1;
    public GameObject zone1bg; // 2nd background
    private List<GameObject> _zone1Spawns = new List<GameObject>();
    public List<GameObject> zone1FishPrefabs;
    
    public GameObject zone1TransitionBG; // 3rd background
    
    public int maxAmountInZone2;
    public GameObject zone2bg; // 4th background
    private List<GameObject> _zone2Spawns = new List<GameObject>();
    public List<GameObject> zone2FishPrefabs;
    
    public GameObject zone2TransitionBG; // 5th background
    
    public int maxAmountInZone3;
    public GameObject zone3bg; // bottom most background
    private List<GameObject> _zone3Spawns = new List<GameObject>();
    public List<GameObject> zone3FishPrefabs;

    private float _zone1Start;
    private float _zone2Start;
    private float _zone3Start;
    [HideInInspector] public float oceanFloor;
    
    // List containing game objects of all fishes that can spawn
    public List<GameObject> allFishPrefabs;

    void Start()
    {
        CaughtFishSign = _caughtFishSign;
        // Get size of backgrounds to use later
        var zone0bgSize = zone0bg.GetComponent<SpriteRenderer>().size;
        var zone1bgSize= zone1bg.GetComponent<SpriteRenderer>().size;
        var zone1TransitionBGSize= zone1TransitionBG.GetComponent<SpriteRenderer>().size;
        var zone2bgSize= zone2bg.GetComponent<SpriteRenderer>().size;
        var zone2TransitionBGSize= zone2TransitionBG.GetComponent<SpriteRenderer>().size;
        var zone3bgSize= zone3bg.GetComponent<SpriteRenderer>().size;

        zone0bg.transform.position = new Vector2(0, camera.transform.position.y - zone0bgSize.y / 1.8f);
        zone1bg.transform.position = new Vector2(0, zone0bg.transform.position.y - zone1bgSize.y);

        zone1TransitionBG.transform.position = new Vector2(0, zone1bg.transform.position.y - zone1bgSize.y);
        zone2bg.transform.position = new Vector2(0, zone1TransitionBG.transform.position.y - zone2bgSize.y);
        
        zone2TransitionBG.transform.position = new Vector2(0, zone2bg.transform.position.y - zone2bgSize.y);
        zone3bg.transform.position = new Vector2(0, zone2TransitionBG.transform.position.y - zone3bgSize.y);
        
        // Get where each zone starts and where the sea floor is
        _zone1Start = zone0bg.transform.position.y;
        _zone2Start = zone1TransitionBG.transform.position.y + zone1TransitionBGSize.y/2;
        _zone3Start = zone2TransitionBG.transform.position.y + zone2TransitionBGSize.y/2;
        oceanFloor = zone3bg.transform.position.y;
        
        // Generate a list of all fishes that can spawn for use in the collectable script
        allFishPrefabs = new List<GameObject>(zone1FishPrefabs.Count +
                                              zone2FishPrefabs.Count +
                                              zone3FishPrefabs.Count);
        allFishPrefabs.AddRange(zone1FishPrefabs);
        allFishPrefabs.AddRange(zone2FishPrefabs);
        allFishPrefabs.AddRange(zone3FishPrefabs);
    }
    
    private void FixedUpdate()
    {
        // Spawns fishes if there are less than max amount of fishes in the zone
        if ((_zone1Spawns.Count < maxAmountInZone1))
        {
            var fish = Instantiate(zone1FishPrefabs[ChooseFishToSpawn(zone1FishPrefabs)], GenerateRandomSpawnPos(new Vector2(-3, _zone1Start), new Vector2(3, _zone2Start)), Quaternion.identity);
            _zone1Spawns.Add(fish);
        }
        
        if ((_zone2Spawns.Count < maxAmountInZone2))
        {
            var fish = Instantiate(zone2FishPrefabs[ChooseFishToSpawn(zone2FishPrefabs)], GenerateRandomSpawnPos(new Vector2(-3, _zone2Start), new Vector2(3, _zone3Start)), Quaternion.identity);
            _zone2Spawns.Add(fish);
        }
        
        if ((_zone3Spawns.Count < maxAmountInZone3))
        {
            var fish = Instantiate(zone3FishPrefabs[ChooseFishToSpawn(zone3FishPrefabs)], GenerateRandomSpawnPos(new Vector2(-3, _zone3Start), new Vector2(3, oceanFloor)), Quaternion.identity);
            _zone3Spawns.Add(fish);
        }
    }

    // Generate a position within a specified area
    private Vector3 GenerateRandomSpawnPos(Vector3 topLeftCorner, Vector3 bottomRightCorner)
    {
        var x = Random.Range(bottomRightCorner.x, topLeftCorner.x);
        var y = Random.Range(bottomRightCorner.y, topLeftCorner.y);
        return new Vector3(x, y, 0);
    }
    
    // Gets a random item from a list
    private int ChooseFishToSpawn(List<GameObject> list)
    {
        int choice = Random.Range( 0, list.Count);
        return choice;
    }
    
    // Used to remove a fish from the spawned fish lists when it gets deleted so new can spawn
    public void RemoveFish(GameObject fish)
    {
        if (fish.name.Contains("1"))
        {
            //_zone1SpawnedFishNumber--;
            _zone1Spawns.Remove(fish);
        }
        if (fish.name.Contains("2"))
        {
            //_zone2SpawnedFishNumber--;
            _zone2Spawns.Remove(fish);
        }
        if (fish.name.Contains("3"))
        {
            //_zone3SpawnedFishNumber--;
            _zone3Spawns.Remove(fish);
        }
    }
}
