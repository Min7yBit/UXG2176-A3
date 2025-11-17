using UnityEngine;

public class Screw : MonoBehaviour
{

    private Renderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        renderer.material.color = Color.yellow;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Collect Screw");
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color= Color.white;
    }
}
