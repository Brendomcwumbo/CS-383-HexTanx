using UnityEngine;

public class Terrains : Tiles
{
    
    private GameObject playertankOBJ;
    private int playerHealthshown;
    private int damage = 10;
    private PlayerTank playertc;
    void Start()
    {
        playertankOBJ = GameObject.Find("PlayerTank");

    if (playertankOBJ != null)
    {
        playertc = playertankOBJ.GetComponent<PlayerTank>();

        if (playertc == null)
        {
            Debug.LogError("PlayerTank component not found on PlayerTank object.");
        }
    }
    else
    {
        Debug.LogError("PlayerTank object not found.");
    }
       //playertank = GameObject.Find("PlayerTank");
       //PlayerTank playertc = playertank.GetComponent<PlayerTank>();
    }
    void Update()
    {
        if(playertankOBJ == null)
        {
            playertankOBJ = GameObject.Find("PlayerTank");
            if(playertc == null)
            {
                playertc = playertankOBJ.GetComponent<PlayerTank>();
            }
        }
        //playertankOBJ = GameObject.Find("PlayerTank");
        //playertc = playertankOBJ.GetComponent<PlayerTank>();
        takeDamage();
    }
    void takeDamage()
    {

        playerHealthshown = playertc.GetHealth();
        if(playertankOBJ.transform.position.x == this.transform.position.x && playertankOBJ.transform.position.y == this.transform.position.y)
        {
            //Debug.Log(": "+playerHealthshown);
            playertc.SetHealth(playerHealthshown-damage);
            playerHealthshown = playertc.GetHealth();
           // Debug.Log(": "+playerHealthshown);
            
        }
    }
}
