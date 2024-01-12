namespace GDTestWork
{
  public class PlayerHandler : CharacterHandler
  {
    private void OnEnable()
    {
      EventManager.PlayerHealthIncreased += IncreaseHealth;
    }

    private void Start()
    {
      CurrentHealth = MaxHealth;
    }

    private void OnDisable()
    {
      EventManager.PlayerHealthIncreased -= IncreaseHealth;
    }

    public override void Damage(int damage)
    {
      CurrentHealth -= damage;

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;

        Destroy(gameObject);
      }

      EventManager.RaisePlayerHealthChanged(CurrentHealth, MaxHealth);
    }

    private void IncreaseHealth(int health)
    {
      CurrentHealth += health;

      if (CurrentHealth >= MaxHealth)
      {
        CurrentHealth = MaxHealth;
      }

      EventManager.RaisePlayerHealthChanged(CurrentHealth, MaxHealth);
    }
  }
}