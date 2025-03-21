using UnityEngine;

public class Background : MonoBehaviour
{
    // 베경 스크롤링 : 배경 이미지를 일정한 속도로 움직여 캐리터나 몬스터 등의 움직임을 더 동적으로 만들어주는 기술
    //     ㄴ 캐릭터는 그대로 두고 배경만 움직이는 '눈속임'

    private Material _material;
    public SpriteRenderer MySpriteRenderer;
    public float ScrollSpeed = 0.6f;
    public float FixScrollSpeed = 0.2f;
    public Player Player;

    private MaterialPropertyBlock _mpb;
    
    private static readonly int MainTexId = Shader.PropertyToID("_MainTex_ST");
    //
    private Vector2 _currentOffset;
    //


    private void Awake()
    {
        
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        /*
        _material = MySpriteRenderer.material;  // 원본 머터리얼의 복사본 (인스턴스)를 생성해서 반환
                                                // vs sharedMaterial = 원본을 가져올 수 있다.
        MySpriteRenderer.material = _material;
        */
        
        _mpb = new MaterialPropertyBlock();
        


    }

    private void Update()
    {
        if (Player.PlayMode == PlayMode.Lightning)
        {
            ScrollSpeed = FixScrollSpeed * 5;
        }
        else
        {
            ScrollSpeed = FixScrollSpeed;
        }
        // 방향을 구하고,
        Vector2 direction = Vector2.up;

        // 방향으로 스크롤링 한다.
        //_material.mainTextureOffset += direction * ScrollSpeed * Time.deltaTime;
        
        _currentOffset += direction * ScrollSpeed * Time.deltaTime;
        MySpriteRenderer.GetPropertyBlock(_mpb);
        _mpb.SetVector(MainTexId, new Vector4(1f, 1f, _currentOffset.x, _currentOffset.y));
        MySpriteRenderer.SetPropertyBlock(_mpb);
        
        
    }
}
