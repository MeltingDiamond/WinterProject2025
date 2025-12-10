using UnityEngine;
using UnityEngine.SceneManagement;

public class FishButtonController : MonoBehaviour
{
    public void OnFishClick()
    {
        SceneManager.LoadScene("AdrianTest");
    }

    public void OnCollectionClisk()
    {
        SceneManager.LoadScene("");
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("Credits");
    }
}
