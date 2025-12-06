using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject zone1bg;
    public GameObject zone2bg;
    public GameObject zone3bg;
    
    private void Start()
    {
        zone1bg.transform.position = Vector3.zero;
        var bg1Size = zone1bg.GetComponent<SpriteRenderer>().size;
        
        zone2bg.transform.position = new Vector3(0, bg1Size.y / 2, 0);
        var bg2Size = zone2bg.GetComponent<SpriteRenderer>().size;
        
        zone3bg.transform.position = new Vector3(0, bg2Size.y / 2, 0);
        var bg3Size = zone3bg.GetComponent<SpriteRenderer>().size;
    }
}
