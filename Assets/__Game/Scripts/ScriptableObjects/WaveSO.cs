using System;
using UnityEngine;

namespace GDTestWork
{
  [CreateAssetMenu(fileName = "Wave", menuName = "Data/Waves")]
  [Serializable]
  public class WaveSO : ScriptableObject
  {
    [field: SerializeField] public GameObject[] Characters { get; private set; }
  }
}