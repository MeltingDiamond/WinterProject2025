using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    public Camera camera;
    public GameObject zone0bg;
    
    public int maxAmountInZone1;
    public GameObject zone1bg;
    private List<GameObject> _zone1Spawns = new List<GameObject>();
    public List<GameObject> zone1FishPrefabs;
    
    public GameObject zone1TransitionBG;
    
    public int maxAmountInZone2;
    public GameObject zone2bg;
    private List<GameObject> _zone2Spawns = new List<GameObject>();
    public List<GameObject> zone2FishPrefabs;
    
    public GameObject zone2TransitionBG;
    
    public int maxAmountInZone3;
    public GameObject zone3bg;
    private List<GameObject> _zone3Spawns = new List<GameObject>();
    public List<GameObject> zone3FishPrefabs;

    public GameObject oceanFloorbg;

    private float _zone1Start;
    private float _zone2Start;
    private float _zone3Start;
    [HideInInspector] public float oceanFloor;
    
    public List<GameObject> allFishPrefabs;

    void Start()
    {
        var zone0bgSize = zone0bg.GetComponent<SpriteRenderer>().size;
        var zone1bgSize= zone1bg.GetComponent<SpriteRenderer>().size;
        var zone1TransitionBGSize= zone1TransitionBG.GetComponent<SpriteRenderer>().size;
        var zone2bgSize= zone2bg.GetComponent<SpriteRenderer>().size;
        var zone2TransitionBGSize= zone2TransitionBG.GetComponent<SpriteRenderer>().size;
        var zone3bgSize= zone3bg.GetComponent<SpriteRenderer>().size;

        zone0bg.transform.position = new Vector2(0, camera.transform.position.y - zone0bgSize.y / 2);
        zone1bg.transform.position = new Vector2(0, zone0bg.transform.position.y - zone1bgSize.y);

        zone1TransitionBG.transform.position = new Vector2(0, zone1bg.transform.position.y - zone1bgSize.y);
        zone2bg.transform.position = new Vector2(0, zone1TransitionBG.transform.position.y - zone2bgSize.y);
        
        zone2TransitionBG.transform.position = new Vector2(0, zone2bg.transform.position.y - zone2bgSize.y);
        zone3bg.transform.position = new Vector2(0, zone2TransitionBG.transform.position.y - zone3bgSize.y);
        oceanFloorbg.transform.position = new Vector2(0, zone3bg.transform.position.y - zone3bgSize.y);

        _zone1Start = zone0bg.transform.position.y;
        _zone2Start = zone1TransitionBG.transform.position.y + zone1TransitionBGSize.y/2;
        _zone3Start = zone2TransitionBG.transform.position.y + zone2TransitionBGSize.y/2;
        oceanFloor = oceanFloorbg.transform.position.y;
            
        allFishPrefabs = new List<GameObject>(zone1FishPrefabs.Count +
                                              zone2FishPrefabs.Count +
                                              zone3FishPrefabs.Count);
        allFishPrefabs.AddRange(zone1FishPrefabs);
        allFishPrefabs.AddRange(zone2FishPrefabs);
        allFishPrefabs.AddRange(zone3FishPrefabs);
    }
    
    private static Vector3[] GetSpriteCorners(SpriteRenderer renderer)
    {
        Vector3 topRight = renderer.transform.TransformPoint(renderer.sprite.bounds.max);
        Vector3 topLeft = renderer.transform.TransformPoint(new Vector3(renderer.sprite.bounds.max.x, renderer.sprite.bounds.min.y, 0));
        Vector3 botLeft = renderer.transform.TransformPoint(renderer.sprite.bounds.min);
        Vector3 botRight = renderer.transform.TransformPoint(new Vector3(renderer.sprite.bounds.min.x, renderer.sprite.bounds.max.y, 0));
        return new Vector3[] { topRight, topLeft, botLeft, botRight };
    }
    
    private void FixedUpdate()
    {
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

    private Vector3 GenerateRandomSpawnPos(Vector3 topLeftCorner, Vector3 bottomRightCorner)
    {
        var x = Random.Range(bottomRightCorner.x, topLeftCorner.x);
        var y = Random.Range(bottomRightCorner.y, topLeftCorner.y);
        return new Vector3(x, y, 0);
    }
    private int ChooseFishToSpawn(List<GameObject> list)
    {
        int choice = Random.Range( 0, list.Count);
        return choice;
    }

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
