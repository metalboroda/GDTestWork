namespace GDTestWork
{
  public class PlayerHandler : CharacterHandler
  {
    public override void Damage(int damage)
    {
      Health -= damage;

      if (Health <= 0)
      {
        Health = 0;

        Destroy(gameObject);
      }
    }
  }
}