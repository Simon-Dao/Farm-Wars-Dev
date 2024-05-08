using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int maxCoins;
    public float spawnInterval;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public static int numCoins;

    // Start is called before the first frame update
    void Start()
    {
        numCoins = 0;
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (numCoins < maxCoins)
            {   
                numCoins++;
                Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                PhotonNetwork.Instantiate(coinPrefab.name, randomPos, Quaternion.identity);
            }
        }
    }
}
