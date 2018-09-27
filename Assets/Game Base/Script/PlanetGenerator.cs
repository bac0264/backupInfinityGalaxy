using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearPlanet
{
    public GameObject Planet;
    public float PercentAppear;
}
public class TargetPlanet
{
    public GameObject Planet;
    public int NumOfTarget;
}
public enum GameLimit{
    Move,
    Time
}
public class PlanetGenerator : SerializedMonoBehaviour
{
    //public GameObject PlanetPrefab;
    LineRenderer Line;
    [BoxGroup("Game Limit")]
    public GameLimit gameLimit;
    [BoxGroup("Game Limit")]
    public int ValueLimit;
    [BoxGroup("Score Star")]
    public int oneStar, twoStar, threeStar;
    public List<AppearPlanet> appearPlanet;
    public List<TargetPlanet> targetPlanet;

    [TableMatrix(SquareCells = true)]
    public GameObject[,] matrixIns = new GameObject[6, 7];
    GameObject[,] matrix = new GameObject[6, 7];
    List<GameObject> planetConnected=new List<GameObject>();
    Vector3 saveLoopPos=new Vector3(-1,-1,-1);
    bool isFalling = false;
    bool touching = false;
    // Use this for initialization
    void Start () {
        Line = GetComponentInChildren<LineRenderer>();
        Camera.main.orthographicSize = ((6 + 1) * 1.0f / Screen.width * Screen.height) / 2;
        Camera.main.transform.position = new Vector3(2.5f,3.5f, -10f);
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (matrixIns[i, j] != null)
                {
                    if (matrixIns[i, j].GetComponent<SpriteRenderer>().sprite == null)
                    {
                        /*matrix[i, j].GetComponentsInChildren<SpriteRenderer>()[0].sprite = temp;
                        matrix[i, j].GetComponentsInChildren<SpriteRenderer>()[1].sprite = temp;*/
                        matrix[i, 6 - j]= Instantiate(rndPlanet(), new Vector3(i, 6 - j, 0), Quaternion.identity, transform);
                    }
                    else
                    {
                        matrix[i, 6-j]=Instantiate(matrixIns[i, j], new Vector3(i, 6-j, 0), Quaternion.identity, transform);
                    }
                }
            }
        }
	}
    GameObject rndPlanet()
    {
        return appearPlanet[UnityEngine.Random.Range(0, appearPlanet.Count)].Planet;
        //return null;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0) && !touching)
        {
            touching = true;
            touchBegin(Input.mousePosition);
        }
        if (touching)
        {
            touchMove(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            touching = false;
            touchEnd(Input.mousePosition);
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touching = true;
                touchBegin(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchMove(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touching = false;
                touchEnd(Input.GetTouch(0).position);
            }
        }
    }
    private void touchBegin(Vector3 mousePosition)
    {

    }
    private void touchMove(Vector3 mousePosition)
    {
        GameObject curObj = ObjectClicked(mousePosition);
        if (curObj != null)
        {
            if (planetConnected.Count == 0)
            {
                //cham vien dau tien
                planetConnected.Add(curObj);
                curObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                curObj.GetComponent<Animator>().SetTrigger("Touched");
                //Dat mau line la mau diem chinh giua cua planet
                Color lineColor = curObj.GetComponent<PlanetElement>().GetPlanetColor();
                Line.SetColors(lineColor, lineColor);
            }
            else
            if (Vector3.Distance(curObj.transform.position, planetConnected[planetConnected.Count - 1].transform.position)-1f < 0.1f
                         && curObj.GetComponent<SpriteRenderer>().sprite == planetConnected[0].GetComponent<SpriteRenderer>().sprite)
            {
                //vien thu 2 tro di và chạm 4 viên xung quanh
                int idx = planetConnected.IndexOf(curObj);
                if (idx == -1)
                {
                    //khong thuoc cac vien da noi
                    planetConnected.Add(curObj);
                    curObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    curObj.GetComponent<Animator>().SetTrigger("Touched");
                }
                else
                {
                    //thuoc cac vien da noi
                    if (planetConnected.Count >= 2)
                    {
                        if (curObj == planetConnected[planetConnected.Count - 2])
                        {
                            //cham vao vien truoc
                            planetConnected[planetConnected.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = 0;
                            planetConnected.RemoveAt(planetConnected.Count - 1);
                        }
                        else if(saveLoopPos == new Vector3(-1, -1, -1) && curObj != planetConnected[planetConnected.Count - 1])
                        {
                            /*//noi thanh vong
                            saveLoopPos = curObj.transform.position;
                            for (int i = 0; i < 6; i++)
                            {
                                for (int j = 0; j < 6; j++)
                                {
                                    if(matrix[i, j].GetComponent<SpriteRenderer>().sprite == planetConnected[0].GetComponent<SpriteRenderer>().sprite)
                                        matrix[i,j].GetComponent<Animator>().SetTrigger("Touched");
                                }
                            }*/
                        }
                    }
                }
            }
            if(curObj == planetConnected[planetConnected.Count - 1]&& saveLoopPos!= new Vector3(-1, -1, -1))
            {
                saveLoopPos = new Vector3(-1, -1, -1);
            }
        }
        
        //DrawLine
        Line.positionCount = planetConnected.Count + 1;
        for (int i = 0; i < planetConnected.Count; i++)
        {
            Vector3 tempPos = planetConnected[i].transform.position;
            tempPos.z = -0.01f;
            Line.SetPosition(i, tempPos);
        }

        Vector3 tempPos2;
        if (saveLoopPos == new Vector3(-1, -1, -1))
        {
            tempPos2 = Camera.main.ScreenToWorldPoint(mousePosition);
        }
        else
        {
            //isLoop
            tempPos2 = saveLoopPos;
        }
        tempPos2.z = -0.01f;
        Line.SetPosition(Line.positionCount - 1, tempPos2);
    }
    private void touchEnd(Vector3 mousePosition)
    {
        //check Loop
        if(saveLoopPos != new Vector3(-1, -1, -1))
        {
            Sprite temp = planetConnected[0].GetComponent<SpriteRenderer>().sprite;
            planetConnected.Clear();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (matrix[i, j].GetComponent<SpriteRenderer>().sprite == temp)
                        planetConnected.Add(matrix[i,j]);
                }
            }
        }

        if (planetConnected.Count >= 2)
        {
            //AnDiem
            for (int i = 0; i < 6; i++)
            {
                //Xet tung cot
                int dropStep = 0;
                for (int j = 0; j < 7; j++)
                {
                    if (dropStep != 0)
                    {
                        matrix[i, j - dropStep] = matrix[i, j];
                        StartCoroutine(PlanetFalling(matrix[i, j - dropStep].transform, j - dropStep));
                    }
                    if (planetConnected.IndexOf(matrix[i, j]) > -1)
                    {
                        //thuoc day da noi
                        dropStep++;
                    }
                }
                //tao them 
                for (int k = dropStep; k > 0; k--)
                {
                    matrix[i, 7 - k] = Instantiate(rndPlanet(), new Vector3(i, 7 + dropStep - k, 0), Quaternion.identity, transform);
                    //matrix[i, 6 - k].transform.DOMoveY(6 - k, 0.5f).SetEase(Ease.InCubic);
                    StartCoroutine(PlanetFalling(matrix[i, 7 - k].transform, 7 - k));
                    matrix[i, 7 - k].transform.DOScale(0, 0.2f).SetEase(Ease.OutCirc).From();
                }
            }
            StartCoroutine(Explosion());
            StartCoroutine(affterGetScore());

        }
        else if(planetConnected.Count==1)
        {
            planetConnected[0].GetComponent<SpriteRenderer>().sortingOrder = 1;
            planetConnected.Clear();
        }
        Line.positionCount = 0;
    }
    IEnumerator Explosion()
    {
        for (int i=0;i<planetConnected.Count;i++)
        {
            planetConnected[i].GetComponent<PlanetElement>().Explosion();
            if(planetConnected.Count-i<=6)
                yield return new WaitForSeconds(0.08f);
        }
        planetConnected.Clear();
    }
    IEnumerator PlanetFalling(Transform transform,float endValue)
    {
       
        yield return new WaitForSeconds(0.11f);
        float v = 0;
        do
        {   
            yield return new WaitForEndOfFrame();
            if (transform != null)
            {
                v -= 0.015f;
                Vector3 vector = transform.position;
                vector.y += v;
                if (vector.y < endValue)
                {
                    vector.y = endValue;
                    transform.position = vector;
                    break;
                }
                transform.position = vector;
            }
            else
            {
                break;
            }
           
        } while (true);
        if (transform != null)
        {
            transform.DOMoveY(transform.position.y + 0.15f, 0.05f).SetEase(Ease.OutSine);
            transform.DOMoveY(transform.position.y, 0.05f).SetDelay(0.05f).SetEase(Ease.InSine);
        }
       
    }
    IEnumerator affterGetScore()
    {
        yield return new WaitForSeconds(5f);
        if (!canConnect())
        {
         SwapPlanet();
        }
    }
    public void SwapPlanet()
    {
        List<GameObject> allObj = new List<GameObject>();
        foreach (var item in matrix)
        {
            allObj.Add(item);
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                matrix[i, j] = allObj[UnityEngine.Random.Range(0, allObj.Count)];
                allObj.Remove(matrix[i, j]);
            }
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                matrix[i, j].transform.DOMove(new Vector3(i, j, 0), 1.5f).SetEase(Ease.OutBack); 
            }
        }
    }
    bool canConnect()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (getObj(i, j + 1) != null)
                {
                    if (getObj(i, j).GetComponent<SpriteRenderer>().sprite == getObj(i, j + 1).GetComponent<SpriteRenderer>().sprite)
                    {
                        return true;
                    }
                }
                if (getObj(i + 1, j) != null)
                {
                    if (getObj(i, j).GetComponent<SpriteRenderer>().sprite == getObj(i + 1, j).GetComponent<SpriteRenderer>().sprite)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    GameObject getObj(int x, int y)
    {
        if (x < 0 || x >= 6 || y < 0 || y >= 7)
            return null;
        return matrix[x, y];
    }
    GameObject ObjectClicked(Vector2 screenPosition)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 rayPos = new Vector2(worldPos.x, worldPos.y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        if (hit)
        {
            return hit.transform.gameObject;
        }
        else return null;
    }
}
