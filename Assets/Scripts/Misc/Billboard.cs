using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera _camera;

    private void Start()
    {
        _camera = Camera.main;    
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new(0, _camera.transform.eulerAngles.y, 0);
    }
}
