using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlanetGenerator : SerializedMonoBehaviour
{
    //public GameObject PlanetPrefab;
    public GameObject flyPaticle;
    [BoxGroup("Texture create background")]
    public Texture2D loi, lom;
    LineRenderer Line;
    [TableMatrix(SquareCells = true)]
    public GameObject[,] matrixIns = new GameObject[6, 7];
    GameObject[,] matrix = new GameObject[6, 7];
    List<GameObject> planetConnected=new List<GameObject>();
    Vector3 saveLoopPos=new Vector3(-1,-1,-1);
    public bool isFalling = false,victory=false;
    bool touching = false;
    float saveTime;
    // Use this for initialization
    void Start () {
        saveTime = 0;
        Application.targetFrameRate = 60;
        Line = GetComponentInChildren<LineRenderer>();
        //Camera.main.orthographicSize = ((6 + 1) * 1.0f / Screen.width * Screen.height) / 2;
        Camera.main.transform.position = new Vector3(2.5f,3.5f, -10f);
        setBackground(matrixIns);
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
    void setBackground(GameObject[,] matrixIns)
    {
        Texture2D res = new Texture2D(100*6+lom.width/2, 100*7+lom.height/2, TextureFormat.ARGB32,false);
        for (int i = 0; i < res.width; i++)
        {
            for (int j = 0; j < res.height; j++)
            {
                res.SetPixel(i, j, new Color(0, 0, 0, 0));
            }
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (matrixIns[i, 6-j] != null)
                {
                    for (int k = 0; k < loi.width; k++)
                    {
                        for (int l = 0; l < loi.height; l++)
                        {
                            if(loi.GetPixel(k, l).a> res.GetPixel(i * 100 + k, j * 100 + l).a)
                                res.SetPixel(i * 100 + k, j * 100 + l, loi.GetPixel(k, l));
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if ((matrixIns[i, 5-j] != null && matrixIns[i + 1, 5 - j + 1]!=null) || (matrixIns[i + 1, 5 - j] != null && matrixIns[i, 5 - j + 1]!=null))
                {
                    for (int k = 0; k < lom.width; k++)
                    {
                        for (int l = 0; l < lom.height; l++)
                        {
                            if(lom.GetPixel(k, l).a > res.GetPixel(i * 100 + 90 + k, j * 100 + 90 + l).a)
                                res.SetPixel(i * 100 + 90 + k, j * 100 + 90 + l, lom.GetPixel(k, l));
                        }
                    }
                }
            }
        }
        res.Apply();
        Sprite tempSprite= Sprite.Create(res, new Rect(0, 0, res.width, res.height), new Vector2(0.5f, 0.5f));
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = tempSprite;
        gameObject.GetComponentInChildren<SpriteMask>().sprite = tempSprite;
        //File.WriteAllBytes("D:/Saved.png", res.EncodeToPNG());
    }
    GameObject rndPlanet()
    {
        return GetComponent<GamePlayManager>().getRandromPlanet();
        //return null;
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetMouseButtonDown(0) && !touching)
        {
            touching = true;
            touchBegin(Input.mousePosition);
            saveTime = 0;
        }
        if (touching)
        {
            saveTime = 0;
            touchMove(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            saveTime = 0;
            touching = false;
            touchEnd(Input.mousePosition);
        }
        if (saveTime < 4 && saveTime + Time.fixedDeltaTime > 4)
        {
            Debug.Log("Gợi ý");
            GameObject obj = canConnect();
            obj.GetComponent<Animator>().SetTrigger("Touched");
            saveTime = 0;
        }
        saveTime += Time.fixedDeltaTime;
        
    }
    private void Update()
    {
        /*if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchBegin(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchMove(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchEnd(Input.GetTouch(0).position);
            }
        }*/
    }
    private void touchBegin(Vector3 mousePosition)
    {

    }
    private void touchMove(Vector3 mousePosition)
    {
        GameObject curObj = ObjectClicked(mousePosition);
        if (curObj != null && !isFalling && GetComponent<GamePlayManager>().ValueLimit>0&&!victory)
        {
            if (planetConnected.Count == 0)
            {
                //cham vien dau tien
                planetConnected.Add(curObj);
                curObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                curObj.GetComponent<Animator>().SetTrigger("Touched");
                /*curObj.transform.DOScale(1.2f, 0.2f);
                curObj.transform.DOScale(1f, 0.2f).SetDelay(0.2f);*/
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
                    /*curObj.transform.DOScale(1.2f, 0.2f);
                    curObj.transform.DOScale(1f, 0.2f).SetDelay(0.2f);*/
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
                            //noi thanh vong
                            saveLoopPos = curObj.transform.position;
                            for (int i = 0; i < 6; i++)
                            {
                                for (int j = 0; j < 6; j++)
                                {
                                    if (matrix[i, j] != null)
                                    {
                                        if (matrix[i, j].GetComponent<SpriteRenderer>().sprite == planetConnected[0].GetComponent<SpriteRenderer>().sprite)
                                            matrix[i, j].GetComponent<Animator>().SetTrigger("Touched");

                                       /* matrix[i, j].transform.DOScale(1.2f, 0.2f);
                                        matrix[i, j].transform.DOScale(1f, 0.2f).SetDelay(0.2f);*/
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if(curObj == planetConnected[planetConnected.Count - 1]&& saveLoopPos!= new Vector3(-1, -1, -1))
            {
                saveLoopPos = new Vector3(-1, -1, -1);
            }
        }
        if (!isFalling)
        {
            //DrawLine
            Line.positionCount = planetConnected.Count + 1;
            for (int i = 0; i < planetConnected.Count; i++)
            {
                Vector3 tempPos = planetConnected[i].transform.localPosition;
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
                    if (matrix[i, j] != null)
                    {
                        if (matrix[i, j].GetComponent<SpriteRenderer>().sprite == temp)
                            planetConnected.Add(matrix[i, j]);
                    }
                }
            }
            saveLoopPos=new Vector3(-1, -1, -1);
        }

        if (planetConnected.Count >= 2)
        {
            //AnDiem
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (planetConnected.IndexOf(matrix[i, j]) > -1)
                    {
                        matrix[i, j] = null;
                    }
                }
                for (int j = 0; j < 7; j++)
                {
                    if (matrix[i, j] == null && matrixIns[i, 6 - j] != null)
                    {
                        bool check = false;
                        for (int k = j + 1; k < 7; k++)
                        {
                            if (matrix[i, k] != null)
                            {
                                matrix[i, j] = matrix[i, k];
                                StartCoroutine(PlanetFalling(matrix[i, j].transform, j));
                                matrix[i, k] = null;
                                check = true; break;
                                
                            }
                        }
                        if (!check)
                        {
                            matrix[i, j] = Instantiate(rndPlanet(), new Vector3(i, 7 , 0), Quaternion.identity, transform);
                            StartCoroutine(PlanetFalling(matrix[i, j].transform, j));
                        }
                    }
                }
            }
            StartCoroutine(Explosion());
            GetComponent<GamePlayManager>().Moved();
        }
        else if(planetConnected.Count==1)
        {
            planetConnected[0].GetComponent<SpriteRenderer>().sortingOrder = 0;
            planetConnected.Clear();
        }
        Line.positionCount = 0;
    }
    IEnumerator Explosion()
    {
        TargetPlanet targetFly = null;
        foreach (var item in GetComponent<GamePlayManager>().targetPlanet)
        {
            if (planetConnected[0].GetComponent<SpriteRenderer>().sprite == item.ItemTarget.GetComponent<Image>().sprite&& item.Count<item.NumOfTarget)
            {
                targetFly = item;
                //set color
                var col = flyPaticle.GetComponent<ParticleSystem>().colorOverLifetime;
                col.enabled = true;
                Gradient grad = new Gradient();
                Color c = planetConnected[0].GetComponent<PlanetElement>().GetPlanetColor();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(c, 0.3f), new GradientColorKey(c, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 0.4f), new GradientAlphaKey(0.0f, 1.0f) });
                col.color = grad;
                if (item.Count + planetConnected.Count >= item.NumOfTarget)
                {
                    bool check = true;
                    foreach (var item2 in GetComponent<GamePlayManager>().targetPlanet)
                    {
                        if (item2 != item && item2.Count != item2.NumOfTarget)
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        victory = true;
                    }
                }
                break;
                
            }
        }
        if (planetConnected.Count >= 5)
        {
            Debug.Log(planetConnected[0].transform.position);
            GameObject.FindGameObjectWithTag("PopupContainer").GetComponent<PopupManager>().showPopup("Great");

        }
        for (int i=0;i<planetConnected.Count;i++)
        {
            planetConnected[i].GetComponent<PlanetElement>().Explosion();
            if(planetConnected.Count-i<=6)
                yield return new WaitForSeconds(0.08f);
            GetComponent<GamePlayManager>().addScore(10);
            if (targetFly != null && i<planetConnected.Count)
            {
                GameObject particle = Instantiate(flyPaticle, new Vector3(planetConnected[i].transform.position.x, planetConnected[i].transform.position.y,-1f), Quaternion.identity);
                float timeFly = 1.5f;
                particle.transform.DOMove(targetFly.ItemTarget.transform.position, timeFly).SetEase(Ease.InOutCubic);
                StartCoroutine(flyComplete(targetFly, timeFly));
                Destroy(particle, timeFly);
            }
        }
        planetConnected.Clear();
    }
 
    IEnumerator flyComplete(TargetPlanet target,float time)
    {
        isFalling = true;
        yield return new WaitForSeconds(time);
        if(target.Count<target.NumOfTarget)
        {
            target.Count++;
            /*if (target.Count == target.NumOfTarget)
            {
                
            }
            else*/
            target.ItemTarget.GetComponentInChildren<Text>().text = target.Count.ToString()+"/"+target.NumOfTarget.ToString();
            DOTween.Kill("scoreAdd");
            target.ItemTarget.transform.DOScale(1.3f, 0.05f).SetId("scoreAdd");
            target.ItemTarget.transform.DOScale(1f, 0.2f).SetId("scoreAdd").SetDelay(0.05f);
        }
        isFalling = false;
    }
    IEnumerator PlanetFalling(Transform transform,float endValue)
    {
        isFalling = true;
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
        yield return new WaitForSeconds(0.2f);
        isFalling = false;
        StartCoroutine(affterGetScore());

    }
    IEnumerator affterGetScore()
    {
        do
        {
            yield return new WaitForEndOfFrame();
        } while (isFalling);
        GetComponent<GamePlayManager>().checkGame();
        bool swapped = false;
        while (canConnect()==null)
        {
            swapped = true;
            SwapPlanet();
        }
        if (swapped)
        {
            isFalling = true;
            yield return new WaitForSeconds(0.05f);
            GameObject.FindGameObjectWithTag("PopupContainer").GetComponent<PopupManager>().showPopup("Shuffling");
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (matrix[i, j] != null)
                    {
                        yield return new WaitForSeconds(0.002f);
                        matrix[i, j].transform.DOMove(new Vector3(i, j, 0), 0.5f).SetEase(Ease.InOutQuad);
                    }
                }
            }
            yield return new WaitForSeconds(1f);
            isFalling = false;
        }
    }
    public void SwapPlanet()
    {
        List<GameObject> allObj = new List<GameObject>();
        foreach (var item in matrix)
        {
            if(item!=null&&item.GetComponent<CircleCollider2D>().enabled == true)
                allObj.Add(item);
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (matrixIns[i, 6 - j] != null&& matrix[i,j].GetComponent<CircleCollider2D>().enabled == true)
                {
                    matrix[i, j] = allObj[UnityEngine.Random.Range(0, allObj.Count)];
                    allObj.Remove(matrix[i, j]);
                }
            }
        }
       /* for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (matrixIns[i, 6 - j] != null)
                {
                    matrix[i, j].transform.DOMove(new Vector3(i, j, 0), 1f).SetEase(Ease.OutCirc);
                }
            }
        }*/
    }
    GameObject canConnect()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (getObj(i, j + 1) != null && getObj(i, j) != null)
                {
                    if (getObj(i, j).GetComponent<SpriteRenderer>().sprite == getObj(i, j + 1).GetComponent<SpriteRenderer>().sprite&& getObj(i, j).GetComponent<CircleCollider2D>().enabled == true)
                    {
                        return getObj(i, j);
                    }
                }
                if (getObj(i + 1, j) != null && getObj(i, j) != null)
                {
                    if (getObj(i, j).GetComponent<SpriteRenderer>().sprite == getObj(i + 1, j).GetComponent<SpriteRenderer>().sprite&& getObj(i, j).GetComponent<CircleCollider2D>().enabled == true)
                    {
                        return getObj(i, j);
                    }
                }
            }
        }
        return null;
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
