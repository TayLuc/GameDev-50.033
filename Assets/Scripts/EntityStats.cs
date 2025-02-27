using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Game/Entity Stats", order = 1)]
public class EntityStats : ScriptableObject
{
    [Header("Basic Stats")]
    public float maxHealth = 100f;
    public float movementSpeed = 5f;
    public float cost = 100f;

    [Header("Combat Stats")]
    public float attackDamage = 10f;
    public float attackSpeed = 1f;
    public float attackRange = 2f;
    public Sprite bulletSprite;




}