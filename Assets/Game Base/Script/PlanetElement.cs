using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetElement : MonoBehaviour {
   /* [SerializeField]
    private List<Sprite> ListNormalPlanet;*/
    [SerializeField]
    private GameObject explosionEffect;
    // Use this for initialization
    IEnumerator Start () {
        GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(Random.Range(0,2f));
        GetComponent<Animator>().enabled = true;
    }
    public void Explosion()
    {
        GameObject obj=Instantiate(explosionEffect, transform.position, Quaternion.identity);
        obj.GetComponentsInChildren<ParticleSystem>()[0].startColor = GetPlanetColor();
        obj.GetComponentsInChildren<ParticleSystem>()[2].startColor = GetPlanetColor();
        Destroy(obj, 1f);
        GetComponent<Animator>().SetTrigger("Destroyed");
        Destroy(gameObject, 0.3f);
    }
    public Color GetPlanetColor()
    {
        Rect rect = gameObject.GetComponent<SpriteRenderer>().sprite.textureRect;
        return gameObject.GetComponent<SpriteRenderer>().sprite.texture.GetPixel((int)(rect.x + rect.width / 2), (int)(rect.y + rect.height / 2));
    }
}
