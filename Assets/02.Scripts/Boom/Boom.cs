using Unity.VisualScripting;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public int Damage = 1000;
    public GameObject ExplosionPrefab;


    private float _lifeTime = 2f;
    private float _time = 0;

    private void Start()
    {
        GameObject vfx =  Instantiate(ExplosionPrefab);
        vfx.transform.position = this.transform.position;
        
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _lifeTime)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy == null) return;
            Damage damage = new Damage()
            {
                Value = 100000,
                Type = DamageType.Boom,
            };
            
            enemy.TakeDamage(damage);
        }
    }
}
