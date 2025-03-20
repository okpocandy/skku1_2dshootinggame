using UnityEngine;



public class PetFire : MonoBehaviour
{
    public GameObject SubBulletPrefab;
    public GameObject[] SubMuzzles;
    public float Cooltimer = 0f;

    private Player _player;

    private void Update()
    {
        Cooltimer -= Time.deltaTime;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


        // 쿨타임이 아직 안됐으면 종료
        if (Cooltimer > 0)
        {
            return;
        }

        if (_player.PlayMode == PlayMode.Auto || Input.GetButtonDown("Fire1"))
        {
            foreach (GameObject subMuzzle in SubMuzzles)
            {
                GameObject subBullet = Instantiate(SubBulletPrefab); // 인스턴스화

                subBullet.transform.position = subMuzzle.transform.position;
            }

            Cooltimer = _player.AttackCooltime;
        }
    }
}
