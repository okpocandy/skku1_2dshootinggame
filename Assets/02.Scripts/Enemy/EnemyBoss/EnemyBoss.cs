
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Normal,
    Rotate,
    Random,
}

public class EnemyBoss : MonoBehaviour
{

    public float Speed = 2f;
    public GameObject BossBulletPrefab;
    public GameObject BossBoomBulletPrefab;
    public GameObject ExplosionVFXPrefab;
    public GameObject ExplosionBFXPrefab;
    public GameObject BossSpawner;

    public int _bulletCount = 12;
    public float _radius = 3f;

    public float Health = 1000f;
    public float MaxHealth = 1000f;

    public float AttackCooltime = 1f;
    public float RotateAttackCooltime = 0.2f;

    public float Cooltimer = 0f;

    public float RotateModeRotateSpeed = 720f;


    [SerializeField]
    private float _rotateModeR = 0f;

    private bool _isPosition = false;
    [SerializeField]
    private AttackType _attackType = AttackType.Normal;

    private float _randomCooltimer = 0f;
    private float _randomDirectionChangeCooltime = 0.3f;

    private Animator _animator;
    private PlayerMove Player;
    private void Start()
    {
        Player = FindAnyObjectByType<PlayerMove>();
        _animator = GetComponent<Animator>();
        UI_Game.Instance.BossHealthSliderOn();
        
    }

    private void Update()
    {
        if (_isPosition == false)
        {
            Move();
        }
        else
        {
            Cooltimer -= Time.deltaTime;
            if (Cooltimer > 0)
                return;

            switch (_attackType)
            {
                case AttackType.Normal:
                    {
                        _rotateModeR = 0;
                        AttackCooltime = 1f;
                        Fire(_rotateModeR, BossBulletPrefab);
                        break;
                    }
                case AttackType.Rotate:
                    {
                        _rotateModeR += RotateModeRotateSpeed * Time.deltaTime;
                        if (_rotateModeR >= 360)
                        {
                            _rotateModeR = 0;
                        }
                        AttackCooltime = RotateAttackCooltime;
                        Fire(_rotateModeR, BossBulletPrefab);
                        break;
                    }
                case AttackType.Random:
                    {
                        AttackCooltime = 0.5f;
                        _bulletCount = Random.Range(3, 6);
                        Fire(0, BossBoomBulletPrefab);
                        break;
                    }
            }


        }

    }

    private void Move()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
        if (transform.position.y <= 2.5)
        {
            transform.position = new Vector3(0, 2.5f, 0);
            _isPosition = true;
        }
    }

    private void Fire(float r, GameObject bulletPrefab)
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            float angle = i * (360 / _bulletCount) + r;
            float radian = angle * Mathf.Deg2Rad;

            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radian) * _radius, Mathf.Sin(radian) * _radius, 0);
            Vector3 direction = (spawnPosition - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

            Instantiate(bulletPrefab, spawnPosition, rotation);
        }
        Cooltimer = AttackCooltime;
    }

    public void TakeDamage(Damage damage)
    {
        Health -= damage.Value;
        UI_Game.Instance.BossHealth(Health / MaxHealth);

        _animator.SetTrigger("Hit");

        if (Health <= 0)
        {
            OnDeath(damage);
            Destroy(this.gameObject);
        }
    }
         private void OnDeath(Damage damage)
    {
        UI_Game.Instance.BossHealthSliderOff();
        UI_Game.Instance.Refresh((int)Enemy.DiedEnemyCount / 20, Enemy.DiedEnemyCount, Enemy._score + 10000);
        GameObject bfx = Instantiate(ExplosionVFXPrefab);
        bfx.transform.position = this.transform.position;
        GameObject vfx = Instantiate(ExplosionVFXPrefab);
        vfx.transform.position = this.transform.position;
        EnemyBossSpawner spawner = FindFirstObjectByType<EnemyBossSpawner>();
        spawner._isDestory = true;
    }

}
