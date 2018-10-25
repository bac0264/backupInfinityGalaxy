using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class WarningPanel : MonoBehaviour {
    public GameObject panel;
    public enum _Type
    {
        Exit,
        Yes,
        No,
    };
    _Type type = new _Type();

    private void Start()
    {
        ShopManager.instance.select = true;
        gameObject.GetComponent<Animator>().Play("In");
        gameObject.GetComponent<Animator>().SetBool("In", false);
        if (panel != null)
        {
            panel.SetActive(true);
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.8f);
        }
    }
    public void isExit() {
        type = _Type.Exit;
        StartCoroutine(loadAnimation());
        Destroy(gameObject);
    }
    public void isYes()
    {
        // do somthing
        type = _Type.Yes;
        StartCoroutine(loadAnimation());
    }

    public void isNo()
    {
        type = _Type.No;
        StartCoroutine(loadAnimation());
    }

    IEnumerator loadAnimation()
    {
        if (type == _Type.Yes)
        {
            // do something
            Destroy(gameObject);
        }
        else
        {
            if (panel != null)
            {
                panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
            }
            if (type == _Type.No)
            {
                gameObject.transform.GetChild(1).GetChild(3).GetComponent<Animator>().Play("ScaleButton");
            }
            else if (type == _Type.Exit)
            {
                gameObject.transform.GetChild(1).GetChild(1).GetComponent<Animator>().Play("ScaleButton");
            }
            yield return new WaitForSeconds(0.2f);
            Animator ani = gameObject.GetComponent<Animator>();
            ani.Play("out");
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}
