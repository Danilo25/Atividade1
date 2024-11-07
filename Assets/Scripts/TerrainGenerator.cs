using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int profundidade = 20;

    public int largura = 256;
    public int altura = 256;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public float scale = 20f;
    // Start is called before the first frame update
    private void Start()
    {
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);
    }
    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        offsetX += Time.deltaTime * 1f;
    }

    TerrainData GenerateTerrain( TerrainData terrainData)
    {
        terrainData.heightmapResolution =largura +1; 
        terrainData.size = new Vector3(largura, profundidade, altura);

        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[largura, altura];
        for(int x = 0; x< largura; x++)
        {
            for (int y = 0; y< altura; y++)
            {
                heights[x, y] = CalculateHeights(x, y);
            }
        }
        return heights;
    }

    float CalculateHeights(int x, int y)
    {
        float xCoord = (float)x/largura * scale + offsetX;
        float yCoord = (float)y /altura * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
