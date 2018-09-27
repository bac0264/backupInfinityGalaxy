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
    void Start () {
        /*Sprite temp= ListNormalPlanet[Random.Range(0,8)];
         GetComponentsInChildren<SpriteRenderer>()[0].sprite = temp;
         GetComponentsInChildren<SpriteRenderer>()[1].sprite = temp;*/
    }
    public void Explosion()
    {
        GameObject obj=Instantiate(explosionEffect, transform.position, Quaternion.identity);
        obj.GetComponentsInChildren<ParticleSystem>()[0].startColor = GetPlanetColor();
        obj.GetComponentsInChildren<ParticleSystem>()[2].startColor = GetPlanetColor();
        Destroy(obj, 1f);
        transform.DOScale(0, 0.3f);
        Destroy(gameObject, 0.3f);
    }
    public Color GetPlanetColor()
    {
   
        Rect rect = gameObject.GetComponent<SpriteRenderer>().sprite.textureRect;
        return gameObject.GetComponent<SpriteRenderer>().sprite.texture.GetPixel((int)(rect.x + rect.width / 2), (int)(rect.y + rect.height / 2));
    }
}
