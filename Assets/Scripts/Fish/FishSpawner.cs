using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public List<GameObject> zone1FishPrefabs;
    public int maxAmountInZone1;
    private List<GameObject> _zone1Spawns;
    
    public List<GameObject> zone2FishPrefabs;
    public int maxAmountInZone2;
    private List<GameObject> _zone2Spawns;
    
    public List<GameObject> zone3FishPrefabs;
    public int maxAmountInZone3;
    private List<GameObject> _zone3Spawns;
    
    void Start()
    {
        
    }

    void Update()
    {
        //if ((_zone1Spawns.Count < maxAmountInZone1))
        //{
          //  var fish = Instantiate(zone1FishPrefabs[ChooseFishToSpawn(zone1FishPrefabs)], GenerateRandomSpawnPos());
            //_zone1Spawns.Add(fish);
        //}
        
        //if ((_zone2Spawns.Count < maxAmountInZone2))
        //{
          //  var fish = Instantiate(zone2FishPrefabs[ChooseFishToSpawn(zone2FishPrefabs)], GenerateRandomSpawnPos());
            //_zone2Spawns.Add(fish);
        //}
        
       // if ((_zone3Spawns.Count < maxAmountInZone3))
        //{
         //   var fish = Instantiate(zone3FishPrefabs[ChooseFishToSpawn(zone3FishPrefabs)], GenerateRandomSpawnPos());
           // _zone3Spawns.Add(fish);
        //}
    }

    private Vector2 GenerateRandomSpawnPos(Vector2 topLeftCorner, Vector2 bottomRightCorner)
    {
        var x = Random.Range(bottomRightCorner.x, topLeftCorner.x);
        var y = Random.Range(bottomRightCorner.y, bottomRightCorner.y);
        return new Vector2(x, y);
    }
    private int ChooseFishToSpawn(List<GameObject> list)
    {
        int choice = Random.Range( 0, list.Count -1);
        return choice;
    }
}
