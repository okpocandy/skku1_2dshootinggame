using UnityEngine;

public class BoomSpawner : MonoBehaviour
{
    public GameObject BoomPrefab;

    private int _minEnemyCount = 20;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Enemy.DiedEnemyCount < _minEnemyCount)
                return;
            Enemy.DiedEnemyCount -= 20;
            Instantiate(BoomPrefab);
        }
    }
}
