using Mono.Cecil;
using UnityEngine;


[CreateAssetMenu(fileName ="WaveData", menuName = "ScriptableObject")]
public class WaveData : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject enemyPrefab;
    public int enemyCount;
    public float enemySpacing;
    public float enemyRespawnTime;
    public int enemyHealth;
    public int currencyPerEnemy;
    public float enemySpeed;

    
}
