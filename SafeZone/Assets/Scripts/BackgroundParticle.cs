
using UnityEngine;

public class BackgroundParticle : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
