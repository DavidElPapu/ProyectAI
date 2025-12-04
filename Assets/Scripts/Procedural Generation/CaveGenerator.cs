using UnityEngine;
using UnityEngine.Windows;

public class CaveGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    public int width = 50;
    public int height = 50;
    [Range(0, 100)] public int fillPercent = 45;

    [Header("Cellular Automata")]
    public int smoothIteration = 3;

    [Header("Visualization")]
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public float cellSize = 1;

    private PlayerActions actions;

    private int[,] map;

    private void Start()
    {
        actions = new PlayerActions();
        actions.Gameplay.Enable();
        GenerateMap();
    }

    private void Update()
    {
        if (actions.Gameplay.Shoot.IsPressed())
        {
            GenerateMap();
        }
        if (actions.Gameplay.SKey.IsPressed())
        {
            SmoothMap();
            //gameObject.GetComponent<Connector>
            RenderMap();
        }
    }

    private void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < smoothIteration; i++)
        {
            SmoothMap();
        }
        RenderMap();
    }

    private void RandomFillMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    map[x, y] = 1;
                else
                    map[x, y] = (Random.Range(0, 100) < fillPercent) ? 1 : 0;
            }
        }
    }

    private void SmoothMap()
    {
        int[,] newMap = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighborCount = GetSurroundingWallCount(x, y);
                if (neighborCount > 4)
                    newMap[x, y] = 1;
                else if (neighborCount < 4)
                    newMap[x, y] = 0;
                else
                    newMap[x, y] = map[x, y];
            }
        }
        map = newMap;
    }

    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int nX = gridX - 1; nX <= gridX + 1; nX++)
        {
            for (int nY = gridY - 1; nY <= gridY + 1; nY++)
            {
                if (nX >= 0 && nX < width && nY >= 0 && nY < height)
                {
                    if (nX != gridX || nY != gridY)
                        wallCount += map[nX, nY];
                    else
                        wallCount++;
                }
            }
        }
        return wallCount;
    }

    private void RenderMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);
                if (map[x,y] == 1)
                    Instantiate(wallPrefab, pos, Quaternion.identity, transform);
                else
                    Instantiate(floorPrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
