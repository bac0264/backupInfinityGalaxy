using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopupManager: MonoBehaviour
{
    public void showPopup(string name)
    {
        StartCoroutine(showPopupAndDestroy(name));
    }
    public void showPopupAtPos(string name,Vector3 pos)
    {
        showPopupAtPosAndDestroy(name, pos);
    }
    public void showDialog(string name)
    {
        GameObject popup = transform.Find(name).gameObject;
        popup.SetActive(true);
    }
    IEnumerator showPopupAndDestroy(string name)
    {
        Transform popup=transform.Find(name);
        popup.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        popup.gameObject.SetActive(false);
    }
    IEnumerator showPopupAtPosAndDestroy(string name,Vector3 pos)
    {
        GameObject popup = transform.Find(name).gameObject;
        popup.transform.position = new Vector3(pos.x+2.5f,pos.y+3.5f);
        popup.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        popup.SetActive(false);
    }
}
