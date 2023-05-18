using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMatrix : MonoBehaviour
{
    [SerializeField]
    public Texture2D perlinPicture;
    public int width = 256;
    public int height = 256;

  void Start()
    {
        Terrain terrainToModify = GetComponent<Terrain>();
        terrainToModify.terrainData = InitializeTerrain(terrainToModify.terrainData);

    }

    TerrainData InitializeTerrain(TerrainData terrainToModify)
    {
        terrainToModify.SetHeights(0,0,GetPerlinHeights());
        return terrainToModify;
    }

float[,] GetPerlinHeights()
    {
        float[,] heightVals = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heightVals[x,y] = CalculatePerlinVal(x,y);
            }
        }
        return heightVals;
    }
    
    float CalculatePerlinVal(int x, int y)
    {
        return perlinPicture.GetPixel(x,y).grayscale;
    }

}
