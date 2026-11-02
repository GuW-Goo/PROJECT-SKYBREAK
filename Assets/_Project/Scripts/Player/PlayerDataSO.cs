using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "SKYBREAK/Player/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Combat")]
    [SerializeField] private int attackPower = 1;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 30.0f;

    public int AttackPower => attackPower;
    public float MoveSpeed => moveSpeed;
}
