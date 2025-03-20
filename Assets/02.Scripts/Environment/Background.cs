using UnityEngine;

public class Background : MonoBehaviour
{
    // ���� ��ũ�Ѹ� : ��� �̹����� ������ �ӵ��� ������ ĳ���ͳ� ���� ���� �������� �� �������� ������ִ� ���
    //     �� ĳ���ʹ� �״�� �ΰ� ��游 �����̴� '������'

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
        _material = MySpriteRenderer.material;  // ���� ���͸����� ���纻 (�ν��Ͻ�)�� �����ؼ� ��ȯ
                                                // vs sharedMaterial = ������ ������ �� �ִ�.
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
        // ������ ���ϰ�,
        Vector2 direction = Vector2.up;

        // �������� ��ũ�Ѹ� �Ѵ�.
        //_material.mainTextureOffset += direction * ScrollSpeed * Time.deltaTime;
        
        _currentOffset += direction * ScrollSpeed * Time.deltaTime;
        MySpriteRenderer.GetPropertyBlock(_mpb);
        _mpb.SetVector(MainTexId, new Vector4(1f, 1f, _currentOffset.x, _currentOffset.y));
        MySpriteRenderer.SetPropertyBlock(_mpb);
        
        
    }
}
