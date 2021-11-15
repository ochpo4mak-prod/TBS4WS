using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Tilemap _tilemapCol;

    [SerializeField] private TileBase _castle;
    [SerializeField] private TileBase _forest;
    [SerializeField] private TileBase _hexagone;

    [SerializeField] private GameObject[] _enemyArr;
    [SerializeField] private GameObject[] _heroesArr;
    [SerializeField] private GameObject _startHero;

    [SerializeField] private int EnemyCount;
    [SerializeField] private int CoinCount;

    [SerializeField] private Text moneyText;

    public static Vector3 heroSpawnPos;
    public static Vector2Int castleCoords;
    public static List<GameObject> enemies = new List<GameObject>();
    public static List<Character> characters = new List<Character>();
    private List<Vector3Int> _coords = new List<Vector3Int>();

    void Start()
    {
        int x, y;

        if (!globals.isGenerateMap)
        {
            x = UnityEngine.Random.Range(1, 149);
            y = UnityEngine.Random.Range(1, 149);
            castleCoords = new Vector2Int(x, y);
            EnemySpawnOnLaunch(x, y);
            SQLscript.RequestExecution($"INSERT INTO playerinfo (idPlayer, coinCount) VALUES (1, {CoinCount})");
            moneyText.text = Convert.ToString(CoinCount);
            BuyHeroes.heroes.Add(_startHero);
        }
        else
        {
            x = castleCoords.x;
            y = castleCoords.y;
            string coinCount = SQLscript.RequestSelectExecution("SELECT coinCount FROM playerinfo WHERE idPlayer = 1");
            moneyText.text = coinCount;
            CharactersSpawn(characters);
            GameObject lastHero = BuyHeroes.heroes[BuyHeroes.heroes.Count - 1];
            Camera.main.transform.parent = lastHero.transform;
            Camera.main.transform.position = new Vector3(lastHero.transform.position.x, lastHero.transform.position.y, Camera.main.transform.position.z);
            if (_startHero != null)
                Destroy(_startHero);
        }
        CastleSpawn(x, y);
        globals.isGenerateMap = true;
    }

    void CastleSpawn(int x, int y)
    {
        _tilemap.SetTile(new Vector3Int(x, y, 0), _castle);

        _tilemap.SetTile(new Vector3Int(x + 1, y, 0), _forest);
        _tilemap.SetTile(new Vector3Int(x - 1, y, 0), _forest);
        _tilemap.SetTile(new Vector3Int(x, y + 1, 0), _forest);
        _tilemap.SetTile(new Vector3Int(x, y - 1, 0), _forest);
        _tilemap.SetTile(new Vector3Int(x - 1, y + 1, 0), _forest);
        _tilemap.SetTile(new Vector3Int(x - 1, y - 1, 0), _forest);
        _tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), _forest);
        _tilemap.SetTile(new Vector3Int(x + 1, y - 1, 0), _forest);

        _tilemapCol.SetTile(new Vector3Int(x, y, 0), _hexagone);
        _tilemapCol.SetTile(new Vector3Int(x + 1, y, 0), _hexagone);
        _tilemapCol.SetTile(new Vector3Int(x - 1, y, 0), _hexagone);
        _tilemapCol.SetTile(new Vector3Int(x, y + 1, 0), _hexagone);
        _tilemapCol.SetTile(new Vector3Int(x, y - 1, 0), _hexagone);
        _tilemapCol.SetTile(new Vector3Int(x - 1, y + 1, 0), _hexagone);
        _tilemapCol.SetTile(new Vector3Int(x - 1, y - 1, 0), _hexagone);
        _tilemapCol.SetTile(new Vector3Int(x + 1, y + 1, 0), _hexagone);
        _tilemapCol.SetTile(new Vector3Int(x + 1, y - 1, 0), _hexagone);

        if (!globals.isGenerateMap)
        {
            heroSpawnPos = _tilemap.CellToWorld(new Vector3Int(x, y - 1, 0));
            _startHero.transform.position = heroSpawnPos;
        }
    }

    void EnemySpawnOnLaunch(int xCastle, int yCastle)
    {
        int x, y;

        if (EnemyCount > 100) EnemyCount = 100;

        for (int i = 0; i < EnemyCount; i++)
        {
            x = UnityEngine.Random.Range(1, 299);
            y = UnityEngine.Random.Range(1, 299);

            if (!_coords.Contains(new Vector3Int(x, y, 0)) &&
            ((System.Math.Abs(xCastle - x) > 8) && System.Math.Abs(yCastle - y) > 8))
            {
                _coords.Add(new Vector3Int(x, y, 0));
            }
            else i--;
        }

        for (int i = 0; i < _coords.Count; ++i)
        {
            enemies.Add(Instantiate(_enemyArr[UnityEngine.Random.Range(0, _enemyArr.Length)], _tilemap.CellToWorld(_coords[i]), Quaternion.identity));
        }
    }

    public void CharactersSpawn(List<Character> heroes)
    {
        BuyHeroes.heroes.Clear();
        enemies.Clear();
        foreach (Character h in heroes)
        {
            switch (h.Name)
            {
                case "hero1":
                    BuyHeroes.heroes.Add(Instantiate(_heroesArr[0], h.Position, Quaternion.identity));
                    break;

                case "hero2":
                    BuyHeroes.heroes.Add(Instantiate(_heroesArr[1], h.Position, Quaternion.identity));
                    break;

                case "hero3":
                    BuyHeroes.heroes.Add(Instantiate(_heroesArr[2], h.Position, Quaternion.identity));
                    break;

                case "hero4":
                    BuyHeroes.heroes.Add(Instantiate(_heroesArr[3], h.Position, Quaternion.identity));
                    break;

                case "hero5":
                    BuyHeroes.heroes.Add(Instantiate(_heroesArr[4], h.Position, Quaternion.identity));
                    break;

                case "hero6":
                    BuyHeroes.heroes.Add(Instantiate(_heroesArr[5], h.Position, Quaternion.identity));
                    break;

                case "hero7":
                    BuyHeroes.heroes.Add(Instantiate(_heroesArr[6], h.Position, Quaternion.identity));
                    break;

                case "enemy1":
                    enemies.Add(Instantiate(_enemyArr[0], h.Position, Quaternion.identity));
                    break;

                case "enemy2":
                    enemies.Add(Instantiate(_enemyArr[1], h.Position, Quaternion.identity));
                    break;

                case "enemy3":
                    enemies.Add(Instantiate(_enemyArr[2], h.Position, Quaternion.identity));
                    break;

                case "enemy4":
                    enemies.Add(Instantiate(_enemyArr[3], h.Position, Quaternion.identity));
                    break;

                case "enemy5":
                    enemies.Add(Instantiate(_enemyArr[4], h.Position, Quaternion.identity));
                    break;

                case "enemy6":
                    enemies.Add(Instantiate(_enemyArr[5], h.Position, Quaternion.identity));
                    break;

                case "enemy7":
                    enemies.Add(Instantiate(_enemyArr[6], h.Position, Quaternion.identity));
                    break;
            }
        }
    }
}