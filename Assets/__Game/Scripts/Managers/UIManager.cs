using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GDTestWork
{
  public class UIManager : MonoBehaviour
  {
    [Header("Player Screen")]
    [SerializeField] private Image playerHealthbar;

    [Header("Game Screen")]
    [SerializeField] private TextMeshProUGUI waveCounterTxt;
    [SerializeField] private Button restartButton;

    [Inject] private SceneController _sceneController;

    private void OnEnable()
    {
      EventManager.PlayerHealthChanged += UpdatePlayerHealthBar;
      EventManager.EnemyWaveChanged += UpdateWaveCounter;
    }

    private void Start()
    {
      SubscribeButtons();
    }

    private void OnDisable()
    {
      EventManager.PlayerHealthChanged -= UpdatePlayerHealthBar;
      EventManager.EnemyWaveChanged -= UpdateWaveCounter;
    }

    private void SubscribeButtons()
    {
      restartButton.onClick.AddListener(() => { _sceneController.RestartCurrentScene(); });
    }

    private void UpdatePlayerHealthBar(int currentHealth, int maxHealth)
    {
      float fillAmount = (float)currentHealth / maxHealth;

      playerHealthbar.fillAmount = fillAmount;
    }

    private void UpdateWaveCounter(int wave)
    {
      waveCounterTxt.SetText($"WAVE {wave + 1}");
      waveCounterTxt.transform.DOPunchRotation(new Vector3(0, 0, 3), 0.25f);
    }
  }
}