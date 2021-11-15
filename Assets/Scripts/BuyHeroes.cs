using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuyHeroes : MonoBehaviour
{
    [SerializeField] private int _heroCost;
    [SerializeField] private GameObject _heroPrefab;
    public static List<GameObject> heroes = new List<GameObject>();

    public void BuyHero()
    {
        int coinCount = Convert.ToInt32(SQLscript.RequestSelectExecution("SELECT coinCount FROM playerinfo WHERE idPlayer = 1"));

        if (coinCount < _heroCost)
            Debug.Log("Not enough coins");
        
        else
        {
            SQLscript.RequestExecution($"UPDATE playerinfo SET coinCount = coinCount - { _heroCost }");
            GameObject.Find("Text").GetComponent<Text>().text = Convert.ToString(coinCount - _heroCost);

            heroes.Add(Instantiate(_heroPrefab, Spawner.heroSpawnPos, Quaternion.identity));
        }
    }
}

[Serializable] public class Character
{
    public string Name;
    public Vector3 Position;

    public Character(string name, Vector3 position)
    {
        Name = name.Split('(')[0];
        Position = position;
    }
}