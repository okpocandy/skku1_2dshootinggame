using Unity.VisualScripting;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public GameObject LightningMuzzle;
    public int Damgae;
    private bool _isLightningMode = false;
    private int _damage;

    private void Start()
    {
        transform.position = LightningMuzzle.transform.position + Vector3.up * 2;
        _damage = (int)(this.Damgae * Time.deltaTime);
    }
    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("err");
        if (collision.CompareTag("Enemy"))
        {
            Damage damage = new Damage()
            {
                Value = _damage,
                Type = DamageType.Lightning,
            };
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }
}
