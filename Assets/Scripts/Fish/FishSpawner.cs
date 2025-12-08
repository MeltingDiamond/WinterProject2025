using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public List<GameObject> zone1FishPrefabs;
    public List<GameObject> zone2FishPrefabs;
    public List<GameObject> zone3FishPrefabs;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private int ChooseFishToSpawn(List<GameObject> list)
    {
        int choice = Random.Range( 0, list.Count -1);
        return choice;
    }
}
