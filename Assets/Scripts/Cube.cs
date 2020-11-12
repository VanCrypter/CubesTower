using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerCreator
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cube : MonoBehaviour, IMover,IDestruction 
    {

        [SerializeField]private Vector3 velocity;
        [SerializeField]private ColorCube color;      
        [SerializeField]private bool isFall = true;
                               
        private Rigidbody body;
        private Tower tower;

        
        private void Start()
        {
            body = GetComponent<Rigidbody>();
            tower = FindObjectOfType<Tower>();
        
        }

      
        private void FixedUpdate()
        {
            if (isFall)
            {
                body.velocity = velocity;
                OnRaycast();
            }         
        }
            

        public void OnRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.5f))
            {                    
                if (hit.transform.tag == "Cube")
                {       
                        isFall = false;
                        transform.position = Absolutaize(transform.position);
                        body.constraints = RigidbodyConstraints.FreezeAll;
                      
                        if (transform.position.y > Tower.highestColumn)
                        {
							tower.AddLayerCubes((int)color, gameObject);                      
						}else
							tower.LandedCube(gameObject) ;        

                }  
                if (hit.transform.tag == "Ground")
                {
                    isFall = false;
                    transform.position = Absolutaize(transform.position);
                    body.constraints = RigidbodyConstraints.FreezeAll;
                
                    if (transform.position.y > Tower.highestColumn)
                    {                        
                        tower.AddLayerCubes((int)color,gameObject);
                        
                    }else
                        tower.LandedCube(gameObject);
                }              
            }
        }

   

        private bool CompareColorCubes(GameObject raycastCube)
        {
            if ((int)color == raycastCube.GetComponent<Cube>().GetColorCube())
            {
                return true;
            } else
                return false;
        }
        public int GetColorCube()
        {
            return (int)color;        
        }
     
        public float CheckDistance()
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 20f);                      
            return hit.distance;
        }

        private Vector3 Absolutaize(Vector3 pos)
        {
            float yPos = pos.y;
            yPos = Mathf.Round(yPos);
           return new Vector3(pos.x, yPos, pos.z);            
        }

        public void MoveCube(Vector2 dir)
        {
            Vector3 zeroXZ = new Vector3(0f, transform.position.y, 0f);

            if (isFall && CheckDistance() > 1f )
            {
                float newPosX = transform.position.x + dir.x;
                float newPosZ = transform.position.z + dir.y;

                if (newPosX >1f )
                    newPosX = 1f;                

                if (newPosX <-1f)
                    newPosX = -1f;


                if (newPosZ > 1f)
                    newPosZ = 1f;

                if (newPosZ < -1f)
                    newPosZ = -1f;

                transform.position = new Vector3(newPosX, transform.position.y,newPosZ);

                if (transform.position == zeroXZ )
                    MoveCube(dir);
              
            }
        }

        public void Destruction()
        {
            if (isFall)
            {
                Destroy(gameObject);
            }
          
        } 
        
        public void Destruction(bool die)
        {          
                Destroy(gameObject);         
        }


        public void SwitchFall()
        {
            transform.position += Vector3.down;
        }
    }


  


}