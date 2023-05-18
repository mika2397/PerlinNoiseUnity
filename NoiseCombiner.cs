using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseCombiner : MonoBehaviour
{
    [SerializeField]
    public Texture2D perlinPicture1;
    [SerializeField]    
    public Texture2D perlinPicture2;
    public Texture2D perlinPictureMerged;
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
        perlinPictureMerged = MergeMethod(perlinPicture1,perlinPicture2);
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
        return perlinPictureMerged.GetPixel(x,y).grayscale;
    }

    public Texture2D MergeMethod(Texture2D firstTexture, Texture2D secondTexture) {

        Texture2D resultToReturn = new Texture2D(width,height);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Color firstTextureColor = firstTexture.GetPixel(x,y);
                Color secondTextureColor = secondTexture.GetPixel(x,y);
                Color MergeResult = (firstTextureColor + secondTextureColor) / 2;
                resultToReturn.SetPixel(x,y,MergeResult);
            }
        }

        resultToReturn.Apply();
        return resultToReturn;
    }

}
