using UnityEngine;

public class Player : MonoBehaviour
{
    // "응집도"는 높히고! "결합도"는 낮춰라
    // 응집도: 데이터와 데이터를 조작하는 로직이 한 곳에 모여 있는 구조 -> 응집도가 높은 구조
    // 결합도: 두 클래스간에 상호작용 의존 정도
    // 내 코드가 그렇게 이상한가요?(C#) -> 도메인 주도 설계 철저 입문(C#)
    
    
    public int   Health = 100;
    public float MoveSpeed = 3f;
    public float AttackCooltime  = 0.6f;

    public float Defence = 0.2f;
    public Lightning LightPrefab;

    // - 모드(자동, 수동)
    public PlayMode PlayMode = PlayMode.Mannual;

    private CameraShake _cameraShake;

    // light 오브젝트에 대한 참조를 저장할 변수
    private GameObject muzzleLight;

    // 불빛의 현재 상태를 추적하는 변수
    private bool isLightningOn = false;

    private void Start()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();

        Transform muzzleTransform = transform.Find("LightningMuzzle");
        if (muzzleTransform != null)
        {
            Transform lightningTransform = muzzleTransform.Find("fx_lightning");

            if (lightningTransform != null)
            {
                muzzleLight = lightningTransform.gameObject;

                muzzleLight.SetActive(false);
            }
            else
            {
                Debug.LogError("Light 오브젝트를 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.LogError("Muzzle 오브젝트를 찾을 수 없습니다!");
        
        }
        //UI_Game.Instance.Load();

    }
    public void TakeDamage(int damage)
    {
        Health -= (int)(damage * Defence);
        if (_cameraShake != null)
        {
            _cameraShake.ShakeCamera();
        }

        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        // 키 입력 검사
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayMode = PlayMode.Auto;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayMode = PlayMode.Mannual;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if(muzzleLight != null)
            {
                isLightningOn = !isLightningOn;
                if(isLightningOn)
                {
                    PlayMode = PlayMode.Lightning;
                    MoveSpeed *= 2.5f;
                }
                else
                {
                    PlayMode = PlayMode.Mannual;
                    MoveSpeed /= 2.5f;
                }
                
                muzzleLight.SetActive(isLightningOn);
            }
        }

    }
}
