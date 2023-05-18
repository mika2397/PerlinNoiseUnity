using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int width = 256;
    public int height = 256;
    public float scale = 20f;

    public float xRandom;
    public float yRandom;

    void Start()
    {
        xRandom = Random.Range(0f,1000f);
        yRandom = Random.Range(0f,1000f);

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
        Texture2D texture = new Texture2D(256,256);
        float[,] heightVals = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                texture.SetPixel(x,y,CalculateColorValue(x,y));
            }
        }
        texture.Apply();
        //New lines in the GetPerlinHeights method
        SaveTextureAsPNG(texture,"Assets/GeneratedNoise/PerlinGenerated2.png");
        GetComponent<Renderer>().material.mainTexture = texture;
        return heightVals;
    }

    Color CalculateColorValue(float x, float y) 
    {
        float xVal = (float) x/width*scale + xRandom;
        float yVal = (float) y/height*scale + yRandom; 
        float colorToReturn = Mathf.PerlinNoise(xVal,yVal);
        return new Color(colorToReturn,colorToReturn,colorToReturn);
    }

    public static void SaveTextureAsPNG(Texture2D textureInput, string filePath) 
    {
        byte[] bytes = textureInput.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath,bytes);
    }

}
