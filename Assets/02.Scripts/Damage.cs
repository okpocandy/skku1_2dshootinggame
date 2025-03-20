using UnityEngine;

public enum DamageType
{
    Bullet,
    Boom,
    Lightning,
}

// 데미지를 추상화..
public struct Damage
{
    public int Value;
    public DamageType Type;
    public GameObject From;
}
