namespace GDTestWork
{
  public class PlayerHandler : CharacterHandler
  {
    private void Start()
    {
      CurrentHealth = MaxHealth;
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
  }
}