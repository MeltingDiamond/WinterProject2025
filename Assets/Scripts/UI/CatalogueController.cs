using UnityEngine;
using UnityEngine.SceneManagement;

public class CatalogueController : MonoBehaviour
{
   public void OnCloseClick()
   {
      SceneManager.LoadScene("MainMenu");
   }
}
