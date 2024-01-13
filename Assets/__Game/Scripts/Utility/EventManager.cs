using UnityEngine.Events;

namespace GDTestWork
{
  public static class EventManager
  {
    #region Player
    public static event UnityAction<int, int> PlayerHealthChanged;
    public static void RaisePlayerHealthChanged(int currentHealth,
      int maxHealth) => PlayerHealthChanged?.Invoke(currentHealth, maxHealth);

    public static event UnityAction<int> PlayerHealthIncreased;
    public static void RaisePlayerHealthIncreased(int health) => PlayerHealthIncreased?.Invoke(health);
    #endregion

    #region Game
    public static event UnityAction<int> EnemyWaveChanged;
    public static void RaiseEnemyWaveChanged(int wave) => EnemyWaveChanged?.Invoke(wave);
    #endregion
  }
}