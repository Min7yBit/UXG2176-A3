using UnityEngine;

public class Screw : MonoBehaviour
{

    private Renderer Rrenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rrenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Rrenderer.material.color = Color.yellow;
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
        Rrenderer.material.color= Color.white;
    }
}
