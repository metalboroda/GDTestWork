using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDTestWork
{
  public class UIManager : MonoBehaviour
  {
    [Header("Player Screen")]
    [SerializeField] private Image playerHealthbar;

    [Header("Game Screen")]
    [SerializeField] private TextMeshProUGUI waveCounterTxt;

    private void OnEnable()
    {
      EventManager.PlayerHealthChanged += UpdatePlayerHealthBar;
      EventManager.EnemyWaveChanged += UpdateWaveCounter;
    }

    private void OnDisable()
    {
      EventManager.PlayerHealthChanged -= UpdatePlayerHealthBar;
      EventManager.EnemyWaveChanged -= UpdateWaveCounter;
    }

    private void UpdatePlayerHealthBar(int currentHealth, int maxHealth)
    {
      float fillAmount = (float)currentHealth / maxHealth;

      playerHealthbar.fillAmount = fillAmount;
    }

    private void UpdateWaveCounter(int wave)
    {
      waveCounterTxt.SetText($"WAVE {wave + 1}");
    }
  }
}