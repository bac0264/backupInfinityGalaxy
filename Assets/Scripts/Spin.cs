using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 spinStrength = new Vector3(0, 0, 1);
    public bool active = false;

    void Update()
    {
        if (active)
        {
            transform.Rotate(spinStrength);
        }
    }
}