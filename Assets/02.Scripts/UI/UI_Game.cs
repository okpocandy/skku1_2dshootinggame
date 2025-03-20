using UnityEngine;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;


public class UI_Game : MonoBehaviour
{
    public static UI_Game Instance { get; private set; } // 싱글톤 인스턴스
    
    public List<GameObject> Booms;
    public TextMeshProUGUI KillText;
    public TextMeshProUGUI ScoreText;
    public GameObject WarningTextL;
    public GameObject WarningTextR;
    public GameObject HealthSlider;

    public float WarningTextLength = 1000f;
    public float WarningDuration = 3f;

    public Vector3 TargetScale;
    public Vector3 CurrentScale;

    private const float _duration = 0.2f;
 
    [SerializeField]
    private float _blinkDurationIn = 0.3f;
    [SerializeField]
    private float _blinkDurationOut = 0.7f;


    private int _score = 0;

    private const string XorKey = "dm2ldiv";
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // 이미 인스턴스가 존재하면 새로 생성된 객체를 제거합니다.
            Destroy(gameObject);
            return;
        }

        // 현재 객체를 싱글톤 인스턴스로 설정합니다.
        Instance = this;


    }
    public void WarningOn()
    {
        WarningTextL.SetActive(true);
        WarningTextR.SetActive(true);
        WarningTextR.transform.DOLocalMoveY(WarningTextR.transform.position.y + WarningTextLength, WarningDuration);
        WarningTextL.transform.DOLocalMoveY(WarningTextL.transform.position.y - WarningTextLength, WarningDuration);
        WarningTextL.GetComponent<TextMeshProUGUI>().DOFade(0, _blinkDurationIn).OnComplete(()=>
        {
            WarningTextL.GetComponent<TextMeshProUGUI>().DOFade(1, _blinkDurationOut);
        }).SetLoops(-1, LoopType.Yoyo);



        WarningTextR.GetComponent<TextMeshProUGUI>().DOFade(0, _blinkDurationIn).OnComplete(() =>
        {
            WarningTextR.GetComponent<TextMeshProUGUI>().DOFade(1, _blinkDurationOut);
        }).SetLoops(-1, LoopType.Yoyo);
       


    }
    public void WarningOff()
    {
        WarningTextL.SetActive(false);
        WarningTextR.SetActive(false);
    }

    public void Save()
    {
        PlayerData player = new PlayerData
        {
            Score = _score,
            KillCount = Enemy.DiedEnemyCount,
            BoomCount = (int)Enemy.DiedEnemyCount/3,
        };
        string jsonPlayer = JsonUtility.ToJson(player);
        string encrypted = XorEncryptDecrypt(jsonPlayer, XorKey);
        PlayerPrefs.SetString("PlayerData", encrypted);
        
    }

    public void Load()
    {
        string encrypted = PlayerPrefs.GetString("PlayerData");
        
        if (string.IsNullOrEmpty(encrypted)) return;
        
        string decrypted = XorEncryptDecrypt(encrypted, XorKey);
        Debug.Log(decrypted);
        PlayerData loadedPlayer = JsonUtility.FromJson<PlayerData>(decrypted);
        JsonUtility.FromJsonOverwrite(decrypted, loadedPlayer);
        
        _score = loadedPlayer.Score;
        Enemy.DiedEnemyCount = loadedPlayer.KillCount;
    }

    private string XorEncryptDecrypt(string text, string key)
    {
        char[] result = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            result[i] = (char)(text[i] ^ key[i % key.Length]);
        }
        return new string(result);
    }

    public void Refresh(int boomCount, int killCount, int score)
    {
        for (int i = 0; i < 3; ++i)
        {
            Booms[i].SetActive(i < boomCount);
        }
        KillText.text = $"Kills: {killCount}";
        _score += score;
        ScoreText.text = _score.ToString("N0");
        ScoreText.transform.DOScale(TargetScale, _duration).SetEase(Ease.InOutBack)
        .OnComplete(() =>
        {
            ScoreText.transform.DOScale(CurrentScale, _duration);
        });
        //Save();
    }
    
    public void BossHealthSliderOn()
    {
        HealthSlider.SetActive(true);
    }
    public void BossHealth(float healthRate)
    {
        Slider bossHealth = HealthSlider.GetComponent<Slider>();
        bossHealth.value = healthRate;
    }
    public void BossHealthSliderOff()
    {
        HealthSlider.SetActive(false);
    }
}
