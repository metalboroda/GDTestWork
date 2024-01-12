using UnityEngine;
using UnityEngine.UI;

namespace GDTestWork
{
  public class UIManager : MonoBehaviour
  {
    [Header("Game Screen")]
    [SerializeField] private Image playerHealthbar;

    private void OnEnable()
    {
      EventManager.PlayerHealthChanged += UpdatePlayerHealthBar;
    }

    private void OnDisable()
    {
      EventManager.PlayerHealthChanged -= UpdatePlayerHealthBar;
    }

    public void UpdatePlayerHealthBar(int currentHealth, int maxHealth)
    {
      float fillAmount = (float)currentHealth / maxHealth;

      playerHealthbar.fillAmount = fillAmount;
    }
  }
}