namespace GDTestWork
{
  public class EnemyHandler : CharacterHandler
  {
    private EnemyController _enemyController;

    private void Awake()
    {
      _enemyController = GetComponent<EnemyController>();
    }

    private void Start()
    {
      CurrentHealth = Health;
    }

    public override void Damage(int damage)
    {
      CurrentHealth -= damage;

      if (CurrentHealth <= 0)
      {
        CurrentHealth = 0;

        _enemyController.EnemyPool.ReturnObjectToPool(_enemyController);
      }
    }
  }
}