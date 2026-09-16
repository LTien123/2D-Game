using UnityEngine;
using UnityEngine.InputSystem;

public class BoosterFlameController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector3 originalPosition;
    void Start()
    {
       originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Keyboard.current.spaceKey.isPressed)
        {
            transform.localPosition = new Vector3(0, -2f, -1f);
        } else if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            transform.localPosition = originalPosition;
        } 
    }
}
