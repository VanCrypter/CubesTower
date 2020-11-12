using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerCreator
{
    public interface IMover
    {
        void MoveCube(Vector2 dir);      
    }


   public enum ColorCube 
    {
		Red,
		Yellow,
		Blue,
		Green    
    }
}