using TMPro;
using UnityEngine;

public class DistanceUpdater : MonoBehaviour
{
    public TMP_Text muhText;
    public Transform fishingHook;
    
    void Update()
    {
        // Update with the fishing hooks y position
        muhText.text = $"{fishingHook.position.y.ToString()} MUH";
    }
}
