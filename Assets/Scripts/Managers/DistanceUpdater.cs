using TMPro;
using UnityEngine;

public class DistanceUpdater : MonoBehaviour
{
    public TMP_Text muhText;
    public Transform fishingHook;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        muhText.text = $"{fishingHook.position.y.ToString()} MUH";
    }
}
