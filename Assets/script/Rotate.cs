using UnityEngine;

public class Rotate : MonoBehaviour
{

    public Vector3 rot;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rot * Time.deltaTime);
    }
}
