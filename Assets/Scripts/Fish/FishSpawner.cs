using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    public GameObject zone0bg;
    
    public int maxAmountInZone1;
    public GameObject zone1bg;
    private List<GameObject> _zone1Spawns = new List<GameObject>();
    public List<GameObject> zone1FishPrefabs;
    
    public int maxAmountInZone2;
    public GameObject zone2bg;
    private List<GameObject> _zone2Spawns = new List<GameObject>();
    public List<GameObject> zone2FishPrefabs;
    
    public int maxAmountInZone3;
    public GameObject zone3bg;
    private List<GameObject> _zone3Spawns = new List<GameObject>();
    public List<GameObject> zone3FishPrefabs;

    public float zone1Start;
    public float zone2Start;
    public float zone3Start;
    public float oceanFloor;
    
    public List<GameObject> allFishPrefabs;

    void Start()
    {
        zone0bg.transform.position = new Vector2(0, zone1Start);
        zone1bg.transform.position = new Vector2(0, zone2Start);
        zone2bg.transform.position = new Vector2(0, zone3Start);
        zone3bg.transform.position = new Vector2(0, oceanFloor);
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
            var fish = Instantiate(zone1FishPrefabs[ChooseFishToSpawn(zone1FishPrefabs)], GenerateRandomSpawnPos(new Vector2(-3, zone1Start), new Vector2(3, zone2Start)), Quaternion.identity);
            _zone1Spawns.Add(fish);
        }
        
        if ((_zone2Spawns.Count < maxAmountInZone2))
        {
            var fish = Instantiate(zone2FishPrefabs[ChooseFishToSpawn(zone2FishPrefabs)], GenerateRandomSpawnPos(new Vector2(-3, zone2Start), new Vector2(3, zone3Start)), Quaternion.identity);
            _zone2Spawns.Add(fish);
        }
        
        if ((_zone3Spawns.Count < maxAmountInZone3))
        {
            var fish = Instantiate(zone3FishPrefabs[ChooseFishToSpawn(zone3FishPrefabs)], GenerateRandomSpawnPos(new Vector2(-3, zone3Start), new Vector2(3, oceanFloor)), Quaternion.identity);
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
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector2(-3, zone1Start),  new Vector2(3, zone1Start));
        Gizmos.DrawLine(new Vector2(-3, zone2Start),  new Vector2(3, zone2Start));
        Gizmos.DrawLine(new Vector2(-3, zone3Start),  new Vector2(3, zone3Start));
        Gizmos.DrawLine(new Vector2(-3, oceanFloor),  new Vector2(3, oceanFloor));
    }
}
