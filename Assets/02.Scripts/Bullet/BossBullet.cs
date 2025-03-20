using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float Speed = 5f;
    public int Damage;

    protected virtual void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 dir = Vector2.up;
        transform.Translate(dir * Speed * Time.deltaTime, Space.Self);
    }
}
