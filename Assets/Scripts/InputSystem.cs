using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TowerCreator;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private Text debugTextFieldVector;
    [SerializeField] private Text debug2;
    [SerializeField] private CameraControl cameraControl;
    [SerializeField] private bool isMoveCube = false;
    [SerializeField] RotateArea numberRotateArea;
    private GameObject curCube= null;

    private Vector2 firstTouchPos;
    private Vector2 lastTouchPos;
    private float _dragDistance;
    private float timeTap;

    private void Start()
    {
        dragDistance = Screen.width * 15 / 100;
        Application.targetFrameRate = 60;
        timeTap = 0;
       
    }


    private void Update()
    {
        
#if UNITY_EDITOR
        SwipeEmulatorMouse();
#else
        SwipeForum();
#endif


    }

    private void SwipeForum()
    {
        CalculateRotationArea(cameraControl.transform.rotation.eulerAngles.y);
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch

            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                firstTouchPos = touch.position;
                lastTouchPos = touch.position;
                Ray ray = Camera.main.ScreenPointToRay(firstTouchPos);
                RaycastHit hit;

                Physics.Raycast(ray, out hit, 100f);

                if (hit.collider.tag == "Cube")
                {
                    isMoveCube = true;
                    curCube = hit.collider.gameObject;
                }
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lastTouchPos = touch.position;  //last touch position. Ommitted if you use list

                if (!isMoveCube)                
                    cameraControl.RotateCamera(lastTouchPos.x - firstTouchPos.x);                
               
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > dragDistance || Mathf.Abs(lastTouchPos.y - firstTouchPos.y) > _dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal                  

                    if (Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > Mathf.Abs(lastTouchPos.y - firstTouchPos.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lastTouchPos.x > firstTouchPos.x))  //If the movement was to the right)
                        {   //Right swipe
                            if (isMoveCube)
                            {
                                Vector2 directionMove = CalculateDirectionMove(Vector2.right);
                                curCube.GetComponent<IMover>().MoveCube(directionMove);
                                isMoveCube = false;
                                debug2.text = "swipe right+ debugSwipe";
                            }
                          
                        }
                        else
                        {   //Left swipe
                            if (isMoveCube)
                            {
                                Vector2 directionMove = CalculateDirectionMove(Vector2.left);
                                curCube.GetComponent<IMover>().MoveCube(directionMove);
                                isMoveCube = false;
                                debug2.text = "swipe left+ debugSwipe";
                            }                          
                            
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lastTouchPos.y > firstTouchPos.y)  //If the movement was up
                        {   //Up swipe
                            if (isMoveCube)
                            {
                                Vector2 directionMove = CalculateDirectionMove(Vector2.up);
                                curCube.GetComponent<IMover>().MoveCube(directionMove);
                                isMoveCube = false;
                                debug2.text = "Up Swipe+ debugSwipe";
                            }
                           
                        }
                        else
                        {   //Down swipe
                            if (isMoveCube)
                            {
                                Vector2 directionMove = CalculateDirectionMove(Vector2.down);
                                curCube.GetComponent<IMover>().MoveCube(directionMove);
                                isMoveCube = false;
                                debug2.text = "Down Swipe + debugSwipe";
                            }
                           
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    debug2.text ="Tap ";
                
                    if (Time.time - timeTap < 0.4f)
                    {
                        debug2.text = "Double TAp";
                        if (curCube != null)
                        {
                            curCube.GetComponent<IDestruction>().Destruction();
                            curCube = null;
                        }
                    }
                    timeTap = Time.time;
                }





            }

        }

      
         
        

    }

    private void SwipeEmulatorMouse() 
    {      
        CalculateRotationArea(cameraControl.transform.rotation.eulerAngles.y);

        if (Input.GetMouseButtonDown(0))
        {
            firstTouchPos = Input.mousePosition;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(firstTouchPos);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Cube")
                {
                    isMoveCube = true;
                    curCube = hit.collider.gameObject;
                }else
                {
                    isMoveCube = false;
                    curCube = null;
                }
            }

            lastTouchPos = Input.mousePosition;          
            
        }else
            if (Input.GetMouseButtonUp(0))
            {

            lastTouchPos = Input.mousePosition;

            if (Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > _dragDistance || Mathf.Abs(lastTouchPos.y - firstTouchPos.y) > _dragDistance)
            {
                //It's a drag 

                if (!isMoveCube)               
                    cameraControl.RotateCamera((lastTouchPos.x - firstTouchPos.x));
                

                //check if the drag is vertical or horizontal
                if (Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > Mathf.Abs(lastTouchPos.y - firstTouchPos.y))
                {   //If the horizontal movement is greater than the vertical movement...
                    if ((lastTouchPos.x > firstTouchPos.x))  //If the movement was to the right)
                    {   //Right swipe
                        
                        debug2.text = "swipe right";
                        if (isMoveCube)
                        {                          
                            Vector2 directionMove = CalculateDirectionMove(Vector2.right);
                            curCube.GetComponent<IMover>().MoveCube(directionMove);
                            isMoveCube = false;                        
                        }                       
                    }
                    else
                    {   //Left swipe
                        if (isMoveCube)
                        {
                            Vector2 directionMove = CalculateDirectionMove(Vector2.left);
                            curCube.GetComponent<IMover>().MoveCube(directionMove);
                            isMoveCube = false;
                        }                       
                        debug2.text = "swipe left";                  
                    }
                }
                else
                {   //the vertical movement is greater than the horizontal movement
                    if (lastTouchPos.y > firstTouchPos.y)  //If the movement was up
                    {   //Up swipe

                        if (isMoveCube)
                        {
                            Vector2 directionMove = CalculateDirectionMove(Vector2.up);
                            curCube.GetComponent<IMover>().MoveCube(directionMove);
                            isMoveCube = false;
                        }
                        debug2.text = "Up Swipe";
                    }
                    else
                    {   //Down swipe
                        if (isMoveCube)
                        {
                            Vector2 directionMove = CalculateDirectionMove(Vector2.down);
                            curCube.GetComponent<IMover>().MoveCube(directionMove);
                            isMoveCube = false;
                        }
                        debug2.text = "Down Swipe";
                    }
                }
            }
            else
            {   //It's a tap as the drag distance is less than 20% of the screen height
                           
                if (Time.time - timeTap < 0.3f)
                {                 
                    if (curCube!=null)
                    {
                        curCube.GetComponent<IDestruction>().Destruction();
                        curCube = null;
                    }
                }
                timeTap = Time.time;

            }
        }

    }

    private Vector2 CalculateDirectionMove(Vector2 directionSwipe) 
    {
        switch (_numberRotateArea)
        {
            case RotateArea.I:
                if (directionSwipe == Vector2.left)
                        return new Vector2(-1f, 0f);
                else
                    if (directionSwipe == Vector2.right)
                        return new Vector2(1f, 0f);
                else
                    if (directionSwipe == Vector2.up)
                        return new Vector2(0, 1f);
                else
                        return new Vector2(0,-1f);
                
            case RotateArea.II:

                if (directionSwipe == Vector2.left)
                    return new Vector2(0f, 1f);
                else
                 if (directionSwipe == Vector2.right)
                    return new Vector2(1f, 0f);
                else
                 if (directionSwipe == Vector2.up)
                    return new Vector2(1f, 1f);
                else
                    return new Vector2(-1f, -1f);
                                              
            case RotateArea.III:

                if (directionSwipe == Vector2.left)
                    return new Vector2(0f, 1f);
                else
                if (directionSwipe == Vector2.right)
                    return new Vector2(0f, -1f);
                else
                if (directionSwipe == Vector2.up)
                    return new Vector2(1f, 0f);
                else
                    return new Vector2(-1f, 0f);

            case RotateArea.IV:
                if (directionSwipe == Vector2.left)
                    return new Vector2(1f, 0f);
                else
                if (directionSwipe == Vector2.right)
                    return new Vector2(0f, -1f);
                else
                if (directionSwipe == Vector2.up)
                    return new Vector2(1f, -1f);
                else
                    return new Vector2(-1f, 1f);

            case RotateArea.V:
                if (directionSwipe == Vector2.left)
                    return new Vector2(1f, 0f);
                else
                  if (directionSwipe == Vector2.right)
                    return new Vector2(-1f, 0f);
                else
                  if (directionSwipe == Vector2.up)
                    return new Vector2(0f, -1f);
                else
                    return new Vector2(0f, 1f);

            case RotateArea.VI:
                if (directionSwipe == Vector2.left)
                    return new Vector2(0f, -1f);
                else
                  if (directionSwipe == Vector2.right)
                    return new Vector2(-1f, 0f);
                else
                  if (directionSwipe == Vector2.up)
                    return new Vector2(-1f, -1f);
                else
                    return new Vector2(1f, 1f);

            case RotateArea.VII:
                if (directionSwipe == Vector2.left)
                    return new Vector2(0f, -1f);
                else
                   if (directionSwipe == Vector2.right)
                    return new Vector2(0f, 1f);
                else
                   if (directionSwipe == Vector2.up)
                    return new Vector2(-1f, 0f);
                else
                    return new Vector2(1f, 0f);

            case RotateArea.VIII:
                if (directionSwipe == Vector2.left)
                    return new Vector2(-1f, 0f);
                else
                 if (directionSwipe == Vector2.right)
                    return new Vector2(0f, 1f);
                else
                 if (directionSwipe == Vector2.up)
                    return new Vector2(-1f, 1f);
                else
                    return new Vector2(1f, -1f);

            default:
                return Vector2.zero;
                
        }
    }

    private float RoundRotationAngle(float angle)
    {    
        float buffer = Mathf.Abs(angle);
        int whole = (int)(buffer / 360f);

        return buffer - (whole * 360f);
    }

    private void CalculateRotationArea(float angleRotation)
    {
        float resultAngle = RoundRotationAngle(angleRotation);
      
        if (resultAngle > 0 && resultAngle < 30)  
            _numberRotateArea = RotateArea.I;       
        else
            if (resultAngle > 30 && resultAngle < 60)
        {
            _numberRotateArea = RotateArea.II;
        }
        else
            if (resultAngle > 60 && resultAngle < 120)
        {
            _numberRotateArea = RotateArea.III;
        }
        else
            if (resultAngle > 120 && resultAngle < 150)
        {
            _numberRotateArea = RotateArea.IV;
        }
        else
            if (resultAngle > 150 && resultAngle < 210)
        {
            _numberRotateArea = RotateArea.V;
        }
        else
            if (resultAngle > 210 && resultAngle < 240)
        {
            _numberRotateArea = RotateArea.VI;
        }
        else
            if (resultAngle > 240 && resultAngle < 300)
        {
            _numberRotateArea = RotateArea.VII;
        }
        else
            if (resultAngle > 300 && resultAngle < 330)
        {
            _numberRotateArea = RotateArea.VIII;
        }
        else
            _numberRotateArea = RotateArea.I;

    }



        enum RotateArea
    {
        I = 1,
        II,
        III,
        IV,
        V,
        VI,
        VII,
        VIII

    }
    }



