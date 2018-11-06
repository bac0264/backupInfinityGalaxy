using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DragCamera : MonoBehaviour
{
    public static DragCamera instance;
    // Use this for initialization
    public float speed = 100f;
    public float limitDown = 0;
    [SerializeField]
    public float limitUp;
    Vector3 MouseStart;
    Vector3 ScrollVelocity;
    private bool touching;
    GameObject missionpanel;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void setLimitUp(float limit)
    {
        limitUp = limit;
        Debug.Log(limitUp);
    }
    private void FixedUpdate()
    {
        Vector3 pos = Camera.main.transform.position;
        missionpanel = GameObject.FindGameObjectWithTag("MissionPanel");
        if (Input.GetKey("w"))
        {
            if (pos.y <= limitUp)
                pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            if (pos.y >= limitDown)
                pos.y -= speed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && !touching)
        {
            touching = true;
            _TouchBegin(Input.mousePosition);

        }
        if (touching)
        {
            _TouchHold(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            touching = false;
            _TouchEnd(Input.mousePosition);
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _TouchBegin(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                _TouchHold(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _TouchEnd(Input.GetTouch(0).position);
            }
        }

        DOTween.Kill("MoveY");
        if (Camera.main.transform.position.y - ScrollVelocity.y >= limitDown && Camera.main.transform.position.y - ScrollVelocity.y <= limitUp)
        {
            Camera.main.transform.DOMoveY(Camera.main.transform.position.y - ScrollVelocity.y, 0.1f).SetId("MoveY");
            if (!touching)
                ScrollVelocity = Vector2.Lerp(ScrollVelocity, Vector2.zero, 0.125f);
        }
        else if (Camera.main.transform.position.y - ScrollVelocity.y < limitDown)
        {
            Camera.main.transform.DOMoveY(limitDown, 0.1f);
        }
        else
        {
            Camera.main.transform.DOMoveY(limitUp, 0.1f);
        }
    }
    // Use this for initialization
    public void _TouchBegin(Vector3 position)
    {
        MouseStart = click(position);
    }
    public void _TouchHold(Vector3 position)
    {
        if (missionpanel != null)
        {
            Debug.Log("missionpanel");
        }
        else
        {
            ScrollVelocity = Camera.main.ScreenToWorldPoint(position);
            ScrollVelocity = ScrollVelocity - MouseStart;
        }
        //Vector3 MouseMove = click(position);
        //Vector3 temp = Camera.main.transform.position;
        //if (MouseStart.y - MouseMove.y < 0)
        //{

        //    if ((temp.y - (MouseMove.y - MouseStart.y)) >= limitDown)
        //    {
        //        temp.y = temp.y - (MouseMove.y - MouseStart.y);
        //    }
        //    else temp.y = limitDown; ;
        //}
        //else if (MouseStart.y - MouseMove.y > 0)
        //{
        //    if ((temp.y + (MouseStart.y - MouseMove.y)) <= limitUp)
        //    {
        //        temp.y = temp.y + (MouseStart.y - MouseMove.y);
        //    }
        //    else temp.y = limitUp;
        //}
        //// transform.position = temp;

        //    DOTween.Clear();
        //    Camera.main.transform.DOMove(temp, 1.0f);
        //Camera.main.transform.position = temp;

    }
    public void _TouchEnd(Vector3 position)
    {
    }
    public Vector3 click(Vector3 position)
    {
        position = Camera.main.ScreenToWorldPoint(position);
        position.z = Camera.main.transform.position.z;
        return position;
    }
}
