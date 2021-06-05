using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlockMove : MonoBehaviour/*, IBeginDragHandler, IDragHandler*/
{ 
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.9f;
    public static int height = 27;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
   

    private int touchSensitivityHorizontal = 26;
    private int touchSensitivityVertical = 10;

    Vector2 previousUnitPosition = Vector2.zero;
    Vector2 direction = Vector2.zero;
    bool moved = false;


    void Start()
    {
        StartCoroutine(Accelerate());
    }

    
    void Update()
    {
        KeyboardControls();
        TouchControls();
    }

    void KeyboardControls()
    {
        if (Time.deltaTime > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(-1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!ValidMove())
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                }
            }

            if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0, -1, 0);
                previousTime = Time.time;
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    AddToGrid();
                    CheckForLines();
                    this.enabled = false;
                    FindObjectOfType<Spawner>().NewQube();
                }
            }

        }
    }

    void TouchControls()
    {
        if (Time.deltaTime > 0)
        { 
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);

                if (t.phase == TouchPhase.Began)
                {
                    previousUnitPosition = new Vector2(t.position.x, t.position.y);

                }
                else if (t.phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = t.deltaPosition;
                    direction = touchDeltaPosition.normalized;
                    if (Mathf.Abs(t.position.x - previousUnitPosition.x) >= touchSensitivityHorizontal && direction.x < 0 && t.deltaPosition.y > -10 && t.deltaPosition.y < 10)
                    {
                        //Left
                        transform.position += new Vector3(-1, 0, 0);
                        if (!ValidMove())
                        {
                            transform.position -= new Vector3(-1, 0, 0);
                        }
                        previousUnitPosition = t.position;
                        moved = true;

                    }
                    else if (Mathf.Abs(t.position.x - previousUnitPosition.x) >= touchSensitivityHorizontal && direction.x > 0 && t.deltaPosition.y > -10 && t.deltaPosition.y < 10)
                    {
                        //Right
                        transform.position += new Vector3(1, 0, 0);
                        if (!ValidMove())
                        {
                            transform.position -= new Vector3(1, 0, 0);
                        }
                        previousUnitPosition = t.position;
                        moved = true;
                    }
                    else if (Mathf.Abs(t.position.y - previousUnitPosition.y) >= touchSensitivityVertical && direction.y < 0 && t.deltaPosition.x > -10 && t.deltaPosition.x < 10)
                    {
                        //Down
                        transform.position += new Vector3(0, -1, 0);
                        previousTime = Time.time;
                        if (!ValidMove())
                        {
                            transform.position -= new Vector3(0, -1, 0);
                            AddToGrid();
                            CheckForLines();
                            this.enabled = false;
                            FindObjectOfType<Spawner>().NewQube();
                        }
                        previousUnitPosition = t.position;
                        moved = true;
                    }
                }



                else if (t.phase == TouchPhase.Ended)
                {
                    if (!moved && t.position.x > Screen.width / 4)
                    {
                        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                        if (!ValidMove())
                        {
                            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                        }
                    }
                    moved = false;

                }

            }
        } 
    }

    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HaveLine(i))
            {
                DeleteLine(i);
                
                MoveLines(i);
                Accelerate();
                //Falling(i);
                HighScore.instance.Score();
                
               
            }
            
        }
        
    }

    private IEnumerator Accelerate ()
    {while (true)
        {
            fallTime = fallTime - 0.3f;
            yield return new WaitForSeconds(10f);
            Debug.Log(fallTime);
        }

    }

    //public void Falling(int i)
    //{
    //    if (HighScore.score == 80)
    //    {
    //        fallTime -= 0.4f;

    //    }
    //}

    bool HaveLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
                
            }

            

        }
        return true;
    }


    private static void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
        
    }

    private static void MoveLines(int y)
    {
        for (int i = y; i < height - 1; i++) // The array goes out of bounds if you don't set -1,
                                                 // since you check for the grid above in the second for loop
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, i + 1] != null) // In the tutors code, the code only checks for the row above, now it checks every row
                {
                    grid[x, i] = grid[x, i + 1];
                    grid[x, i].gameObject.transform.position -= new Vector3(0, 1, 0);
                    grid[x, i + 1] = null;
                }
            }
        }
    }
    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
            
        }
        CheckEndGame();
    }

    void CheckEndGame()
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j,height-1] != null)
            {
                GameOver();
            }
        }
    }

    public static void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {

                return false;
            }

        }
        return true;
        
    }

   
}
