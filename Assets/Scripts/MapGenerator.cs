using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int _arrayWidth;
    [SerializeField] private int _arrayHeight;
    [SerializeField] private Tilemap _tilemapCol;
    [SerializeField] private Tilemap _tilemapTerr;

    [SerializeField] private TileBase _hexTile;

    [SerializeField] private TileBase[] _forestArray = new TileBase[9];
    [SerializeField] private TileBase[] _snowArray = new TileBase[9];
    [SerializeField] private TileBase[] _desertArray = new TileBase[9];
    [SerializeField] private TileBase[] _hellArray = new TileBase[9];

    [HideInInspector] public static int[,] map;
    private TileBase[] _newArray;

    void Start()
    {
        if (!globals.isGenerateMap)
        {
            map = GenerateArray(_arrayWidth, _arrayHeight);

            RenderMap(map);
            RenderHexMap(map);
        }
        else
        {
            RenderMap(map);
            RenderHexMap(map);
        }
    }
    

    public static int[,] GenerateArray(int width, int height)
    {
        int[,] map = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = System.Convert.ToInt32(Random.Range(0f, 20f));
            }
        }
        return map;
    }


    public void RenderMap(int[,] map)
    {
        _tilemapTerr.ClearAllTiles();

        for (int x = 0; x < _arrayWidth; x++)
        {
            for (int y = 0; y < _arrayHeight; y++)
            {
                if (x < 150 && y < 150)
                    _newArray = _forestArray;

                else if (x >= 150 && y < 150)
                    _newArray = _snowArray;

                else if (x < 150 && y >= 150)
                    _newArray = _desertArray;

                else
                    _newArray = _hellArray;


                if (map[x, y] > 8)
                {
                    _tilemapTerr.SetTile(new Vector3Int(x, y, 0), _newArray[0]);
                }

                else if (map[x, y] <= 1)
                {
                    _tilemapTerr.SetTile(new Vector3Int(x, y, 0), _newArray[1]);
                }

                else if (map[x, y] == 2)
                {
                    _tilemapTerr.SetTile(new Vector3Int(x, y, 0), _newArray[2]);
                }

                else if (map[x, y] == 3)
                {
                    _tilemapTerr.SetTile(new Vector3Int(x, y, 0), _newArray[3]);
                }

                else if (map[x, y] == 4)
                {
                    _tilemapTerr.SetTile(new Vector3Int(x, y, 0), _newArray[4]);
                }

                else if (map[x, y] == 5)
                {
                    _tilemapTerr.SetTile(new Vector3Int(x, y, 0), _newArray[5]);
                }

                else if (map[x, y] == 6)
                {
                    _tilemapTerr.SetTile(new Vector3Int(x, y, 0), _newArray[6]);
                }

                else if (map[x, y] == 7)
                {
                    _tilemapTerr.SetTile(new Vector3Int(x, y, 0), _newArray[7]);
                }

                else if (map[x, y] == 8)
                {
                    _tilemapTerr.SetTile(new Vector3Int(x, y, 0), _newArray[8]);
                }
            }
        }
    }

    public void RenderHexMap(int[,] map)
    {
        _tilemapCol.ClearAllTiles();

        for (int x = 0; x < _arrayWidth; x++)
        {
            for (int y = 0; y < _arrayHeight; y++)
            {
                if (map[x, y] > 1)
                {
                    _tilemapCol.SetTile(new Vector3Int(x, y, 0), _hexTile);
                }
            }
        }
    }
}