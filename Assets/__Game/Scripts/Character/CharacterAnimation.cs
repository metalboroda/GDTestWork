using UnityEngine;

namespace GDTestWork
{
  public class CharacterAnimation : MonoBehaviour
  {
    [SerializeField] private CharAnimSO _charAnimSO;

    [field: Header("Param's")]
    [field: SerializeField] public float AnimatorSpeed { get; private set; } = 1f;
    [field: SerializeField] public float CrossDur { get; private set; } = 0.2f;
    [field: SerializeField] public float DampTime { get; private set; } = 0.1f;

    public Animator Animator { get; private set; }

    private void Awake()
    {
      Animator = GetComponent<Animator>();
    }

    private void Start()
    {
      Animator.speed = AnimatorSpeed;
    }

    public void MovementAnim()
    {
      PlayAnim(_charAnimSO.MovementBlendAnim);
    }

    public void MovementAnimValue(float value)
    {
      Animator.SetFloat(_charAnimSO.MovementBlendValue, value, DampTime, Time.deltaTime);
    }

    public void IdleFightAnim()
    {
      PlayAnim(_charAnimSO.IdleFightAnim);
    }

    private void PlayAnim(string animName)
    {
      Animator.CrossFadeInFixedTime(animName, CrossDur);
    }
  }
}