using UnityEngine;

public class Tiles : MonoBehaviour
{

    [SerializeField]
    private Color Base, Offset, hightlight;
    [SerializeField]
    private SpriteRenderer rend;

    private Color iscolor;
    public Color playerhere;
    


    //public GameObject playertank;
    void Start()
    {
    
    }
    

    void OnMouseEnter()
    {
        rend.color = hightlight;
    }
    void OnMouseExit()
    {
        rend.color = Base;
    }
    

    void OnMouseUp()
    {
        rend.color = Base;
    }
}   
