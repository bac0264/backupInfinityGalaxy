using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour {
    public Texture2D Map;
    public GameObject circlePrefab;
    public List<GameObject> borderParts;
    float offSet;
    bool[,] checkMatrix;
    bool touching = false, animating = false,exploding=false;
    Vector2 beginPos, curPos, beginObjPos, oldVectorDrag, oldCurPos;
    Vector2 dragOrientation = Vector2.zero, vectorDrag;
    Circle slCircle;
    GameObject selectedObject = null;
    GameObject circleContainer, curPosObj;
    GameObject[,] matrix;
    List<GameObject> listTemp = new List<GameObject>();
    // Use this for initialization
    void Start() {
        //Chinh lai camera
        curPosObj = new GameObject();
        Camera.main.orthographicSize = ((Map.width + 1) * 1.0f / Screen.width * Screen.height) / 2;

        circleContainer = new GameObject("circleContainer", new System.Type[] { typeof(SpriteRenderer), typeof(SpriteMask) });
        Sprite mapSprite = Sprite.Create(Map, new Rect(0, 0, Map.width, Map.height), new Vector2(0.5f, 0.5f), 1);
        circleContainer.GetComponent<SpriteRenderer>().sprite = mapSprite;
        circleContainer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
        circleContainer.GetComponent<SpriteMask>().sprite = mapSprite;

        matrix = new GameObject[Map.width, Map.height];
        checkMatrix = new bool[Map.width, Map.height];
        for (int i = 0; i < Map.width; i++)
        {
            for (int j = 0; j < Map.height; j++)
            {
                if (Map.GetPixel(i, j).a == 1)
                {
                    matrix[i, j] = Instantiate(circlePrefab, new Vector3(i - Map.width / 2 + 0.5f, j - Map.height / 2 + 0.5f, 0), Quaternion.identity);
                    matrix[i, j].GetComponent<Circle>().Pos = new Vector2(i, j);
                }
            }
        }
        createBorder();
        if (CheckAnDiem())
        {
            AnDiem();
        }
    }
    GameObject getObj(int x, int y)
    {
        if (x < 0 || x >= Map.width || y < 0 || y >= Map.height)
            return null;
        return matrix[x, y];
    }
    GameObject getBorder(int x, int y)
    {
        GameObject res = null;
        //Goc 0
        if ((getObj(x, y) != null && getObj(x - 1, y) == null && getObj(x - 1, y - 1) == null && getObj(x, y - 1) == null)
         || (getObj(x, y) == null && getObj(x - 1, y) != null && getObj(x - 1, y - 1) != null && getObj(x, y - 1) != null))
        {
            res = Instantiate(borderParts[0]);
            res.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //Goc 90
        else if ((getObj(x, y) != null && getObj(x - 1, y) == null && getObj(x - 1, y - 1) != null && getObj(x, y - 1) != null)
         || (getObj(x, y) == null && getObj(x - 1, y) != null && getObj(x - 1, y - 1) == null && getObj(x, y - 1) == null))
        {
            res = Instantiate(borderParts[0]);
            res.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        //Goc 180
        else if ((getObj(x, y) != null && getObj(x - 1, y) != null && getObj(x - 1, y - 1) == null && getObj(x, y - 1) != null)
         || (getObj(x, y) == null && getObj(x - 1, y) == null && getObj(x - 1, y - 1) != null && getObj(x, y - 1) == null))
        {
            res = Instantiate(borderParts[0]);
            res.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        //Goc 270
        else if ((getObj(x, y) != null && getObj(x - 1, y) != null && getObj(x - 1, y - 1) != null && getObj(x, y - 1) == null)
         || (getObj(x, y) == null && getObj(x - 1, y) == null && getObj(x - 1, y - 1) == null && getObj(x, y - 1) != null))
        {
            res = Instantiate(borderParts[0]);
            res.transform.eulerAngles = new Vector3(0, 0, 270);
        }
        //Canh 0
        else if ((getObj(x, y) != null && getObj(x - 1, y) != null && getObj(x - 1, y - 1) == null && getObj(x, y - 1) == null)
         || (getObj(x, y) == null && getObj(x - 1, y) == null && getObj(x - 1, y - 1) != null && getObj(x, y - 1) != null))
        {
            res = Instantiate(borderParts[1]);
            res.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //Canh 90
        else if ((getObj(x, y) != null && getObj(x - 1, y) == null && getObj(x - 1, y - 1) == null && getObj(x, y - 1) != null)
         || (getObj(x, y) == null && getObj(x - 1, y) != null && getObj(x - 1, y - 1) != null && getObj(x, y - 1) == null))
        {
            res = Instantiate(borderParts[1]);
            res.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        //Chu thap
        else if ((getObj(x, y) != null && getObj(x - 1, y) == null && getObj(x - 1, y - 1) != null && getObj(x, y - 1) == null)
         || (getObj(x, y) == null && getObj(x - 1, y) != null && getObj(x - 1, y - 1) == null && getObj(x, y - 1) != null))
        {
            res = Instantiate(borderParts[2]);

        }
        if (res != null)
        {
            res.transform.position = new Vector3(x - Map.width / 2, y - Map.width / 2, 0);
            res.transform.parent = circleContainer.transform;
        }
        return res;

    }
    void createBorder()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                getBorder(i, j);
            }
        }
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
    void touchBegin(Vector2 screenPosition)
    {
        selectedObject = ObjectClicked(screenPosition);
        if (selectedObject != null)
        {
            beginPos = Camera.main.ScreenToWorldPoint(screenPosition);
            slCircle = selectedObject.GetComponent<Circle>();
        }

    }
    void touchMove(Vector2 screenPosition)
    {
        if (selectedObject != null)
        {
            curPosObj.transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
            //find dragDirection
            if (((Vector2)curPosObj.transform.position - beginPos).magnitude > 0.1f && dragOrientation == Vector2.zero)
            {
                beginObjPos = curPosObj.transform.position;
                float angle = Vector2.SignedAngle(Vector2.right, ((Vector2)curPosObj.transform.position - beginPos));
                foreach (GameObject item in listTemp)
                {
                    Destroy(item);
                }
                listTemp.Clear();
                if (angle > 45 && angle < 135 || angle > -135 && angle < -45)
                {
                    dragOrientation = Vector2.up;
                    for (int i = 0; i < Map.height; i++)
                    {
                        if (getObj((int)slCircle.Pos.x, i) != null && getObj((int)slCircle.Pos.x, i - 1) == null)
                        {
                            listTemp.Add(Instantiate(circlePrefab, new Vector3((int)slCircle.Pos.x - Map.width / 2 + 0.5f, i - 1 - Map.height / 2 + 0.5f, 0), Quaternion.identity));
                        }
                    }
                }
                else
                {
                    dragOrientation = Vector2.right;
                    for (int i = 0; i < Map.width; i++)
                    {
                        if (getObj(i, (int)slCircle.Pos.y) != null && getObj(i - 1, (int)slCircle.Pos.y) == null)
                        {
                            listTemp.Add(Instantiate(circlePrefab, new Vector3(i - 1 - Map.width / 2 + 0.5f, (int)slCircle.Pos.y - Map.height / 2 + 0.5f), Quaternion.identity));
                        }
                    }
                }
                offSet = 0;
            }
        }
    }
    void touchEnd(Vector2 screenPosition)
    {
       
        if (CheckAnDiem())
        {
            exploding = true;
            //ok xac nhan vi tri
            AnDiem();
        }
        else
        {
            //reset vi tri
            curPosObj.transform.DOMove(beginObjPos, 0.3f);
        }
        StartCoroutine(waitAnimation());
    }
    IEnumerator waitAnimation()
    {
        animating = true;
        yield return new WaitForSeconds(0.3f);
        animating = false;
        selectedObject = null;
        dragOrientation = Vector2.zero;
    }
    GameObject getRealObj(int x, int y)
    {
        if (dragOrientation == Vector2.up && x == slCircle.Pos.x)
        {
            return getObj(x, findNextIdx(y, -Mathf.FloorToInt(vectorDrag.y + 0.5f)));
        }
        else
        if (dragOrientation == Vector2.right && y == slCircle.Pos.y)
        {
            return getObj(findNextIdx(x, -Mathf.FloorToInt(vectorDrag.x + 0.5f)), y);
        }
        else
            return getObj(x, y);
    }
    List<GameObject> Loang(int x, int y)
    {
        List<GameObject> res = new List<GameObject>();
        res.Add(getRealObj(x, y));
        checkMatrix[x, y] = true;
        if (getObj(x, y + 1) != null)
        {
            if (checkMatrix[x, y + 1] == false && getRealObj(x, y).GetComponent<SpriteRenderer>().sprite == getRealObj(x, y + 1).GetComponent<SpriteRenderer>().sprite)
            {
                res.AddRange(Loang(x, y + 1));
            }
        }
        if (getObj(x, y - 1) != null)
        {
            if (checkMatrix[x, y - 1] == false && getRealObj(x, y).GetComponent<SpriteRenderer>().sprite == getRealObj(x, y - 1).GetComponent<SpriteRenderer>().sprite)
            {
                res.AddRange(Loang(x, y - 1));
            }
        }
        if (getObj(x + 1, y) != null)
        {
            if (checkMatrix[x + 1, y] == false && getRealObj(x, y).GetComponent<SpriteRenderer>().sprite == getRealObj(x + 1, y).GetComponent<SpriteRenderer>().sprite)
            {
                res.AddRange(Loang(x + 1, y));
            }
        }
        if (getObj(x - 1, y) != null)
        {
            if (checkMatrix[x - 1, y] == false && getRealObj(x, y).GetComponent<SpriteRenderer>().sprite == getRealObj(x - 1, y).GetComponent<SpriteRenderer>().sprite)
            {
                res.AddRange(Loang(x - 1, y));
            }
        }
        return res;
    }
    void AnDiem()
    {
        foreach (GameObject item in listTemp)
        {
            Destroy(item);
        }
        listTemp.Clear();

        for (int i = 0; i < Map.width; i++)
        {
            for (int j = 0; j < Map.height; j++)
            {
                checkMatrix[i, j] = false;
            }
        }
        //destroy
        List<GameObject> allDestroyObj = new List<GameObject>();
        for (int i = 0; i < Map.width; i++)
        {
            for (int j = 0; j < Map.height; j++)
            {
                if (getRealObj(i, j) != null && checkMatrix[i, j] == false)
                {
                    List<GameObject> listObj = Loang(i, j);
                    if (listObj.Count >= 3)
                    {
                        allDestroyObj.AddRange(listObj);
                        foreach (GameObject obj in listObj)
                        {
                            obj.transform.DOScale(0, 0.5f);
                            Destroy(obj, 0.5f);
                        }
                    }
                }
            }
        }
        //sua idx
        //gan lai mang
        GameObject[,] matrixTemp = new GameObject[Map.width, Map.height];
        for (int i = 0; i < Map.width; i++)
        {
            for (int j = 0; j < Map.height; j++)
            {
                if (Map.GetPixel(i, j).a == 1 && allDestroyObj.IndexOf(getRealObj(i, j)) < 0)
                {
                    matrixTemp[i, j] = getRealObj(i, j);
                    matrixTemp[i, j].GetComponent<Circle>().Pos = new Vector2(i, j);
                    matrixTemp[i, j].transform.DOMove(new Vector3(i - Map.width / 2 + 0.5f, j - Map.height / 2 + 0.5f), 1f).SetEase(Ease.OutBounce);
                }
            }
        }

        matrix = matrixTemp;
        //sua idx
        for (int i = 0; i < Map.width; i++)
        {
            for (int j = 0; j < Map.height; j++)
            {
                if (Map.GetPixel(i, j).a == 1 && matrix[i, j] == null)
                {
                    for (int c = j + 1; c < Map.height; c++)
                    {
                        if (matrix[i, c] != null)
                        {
                            matrix[i, j] = matrix[i, c];
                            matrix[i, j].GetComponent<Circle>().Pos = new Vector2(i, j);
                            matrix[i, j].transform.DOMove(new Vector3(i - Map.width / 2 + 0.5f, j - Map.height / 2 + 0.5f), 1f).SetEase(Ease.OutBounce);
                            matrix[i, c] = null;
                            break;
                        }
                    }
                    if (matrix[i, j] == null)
                    {
                        matrix[i, j] = Instantiate(circlePrefab, new Vector3(i - Map.width / 2 + 0.5f, Map.height / 2 + 0.5f, 0), Quaternion.identity);
                        matrix[i, j].GetComponent<Circle>().Pos = new Vector2(i, j);
                        matrix[i, j].transform.DOMove(new Vector3(i - Map.width / 2 + 0.5f, j - Map.height / 2 + 0.5f), 1f).SetEase(Ease.OutBounce);
                    }
                }
            }
        }
        StartCoroutine(waitExploding());
    }
    IEnumerator waitExploding()
    {
        yield return new WaitForSeconds(1f);
        exploding = false;
        if (CheckAnDiem())
        {
            AnDiem();
        }
    }
    bool CheckAnDiem()
    {
        //set checkMatrix về false;
        for (int i = 0; i < Map.width; i++)
        {
            for (int j = 0; j < Map.height; j++)
            {
                checkMatrix[i, j] = false;
            }
        }
        for (int i = 0; i < Map.width; i++)
        {
            for (int j = 0; j < Map.height; j++)
            {
                if (getRealObj(i, j) != null && checkMatrix[i, j] == false)
                {
                    if (Loang(i, j).Count >= 3)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    void ObjectUpdate()
    {
        if (selectedObject != null && dragOrientation != Vector2.zero&&!exploding)
        {
            vectorDrag = (Vector2)curPosObj.transform.position - beginObjPos;
            if (dragOrientation == Vector2.up)
            {
                //setPosition RealObj
                for (int i = 0; i < Map.height; i++)
                {
                    if (getObj((int)slCircle.Pos.x, i) != null)
                    {
                        getObj((int)slCircle.Pos.x, i).transform.position = new Vector3(slCircle.Pos.x - Map.width / 2 + 0.5f, findNextIdx(i, Mathf.FloorToInt(vectorDrag.y + 0.5f)) - Map.height / 2 + 0.5f + vectorDrag.y - Mathf.Floor(vectorDrag.y + 0.5f));
                    }
                }
                //trigger: swapPosition, changeTempColor, checkAnDiem;
                if ((offSet * (vectorDrag.y - Mathf.Floor(vectorDrag.y + 0.5f)) < 0 && Mathf.Abs(offSet)>0.2f)|| (Mathf.Abs(offSet) > 0.02f&& Mathf.Abs(offSet) < 0.2f))
                {
                    int dau = (int)((vectorDrag.y - Mathf.Floor(vectorDrag.y + 0.5f)) / Mathf.Abs(vectorDrag.y - Mathf.Floor(vectorDrag.y + 0.5f)));
                    int c = (listTemp.Count - 1 + dau) % listTemp.Count;
                    for (int i = 0; i < Map.height; i++)
                    {
                        if (getObj((int)slCircle.Pos.x, i) != null && getObj((int)slCircle.Pos.x, i + dau) == null)
                        {
                            c = (c + 1) % listTemp.Count;
                            listTemp[c].GetComponent<SpriteRenderer>().sprite = matrix[(int)slCircle.Pos.x, findNextIdx(i, -Mathf.FloorToInt(vectorDrag.y + 0.5f))].GetComponent<SpriteRenderer>().sprite;
                        }
                    }
                }
                offSet = vectorDrag.y - Mathf.Floor(vectorDrag.y + 0.5f);
                int k = 0;
                for (int i = 0; i < Map.height; i++)
                {
                    if (getObj((int)slCircle.Pos.x, i) != null && getObj((int)slCircle.Pos.x, i + (int)(-offSet / Mathf.Abs(offSet))) == null && offSet!=0)
                    {
                        listTemp[k].transform.position = new Vector3(slCircle.Pos.x - Map.width / 2 + 0.5f, (i + (int)(-offSet / Mathf.Abs(offSet))) - Map.height / 2 + 0.5f + offSet);
                        k++;
                    }
                } 
            }

            if (dragOrientation == Vector2.right)
            {
                
                for (int i = 0; i < Map.width; i++)
                {
                    if (getObj(i, (int)slCircle.Pos.y) != null)
                    {
                        getObj(i, (int)slCircle.Pos.y).transform.position = new Vector3(findNextIdx(i, Mathf.FloorToInt(vectorDrag.x + 0.5f)) - Map.width / 2 + 0.5f + vectorDrag.x - Mathf.Floor(vectorDrag.x + 0.5f), slCircle.Pos.y - Map.height / 2 + 0.5f);
                    }
                }
                //trigger: swapPosition, changeTempColor, checkAnDiem;
               if ((offSet * (vectorDrag.x - Mathf.Floor(vectorDrag.x + 0.5f)) < 0 && Mathf.Abs(offSet) > 0.2f)|| (Mathf.Abs(offSet) > 0.02f && Mathf.Abs(offSet) < 0.2f))
                {
                    int dau = (int)((vectorDrag.x - Mathf.Floor(vectorDrag.x + 0.5f)) / Mathf.Abs(vectorDrag.x - Mathf.Floor(vectorDrag.x + 0.5f)));
                    int c = (listTemp.Count - 1 + dau) % listTemp.Count;
                    for (int i = 0; i < Map.height; i++)
                    {
                        if (getObj(i, (int)slCircle.Pos.y) != null && getObj(i + dau, (int)slCircle.Pos.y) == null)
                        {
                            c = (c + 1) % listTemp.Count;
                            listTemp[c].GetComponent<SpriteRenderer>().sprite = matrix[findNextIdx(i, -Mathf.FloorToInt(vectorDrag.x + 0.5f)), (int)slCircle.Pos.y].GetComponent<SpriteRenderer>().sprite;
                        }
                    }
                }
                
                offSet = vectorDrag.x - Mathf.Floor(vectorDrag.x + 0.5f);
                //
                int k = 0;
                for (int i = 0; i < Map.width; i++)
                {
                    if (getObj(i, (int)slCircle.Pos.y) != null && getObj(i + (int)(-offSet / Mathf.Abs(offSet)), (int)slCircle.Pos.y) == null && offSet != 0)
                    {
                        listTemp[k].transform.position = new Vector3((i + (int)(-offSet / Mathf.Abs(offSet))) - Map.width / 2 + 0.5f + offSet, slCircle.Pos.y - Map.height / 2 + 0.5f);
                        k++;
                    }
                }
            }
        }
    }
    int findNextIdx(int curIdx,int step)
    {
        if (step != 0)
        {
            int dau = step / Mathf.Abs(step);
            int i = curIdx, c = 0;
            while (c < Mathf.Abs(step))
            {
                if (dragOrientation == Vector2.up)
                {
                    i = (i + Map.height + dau) % Map.height; 
                    if (getObj((int)slCircle.Pos.x, i) != null) c++;
                }
                if (dragOrientation == Vector2.right)
                {
                    i = (i+ Map.width+ dau) % Map.width;
                    if (getObj(i, (int)slCircle.Pos.y) != null) c++;
                }
            }
            return i;
        }
        else return curIdx;
    }
    void Update()
    {
        if (!animating&&!exploding)
        {
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
       
        ObjectUpdate();
    }
    public void RePlay()
    {
        SceneManager.LoadScene("MainGame");
    }
}
