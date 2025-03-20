
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //  ī�޶� ����ũ�� ������ �����ϴ� ����
    public float ShakeIntensity = 0.2f;
    
    // ī�޶� ����ũ�� ���� �ð��� �����ϴ� ����
    public float ShakeDuration = 0.3f;

    // ī�޶��� ���� ��ġ�� ������ ����
    private Vector3 _originalPosition;

    private void Start()
    {
        // ī�޶��� �ʱ� ��ġ ����
        _originalPosition = transform.localPosition;
    }

    public void ShakeCamera()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elpased = 0.0f;
        while (elpased < ShakeDuration)
        {
            // ������ ��ġ ���
            float x = Random.Range(-1f, 1f) * ShakeIntensity;
            float y = Random.Range(-1f, 1f) * ShakeIntensity;

            // ī�޶� ��ġ ����
            transform.localPosition = new Vector3(_originalPosition.x + x, _originalPosition.y + y, _originalPosition.z);

            // ��� �ð� ������Ʈ
            elpased += Time.deltaTime;

            // ���� ������ ���� ���
            yield return null;
        }

        // ����ũ�� ������ ī�޶� ���� ��ġ�� ����
        transform.localPosition = _originalPosition;
    }
}
