using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerCreator;
using System;


[Serializable]
public class LayerCubes
{   
    [SerializeField]private Cube[] Cubes;
    [SerializeField]private int layerNum;
    [SerializeField]private ColorCube colorLayer;
    private Tower tower;

    public LayerCubes(int colorCube,int layerNumber) 
    {
        colorLayer = NewColor(colorCube);
        layerNum = layerNumber;
        Cubes = new Cube[8];
        tower = GameObject.FindObjectOfType<Tower>();

    }
     

    public void AddCube(GameObject cube) 
    {       
        for (int i = 0; i < 8; i++)
        {
            if (Cubes[i] == null)
            {
                Cubes[i] = cube.GetComponent<Cube>();
                if (i == Cubes.Length - 1)
                {
                    Debug.Log("Слой полон");
                    CompareCubesByColor();
                }
                break;
            }          
        }             
    }

    private void CompareCubesByColor()
    {
        int countColorFail = 0;
        for (int i = 0; i < Cubes.Length; i++)
        {         
            if (Cubes[i].GetComponent<Cube>().GetColorCube() !=(int)_colorLayer)
            {
                countColorFail++;
            }
        }

        if (countColorFail > 0)
        {
            Debug.Log("Цвета не совпадают");
            DestroyLayer();
        }

    }

    public ColorCube NewColor(int col) 
    {
        switch (col)
        {
            case 0:
                return ColorCube.Red;
            case 1:
                return ColorCube.Yellow;
            case 2:
                return ColorCube.Blue;
            case 3:
                return ColorCube.Green;

            default:
                throw new Exception("No color");
        }    
  
    }
    /// <summary>
    /// Сдвигаем СлоЙ
    /// </summary>
    public void ShiftLayer() 
    {
        layerNum--;
        foreach (var item in Cubes)
        {
            if (item != null)
            {
                item.SwitchFall();
            }
            else
                continue;            
        }       
    }

    public ColorCube GetColor() 
    {
        return colorLayer;        
    }


    private void DestroyLayer() 
    {
        Debug.Log("Уничтожить слой");
        
        for (int i = 0; i < Cubes.Length; i++)
        {   //уничтожаем кубы
            Cubes[i].Destruction(true);
        }
        //Сдвигаем слоИ
        tower.ShiftLaers(_layerNum);
        //уменьшаем макс.высоту башни
        Tower.highestColumn--;        
    }
    

    public void SetColor(int col) 
    {
     colorLayer =  NewColor(col);

    }

    public int GetNumber() 
    {
        return layerNum;
    }

  
}
