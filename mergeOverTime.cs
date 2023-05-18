using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mergeOverTime : MonoBehaviour
{
    [SerializeField]
    public Texture2D perlinPicture1;
    [SerializeField]    
    public Texture2D perlinPicture2;
    public Texture2D perlinPictureMerged;
    public Terrain terrainToModify;
    [SerializeField]
    public int width = 256;
    [SerializeField]
    public int height = 256;
    public float time = 0;
    public int xOffSet = 0;
    public int yOffSet = 0;



    void Update()
    {
        terrainToModify.terrainData = InitializeTerrain(terrainToModify.terrainData);
        xOffSet += 1;
        yOffSet += 1;
        time += Time.deltaTime;
    }

  void Start()
    {
        terrainToModify = GetComponent<Terrain>();
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

    IEnumerator perlinCoroutine()
    {
        yield return new WaitForSeconds(1);
    }

    float CalculatePerlinVal(int x, int y)
    {
        return perlinPictureMerged.GetPixel(x,y).grayscale*0.4f;
    }

    public Texture2D MergeMethod(Texture2D firstTexture, Texture2D secondTexture) {

        Texture2D resultToReturn = new Texture2D(width,height);
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    Color firstTextureColor = firstTexture.GetPixel(x + xOffSet,y + yOffSet);
                    Color secondTextureColor = secondTexture.GetPixel(x + xOffSet,y + yOffSet);
                    Color MergeResult = Color.Lerp(firstTextureColor,secondTextureColor,Mathf.PingPong(Time.time, 1));
                    resultToReturn.SetPixel(x,y,MergeResult);
                }
                StartCoroutine(perlinCoroutine());
            }
        
        resultToReturn.Apply();
        return resultToReturn;
    }

}
