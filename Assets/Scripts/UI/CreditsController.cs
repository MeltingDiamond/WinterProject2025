using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public void OnCloseClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
