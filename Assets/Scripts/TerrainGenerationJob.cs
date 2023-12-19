using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct TerrainGenerationJob : IJobParallelFor
{
    public int width;
    public int height;
    public float scale;
    public float maxHeight;
    public float holeRadius;
    public Vector2 holePosition;

    public NativeArray<float> heightMap;

    public void Execute(int index)
    {
        int x = index % width;
        int y = index / width;

        float xCoord = (float)x / width * scale + holePosition.x;
        float yCoord = (float)y / height * scale + holePosition.y;

        float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);
        float heightValue = perlinValue * maxHeight;

        // Agujeros: Reducir la altura alrededor del centro del agujero
        float distanceToHole = Vector2.Distance(new Vector2(x, y), holePosition);
        float holeFactor = Mathf.Clamp01(1 - distanceToHole / holeRadius);
        heightValue *= holeFactor;

        heightMap[index] = heightValue;
    }
}
