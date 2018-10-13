using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopupManager: MonoBehaviour
{
    public void showPopup(string name)
    {
        StartCoroutine(showPopupAndDestroy(name));
    }
    public void showDialog(string name)
    {
        GameObject popup = transform.Find(name).gameObject;
        popup.SetActive(true);
    }
    IEnumerator showPopupAndDestroy(string name)
    {
        GameObject popup=transform.Find(name).gameObject;
        popup.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        popup.SetActive(false);
    }

    
}
