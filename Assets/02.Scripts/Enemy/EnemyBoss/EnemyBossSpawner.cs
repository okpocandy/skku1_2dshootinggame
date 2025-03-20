using System.Collections.Generic;
using UnityEngine;



public class EnemyBossSpawner : MonoBehaviour
{
    public List<GameObject> EnemySpawners;
    public GameObject BossPrefab;

    public int BossCounter = 10;
    public bool _isDestory = false;


    private float _timer;
    [SerializeField]
    private float _warningTimer = 2f;
    private bool _isBossOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject boss = null;
        if (Enemy.DiedEnemyCount == BossCounter)
        {
            if (_isBossOn)
                return;
            boss = Instantiate(BossPrefab);
            boss.transform.position = this.transform.position;
            UI_Game.Instance.WarningOn();
            _isBossOn = true;
            foreach (var enemySpawner in EnemySpawners)
            {
                enemySpawner.SetActive(false);
            }

        }
        WarnUI();
        if(_isDestory)
        {
            foreach (var enemySpawner in EnemySpawners)
            {
                enemySpawner.SetActive(true);
                _isBossOn = false;
               
            }
        }
    }

    private void WarnUI()
    {
        if (_isBossOn)
        {
            _timer += Time.deltaTime;
            if (_timer >= _warningTimer)
            {
                UI_Game.Instance.WarningOff();
                _isBossOn = false;
                _timer = 0f;
            }
        }
    }
}
