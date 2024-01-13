using UnityEngine;
using UnityEngine.UI;

namespace GDTestWork
{
  public class EnemyUIHandler : MonoBehaviour
  {
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private Image healthbar;

    private EnemyController _enemyController;

    private void Awake()
    {
      _enemyController = GetComponent<EnemyController>();
    }

    private void OnEnable()
    {
      canvasObj.SetActive(true);
      healthbar.fillAmount = 1;

      _enemyController.StateMachine.StateChanged += EnableUI;
      _enemyController.HealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
      _enemyController.StateMachine.StateChanged -= EnableUI;
      _enemyController.HealthChanged -= UpdateHealthBar;
    }

    private void EnableUI(State state)
    {
      if (state is EnemyDeathState)
      {
        canvasObj.SetActive(false);
      }
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
      float fillAmount = (float)currentHealth / maxHealth;

      healthbar.fillAmount = fillAmount;
    }
  }
}