using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "SKYBREAK/Player/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 30.0f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 3;

    [Header("Combat")]
    [SerializeField] private int maxBombCount = 2;

    public float MoveSpeed => moveSpeed;

    public int MaxHealth => maxHealth;

    public int MaxBombCount => maxBombCount;
}
