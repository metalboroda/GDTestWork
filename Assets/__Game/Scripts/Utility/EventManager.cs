using UnityEngine.Events;

namespace GDTestWork
{
  public static class EventManager
  {
    #region Player
    public static event UnityAction<int, int> PlayerHealthChanged;
    public static void RaisePlayerHealthChanged(int currentHealth,
      int maxHealth) => PlayerHealthChanged?.Invoke(currentHealth, maxHealth);
    #endregion
  }
}