
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //  카메라 셰이크의 강도를 조절하는 변수
    public float ShakeIntensity = 0.2f;
    
    // 카메라 셰이크의 지속 시간을 조절하는 변수
    public float ShakeDuration = 0.3f;

    // 카메라의 원래 위치를 저장할 변수
    private Vector3 _originalPosition;

    private void Start()
    {
        // 카메라의 초기 위치 저장
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
            // 무작위 위치 계산
            float x = Random.Range(-1f, 1f) * ShakeIntensity;
            float y = Random.Range(-1f, 1f) * ShakeIntensity;

            // 카메라 위치 변경
            transform.localPosition = new Vector3(_originalPosition.x + x, _originalPosition.y + y, _originalPosition.z);

            // 경과 시간 업데이트
            elpased += Time.deltaTime;

            // 다음 프레임 까지 대기
            yield return null;
        }

        // 셰이크가 끝나면 카메라를 원래 위치로 복귀
        transform.localPosition = _originalPosition;
    }
}
