using UnityEngine;
using UnityEngine.SceneManagement;

public class FishButtonController : MonoBehaviour
{
    public void OnFishClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnCollectionClisk()
    {
        SceneManager.LoadScene("Catalogue");
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("Credits");
    }
}
