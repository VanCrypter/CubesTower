using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerCreator;

public class Tower : MonoBehaviour
{
    //public int _colomnHighest = 0;    
    public List<LayerCubes> cubeLayers;
    public static int highestColumn;
        
    

    private void Start()
    {
        highestColumn = 0;
        cubeLayers = new List<LayerCubes>();
        
    }
    /// <summary>
    /// Добавить слой 
    /// </summary>
    public void AddLayerCubes(int col,GameObject cube) 
    {
        cubeLayers.Add(new LayerCubes(col, highestColumn));
        LandedCube(cube);

        highestColumn++;
    }

/*
    public void GenNewColor()
    {
        var rndColor = UnityEngine.Random.Range(0, 4);

        switch (rndColor)
        {
            case 0:
                SetColorTarget(ColorCube.Red);
                break;
            case 1:
                SetColorTarget(ColorCube.Yellow);
                break;
            case 2:
                SetColorTarget(ColorCube.Blue);
                break;
            case 3:
                SetColorTarget(ColorCube.Green);
                break;
            default:
                break;
        }

        ColoringIMG coloring = GameObject.Find("MAN").GetComponent<ColoringIMG>();
        coloring.SetColorImg();
        Debug.Log("Generate new color target");
    }
    */

   

    public void LandedCube(GameObject cube)
    {
        //Debug.Log("landed " +highestColumn);
        cubeLayers[(int)cube.transform.position.y - 1].AddCube(cube);
    }

    public void ShiftLaers(int numLayer) 
    {
        foreach (var item in cubeLayers)
        {
            if (item!=null && item.GetNumber() > numLayer)
            {
                item.ShiftLayer();
            }
           
        }

        cubeLayers.RemoveAt(numLayer);


    }

}
