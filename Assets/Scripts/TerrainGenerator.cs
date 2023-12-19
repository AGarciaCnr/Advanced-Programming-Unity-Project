using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;
    public float maxHeight = 10f;
    public float holeRadius = 30f;
    public Vector2 holePosition = new Vector2(128, 128); // Puedes ajustar la posición del agujero según sea necesario

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        NativeArray<float> heightMap = new NativeArray<float>(width * height, Allocator.TempJob);

        TerrainGenerationJob job = new TerrainGenerationJob
        {
            width = width,
            height = height,
            scale = scale,
            maxHeight = maxHeight,
            holeRadius = holeRadius,
            holePosition = holePosition,
            heightMap = heightMap
        };

        JobHandle jobHandle = job.Schedule(height * width, 64);
        jobHandle.Complete();

        // Aplicar la generación de terreno en el objeto Terrain o malla según sea necesario
        // Ejemplo: Aplicar la altura a un objeto Terrain
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData.SetHeights(0, 0, ConvertTo2DArray(heightMap, width, height));

        heightMap.Dispose();
    }

    float[,] ConvertTo2DArray(NativeArray<float> heightMap, int width, int height)
    {
        float[,] result = new float[width, height];
        for (int i = 0; i < heightMap.Length; i++)
        {
            int x = i % width;
            int y = i / width;
            result[x, y] = heightMap[i];
        }
        return result;
    }
}
