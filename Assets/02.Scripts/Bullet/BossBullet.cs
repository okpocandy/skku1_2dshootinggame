using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float Speed = 5f;
    public int Damage = 5;

    protected virtual void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 dir = Vector2.up;
        transform.Translate(dir * Speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player enemy = collision.GetComponent<Player>();
            enemy.TakeDamage(Damage);

            Destroy(this.gameObject);
        }
    }
}
