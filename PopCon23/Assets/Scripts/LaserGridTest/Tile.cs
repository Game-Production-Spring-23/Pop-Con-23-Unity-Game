using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x_cord;
    public int y_cord;

    private int EntryPointNum;

    public GameObject[] paths;
    // Paths goes in clockwise pattern
    /* 0 = N 
     * 1 = NE 
     * 2 = SE 
     * 3 = S 
     * 4 = SW
     * 5 = NW
    */
    public Material Player1Color;
    public Material Player2Color;
    public Material RedShieldColor;
    public Material BlueShieldColor;
    public Material Crossbeams;

    //OUTDATED Old Bools for Mirrors
    
    public bool MirrorStage1NS;
    public bool MirrorStage2NNESSW;
    public bool MirrorStage3NNWSSE;
    public bool MirrorStage4ENEWSW;
    public bool MirrorStage5WNWESE;
    public bool MirrorStage6EW;
    public bool MirrorStage7NorthSplitter;
    public bool MirrorStage8SouthSplitter;
    public bool MirrorStage9Blocker;

    // GameObjects for Mirror Placement
    public GameObject MirrorStage1NSObject;
    public GameObject MirrorStage2NNESSWObject;
    public GameObject MirrorStage3NNWSSEObject;
    public GameObject MirrorStage4ENEWSWObject;
    public GameObject MirrorStage5WNWESEObject;
    public GameObject MirrorStage6EWObject;
    public GameObject MirrorStage7NorthSplitterObject;
    public GameObject MirrorStage8SouthSplitterObject;
    public GameObject MirrorStage9BlockerObject;


    //Indications of whether the tile currently has any items or not
    public bool isEmpty = true;
    public bool hasMirror = false;
    public bool hasSplitter = false;
    public bool hasBlocker = false;
    public bool usedRock = false;
    public bool usedRotator = false;
    public bool usedBigRock = false;
    public bool personalMirror = false;
    public int personalMirrorOwner;

    public bool NorthStart;
    public bool SouthStart;

    public SpriteRenderer CrystalRenderer;
    public Sprite RedCrystalSprite;
    public Sprite BlueCrystalSprite;
    public Sprite RedHitCrystalSprite;
    public Sprite BlueHitCrystalSprite;

    [HideInInspector] // Shielded Entry Bools
    public bool NorthShield;
    [HideInInspector]
    public bool SouthShield;
    [HideInInspector]
    public bool NorthEastShield;
    [HideInInspector]
    public bool NorthWestShield;
    [HideInInspector]
    public bool SouthEastShield;
    [HideInInspector]
    public bool SouthWestShield;

    // GameObjects for Shields
    public GameObject NorthWall;
    public GameObject NorthEastWall;
    public GameObject SouthEastWall;
    public GameObject SouthWall;
    public GameObject SouthWestWall;
    public GameObject NorthWestWall;

    [HideInInspector] // Crystal Bools
    public bool RedCrystal;
    [HideInInspector]
    public bool BlueCrystal;
    private bool RedHasBeenHit;
    private bool BlueHasBeenHit;

    public string CrystalHit;

    //method to check which turn the player is currently on
    private int curTurn = 1;

    void Start()
    {
    }
    public void StartLaser(Material player, string direction)
    {
        switch (direction){
            case "SEDirection":
                if (!ShieldCheck(direction)){
                    return;
                }
                LaserTurnOn(2, player);
                if (!CrystalHitCheck(direction)){
                    return;
                }
                // IF SW PATH IS LAST PATH, CALL HEX SW OF CURRENT HEX
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(2, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                }
            break;
            case "SWDirection":
                if (!ShieldCheck(direction)){
                    return;
                }
                LaserTurnOn(4, player);
                if (!CrystalHitCheck(direction)){
                    return;
                }
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(4, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(1, player);
                    ContinueLaser(-1, 1, player, "SWDirection");
                }
            break;
            case "NEDirection":
                if (!ShieldCheck(direction)){
                    return;
                }
                LaserTurnOn(1, player);
                if (!CrystalHitCheck(direction)){
                    return;
                }
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(1, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1, -1, player, "NEDirection");
                }
            break;
            case "NWDirection":
                if (!ShieldCheck(direction)){
                    return;
                }
                LaserTurnOn(5, player);
                if (!CrystalHitCheck(direction)){
                    return;
                }
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(5, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                }
            break;
            case "NDirection":
                if (!ShieldCheck(direction)){
                    return;
                }
                LaserTurnOn(0, player);
                if (!CrystalHitCheck(direction)){
                    return;
                }
                // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(0, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                }
            break;
            case "SDirection":
                if (!ShieldCheck(direction)){
                    return;
                }
                LaserTurnOn(3, player);
                if (!CrystalHitCheck(direction)){
                    return;
                }
                // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(3, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(0, player);
                    ContinueLaser(0, 2, player, "SDirection");
                }
            break;
            default:
            Debug.Log(direction);
            break;
        }
    }

    //Tells Tile Next door according to where the laser comes out to start their laser script
    public void ContinueLaser(int x, int y, Material player, string direction)
    {
        try 
        {
            GameManager.instance.gridArray[x_cord + x, y_cord + y].StartLaser(player, direction);
        }
        catch (System.IndexOutOfRangeException ex)
        {
            return;
        }
        catch (System.StackOverflowException ex){
            return;
        }
    }
    public bool LaserReflect(int entry, Material player)
    {
        switch (entry){
            case 0:
                if (MirrorStage2NNESSW)
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1,-1,player, "NEDirection");
                    return true;
                }
                if (MirrorStage3NNWSSE)
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    return true;
                }
                if (MirrorStage4ENEWSW)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage5WNWESE)
                {
                    LaserTurnOn(1, player);
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
                if (MirrorStage7NorthSplitter) {
                   LaserTurnOn(4, player);
                   LaserTurnOn(2, player);
                   ContinueLaser(1,-1,player, "NEDirection");
                   ContinueLaser(-1, -1, player, "NWDirection");
                   return true;
                }
            break;
            case 1:
                if (MirrorStage1NS)
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    return true;
                }
                if (MirrorStage2NNESSW)
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
                if (MirrorStage5WNWESE)
                {
                    LaserTurnOn(0, player);
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
                if (MirrorStage6EW)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage8SouthSplitter) {
                    LaserTurnOn(3, player);
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
            break;
            case 2:
                if (MirrorStage1NS)
                {
                   LaserTurnOn(1, player);
                   ContinueLaser(-1, 1, player, "SWDirection");
                   return true;
                }
                if (MirrorStage3NNWSSE)
                {
                    LaserTurnOn(0, player);
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
                if (MirrorStage4ENEWSW)
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
                if (MirrorStage6EW)
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1, -1, player, "NEDirection");
                    return true;
                }
                if (MirrorStage7NorthSplitter) {
                    LaserTurnOn(4, player);
                    LaserTurnOn(0, player);
                    ContinueLaser(1,-1,player, "NEDirection");
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
            break;
            case 3:
                if (MirrorStage2NNESSW)
                {
                    LaserTurnOn(1, player);
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
                if (MirrorStage3NNWSSE)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage4ENEWSW)
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    return true;
                }
                if (MirrorStage5WNWESE)
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1, -1, player, "NEDirection");
                    return true;
                }
                if (MirrorStage8SouthSplitter) {
                    LaserTurnOn(1, player);
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
            break;
            case 4:
                if (MirrorStage1NS)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage2NNESSW)
                {
                    LaserTurnOn(0, player);
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
                if (MirrorStage5WNWESE)
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
                if (MirrorStage6EW)
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    return true;
                }
                if (MirrorStage7NorthSplitter) {
                    LaserTurnOn(0, player);
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
            break;
            case 5:
                if (MirrorStage1NS)
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1, -1, player, "NEDirection");
                    return true;
                }
                if (MirrorStage3NNWSSE)
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
                if (MirrorStage4ENEWSW)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage6EW)
                {
                    LaserTurnOn(1, player);
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
                if (MirrorStage8SouthSplitter) {
                    LaserTurnOn(1, player);
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
            break;
        }
        return false;
    }
    public void LaserTurnOn(int entryPoint, Material player)
    {
        if (paths[entryPoint].activeSelf == true) {
            paths[entryPoint].GetComponent<Renderer>().material = Crossbeams;
        }
        else {
            paths[entryPoint].SetActive(true);
            paths[entryPoint].GetComponent<Renderer>().material = player;
        }
    }

    void OnMouseOver()
    {

        Debug.Log("Mouse is over");
        if (!Input.GetMouseButton(0) && TurnBasedSystem.draggingItem == true && isEmpty == false)
        {
            TurnBasedSystem.curTile = this;

            TurnBasedSystem.isHovering = true;

        }

        //in the case where the player is trying to drop either a rock or a rotator over an existing item
        else if(!Input.GetMouseButton(0) && TurnBasedSystem.draggingItem == true && (TurnBasedSystem.currentItem == 4 || TurnBasedSystem.currentItem == 5 || TurnBasedSystem.currentItem == 7))
        {

            TurnBasedSystem.curTile = this;

            TurnBasedSystem.isHovering = true;

        }

    }

    
    public void CrystalGeneration(){
        if (RedCrystal){
            CrystalRenderer.sprite = RedCrystalSprite;
        }
        if (BlueCrystal){
            CrystalRenderer.sprite = BlueCrystalSprite;
        }

    }
    public bool CrystalHitCheck(string direction){
        if (RedCrystal) {
            if (!GameManager.instance.Player1Turn){
                switch (direction){
                    case "SEDirection":
                        CrystalHit = "SEDirection";
                        return false;
                    break;
                    case "SWDirection":
                        CrystalHit = "SWDirection";
                        return false;
                    break;
                    case "NEDirection":
                        CrystalHit = "NEDirection";
                        return false;
                    break;
                    case "NWDirection":
                        CrystalHit = "NWDirection";
                        return false;
                    break;
                    case "NDirection":
                        CrystalHit = "NDirection";
                        return false;
                    break;
                    case "SDirection":
                        CrystalHit = "SDirection";
                        return false;
                    break;
                }
                return false;
            }
        }
        if (BlueCrystal) {
            if (GameManager.instance.Player1Turn){
                switch (direction){
                    case "SEDirection":
                        CrystalHit = "SEDirection";
                        return false;
                    break;
                    case "SWDirection":
                        CrystalHit = "SWDirection";
                        return false;
                    break;
                    case "NEDirection":
                        CrystalHit = "NEDirection";
                        return false;
                    break;
                    case "NWDirection":
                        CrystalHit = "NWDirection";
                        return false;
                    break;
                    case "NDirection":
                        CrystalHit = "NDirection";
                        return false;
                    break;
                    case "SDirection":
                        CrystalHit = "SDirection";
                        return false;
                    break;
                }
                return false;
            }
        }
        return true;
    }
    public bool ShieldCheck(string direction){
        switch (direction){
            case "SEDirection":
            if (SouthEastShield){
                return false;
            }
            break;
            case "SWDirection":
            if (SouthWestShield){
                return false;
            }
            break;
            case "NEDirection":
            if (NorthEastShield){
                return false;
            }
            break;
            case "NWDirection":
            if (NorthWestShield){
                return false;
            }
            break;
            case "NDirection":
            if (NorthShield){
                return false;
            }
            break;
            case "SDirection":
            if (SouthShield){
                return false;
            }
            break;
        }
        return true;
    }


    //getters and setters for the curTurn int variable
    public void SetTurn()
    {
        curTurn = TurnBasedSystem.turnNumber;
    }

    public int GetTurn()
    {
        return curTurn;
    }

    //we are going to check if a big rock was used
    void OnTriggerStay(Collider collided)
    {

        //check to see if a big rock was used
        if(usedBigRock == true)
        {
            
     
           collided.gameObject.GetComponent<Tile>().usedRock = true;
            

            usedBigRock = false;
        }
        
    }

    public void ShieldsOn(){
        if (RedCrystal) {
            if (!GameManager.instance.Player1Turn){
                CrystalRenderer.sprite = RedHitCrystalSprite;
                switch (CrystalHit){
                    case "SEDirection":
                        SouthEastShield = true;
                        SouthShield = true;
                        NorthEastShield = true;
                        SouthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                    break;
                    case "SWDirection":
                        SouthWestShield = true;
                        SouthShield = true;
                        NorthWestShield = true;
                        SouthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                    break;
                    case "NEDirection":
                        NorthEastShield = true;
                        NorthShield = true;
                        SouthEastShield = true;
                        NorthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                    break;
                    case "NWDirection":
                        NorthWestShield = true;
                        NorthShield = true;
                        SouthWestShield = true;
                        NorthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                    break;
                    case "NDirection":
                        NorthShield = true;
                        NorthEastShield = true;
                        NorthWestShield = true;
                        NorthWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                    break;
                    case "SDirection":
                        SouthShield = true;
                        SouthWestShield = true;
                        SouthEastShield = true;
                        SouthWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                    break;
                }
            }
        }
        if (BlueCrystal){
            Debug.Log("Test");
            if (GameManager.instance.Player1Turn){
                switch (CrystalHit){
                    case "SEDirection":
                        SouthEastShield = true;
                        SouthShield = true;
                        NorthEastShield = true;
                        SouthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                    break;
                    case "SWDirection":
                        SouthWestShield = true;
                        SouthShield = true;
                        NorthWestShield = true;
                        SouthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                    break;
                    case "NEDirection":
                        NorthEastShield = true;
                        NorthShield = true;
                        SouthEastShield = true;
                        NorthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                    break;
                    case "NWDirection":
                        Debug.Log("AAAAAAAA");
                        NorthWestShield = true;
                        NorthShield = true;
                        SouthWestShield = true;
                        NorthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                       // NorthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        //SouthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        //SouthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                    break;
                    case "NDirection":
                        NorthShield = true;
                        NorthEastShield = true;
                        NorthWestShield = true;
                        NorthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                    break;
                    case "SDirection":
                        SouthShield = true;
                        SouthWestShield = true;
                        SouthEastShield = true;
                        SouthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                    break;
                }
            }
        }
    }
}