using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDTestWork
{
  public class SceneController : MonoBehaviour
  {
    public void RestartCurrentScene()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }
}