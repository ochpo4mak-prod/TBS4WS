using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleSpawner : MonoBehaviour
{
    public GameObject[] battleHeroes;
    public GameObject[] battleEnemies;
    public GameObject[] spawnPos;
    public GameObject spawnEnemyPos;

    void Start()
    {
        switch (globals.enemy)
        {
            case "enemy1":
                Instantiate(battleEnemies[0], spawnEnemyPos.transform.position, Quaternion.identity);
                break;
            
            case "enemy2":
                Instantiate(battleEnemies[1], spawnEnemyPos.transform.position, Quaternion.identity);
                break;
            
            case "enemy3":
                Instantiate(battleEnemies[2], spawnEnemyPos.transform.position, Quaternion.identity);
                break;
            
            case "enemy4":
                Instantiate(battleEnemies[3], spawnEnemyPos.transform.position, Quaternion.identity);
                break;
            
            case "enemy5":
                Instantiate(battleEnemies[4], spawnEnemyPos.transform.position, Quaternion.identity);
                break;
            
            case "enemy6":
                Instantiate(battleEnemies[5], spawnEnemyPos.transform.position, Quaternion.identity);
                break;
            
            case "enemy7":
                Instantiate(battleEnemies[6], spawnEnemyPos.transform.position, Quaternion.identity);
                break;
        }

        try
        {
            for (int i = 0; i < 3; i++)
            {
                switch (globals.heroesNearEnemy[i])
                {
                    case "hero1":
                        Instantiate(battleHeroes[0], spawnPos[i].transform.position, Quaternion.identity);
                        break;
                    
                    case "hero2":
                        Instantiate(battleHeroes[1], spawnPos[i].transform.position, Quaternion.identity);
                        break;
                    
                    case "hero3":
                        Instantiate(battleHeroes[2], spawnPos[i].transform.position, Quaternion.identity);
                        break;
                    
                    case "hero4":
                        Instantiate(battleHeroes[3], spawnPos[i].transform.position, Quaternion.identity);
                        break;
                    
                    case "hero5":
                        Instantiate(battleHeroes[4], spawnPos[i].transform.position, Quaternion.identity);
                        break;
                    
                    case "hero6":
                        Instantiate(battleHeroes[5], spawnPos[i].transform.position, Quaternion.identity);
                        break;
                    
                    case "hero7":
                        Instantiate(battleHeroes[6], spawnPos[i].transform.position, Quaternion.identity);
                        break;
                }
            }
        }
        catch
        {
        }
    }
}
