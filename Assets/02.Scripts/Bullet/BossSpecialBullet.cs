using UnityEngine;

public class BossSpecialBullet : BossBullet
{
    public float Timer = 0f;
    public GameObject BossBulletPrefab;


    private float _bulletCount = 4f;
    private float _radius = 0.5f;

    private float _deathTime = 0.2f;
    private bool _isStop = false;
    private float _timer = 0f;
    private float _fireCount = 5f;

    public Vector3 PinPosition;
    private void Awake()
    {
        _isStop = false;
    }
    protected override void Update()
    {
        base.Update();
        Timer += Time.deltaTime;
        if(Timer > _deathTime)
        {
            _isStop = true;
            PinPosition = transform.position;
        }
        if (!_isStop)
            return;
        transform.position = PinPosition;
        _timer += Time.deltaTime;
        if(_fireCount > 0)
        {
            if (_timer >= 0.3f)
            {
                _timer = 0f;
                _bulletCount = Random.Range(4, 10);
                for (int i = 0; i < _bulletCount; i++)
                {
                    float angle = i * (360 / _bulletCount);
                    float radian = angle * Mathf.Deg2Rad;

                    Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radian) * _radius, Mathf.Sin(radian) * _radius, 0);
                    Vector3 direction = (spawnPosition - transform.position).normalized;
                    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

                    Instantiate(BossBulletPrefab, spawnPosition, rotation);
                }
                _fireCount -= 1;
            }
        }
        if (_fireCount > 0)
            return;
        Destroy(gameObject);
    }
}
