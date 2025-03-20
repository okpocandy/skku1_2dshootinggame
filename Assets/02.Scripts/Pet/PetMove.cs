using UnityEngine;
using UnityEngine.UIElements;

public class PetMove : MonoBehaviour
{
    public float MaxDistance = 1f;
    public float Speed = 3f;
    public float RoundCount = 1.5f;

    private GameObject _player;
    private Vector2 _direction;
    private float _angle = 0;
    

    private void Start()
    {

    }

    /*private void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        if(distance >= MaxDistance)
        {
            _direction = _player.transform.position - this.transform.position;
        }
        else
        {
            _direction = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0);
        }
        _direction.Normalize();
        transform.position += (Vector3)_direction * (Speed * Time.deltaTime);
        _angle += (2 * Mathf.PI / RoundCount) * Time.deltaTime; // 2초에 한 바퀴
        if(_angle > 360)
        {
            _angle = 0;
        }
    }
    */
    private void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null) return;

        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if (distance >= MaxDistance)
        {
            // Lerp로 천천히 따라가기
            transform.position = Vector3.Lerp(transform.position, _player.transform.position, Speed * Time.deltaTime);
        }
        else
        {
            // 플레이어 주변 원형 궤도
            Vector3 offset = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle) + 0.3f, 0);
            Vector3 targetPos = _player.transform.position + offset * MaxDistance;
            transform.position = Vector3.Lerp(transform.position, targetPos, Speed * Time.deltaTime);

            _angle += (2 * Mathf.PI / RoundCount) * Time.deltaTime;
            if (_angle > Mathf.PI * 2f)
            {
                _angle -= Mathf.PI * 2f;
            }
        }
    }
}
