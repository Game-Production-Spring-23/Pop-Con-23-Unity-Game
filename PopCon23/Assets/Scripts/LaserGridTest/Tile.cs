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
    public bool initSpawn = false;
    public int blockerTurn;
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

    // Crystal Bools
    public bool RedCrystal;
    public bool BlueCrystal;
    private bool RedHasBeenHit;
    private bool BlueHasBeenHit;

    public string CrystalHit;

    //method to check which turn the player is currently on
    private int curTurn = 1;

    void Start()
    {
    }

    void Update()
    {
        //remove the blocker after a set amount of turns
        if (hasBlocker == true && ((TurnBasedSystem.turnNumber - blockerTurn) > 2))
        {
            hasBlocker = false;
            MirrorStage9Blocker = false;
            MirrorStage9BlockerObject.SetActive(false);
            Debug.Log("Removing the blocker!");
            GameManager.instance.LaserFire();


        }
    }

    // StartLaser fires the lasers from an entry direction centered on this tile
    public void StartLaser(Material player, string direction)
    {   /*  
            Switch Statment checks which direction the laser is coming into. 
            First statement blocks laser from spawning if shield is active, laser then turns on and checks if tile is a crystal
            If there is no crystal third check looks for a mirror on the tile, if so runs laser reflect to figure out which direction laser goes
            If no mirror laser will continue and call the next tile to continue the path
        */
        switch (direction){
            case "SEDirection":
                if (!ShieldCheck(direction)){
                    return;
                }
                LaserTurnOn(2, player);
                if (!CrystalHitCheck(direction)){
                    return;
                }
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

    //Tells Tile Next door according to where the laser comes out to start their laser script, if no tile cancels out to prevent errors.
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
        /*
            LaserReflect script has two checks.
            The first check uses the direction previous called upon in StartLaser from 0 to 5 going clockwise from north
            The second check checks all the mirror bools to then turn on the reflection direction and call the tile in the new direction to continue the laser
        */
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
                    LaserTurnOn(0, player);
                    ContinueLaser(0, 2, player, "SDirection");
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
        // If the laser already has a laser active, turn the laser to the crossbeams laser color
        if (paths[entryPoint].activeSelf == true) {
            paths[entryPoint].GetComponent<Renderer>().material = Crossbeams;
        }
        // If the laser does not already have a laser active, turn on the laser to players color
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
            MirrorChange.Tile = this;
            TurnBasedSystem.isHovering = true;
            initSpawn = true;

        }

        //in the case where the player is trying to drop either a rock or a rotator over an existing item
        else if(!Input.GetMouseButton(0) && TurnBasedSystem.draggingItem == true && (TurnBasedSystem.currentItem == 4 || TurnBasedSystem.currentItem == 5 || TurnBasedSystem.currentItem == 7))
        {

            TurnBasedSystem.curTile = this;

            TurnBasedSystem.isHovering = true;

        }

    }

    
    public void CrystalGeneration(){
        // If crystal has crystal boolean (declared in gamemanager start script) sets the renderer sprite to the color stated.
        if (RedCrystal){
            CrystalRenderer.sprite = RedCrystalSprite;
        }
        if (BlueCrystal){
            CrystalRenderer.sprite = BlueCrystalSprite;
        }

    }
    public bool CrystalHitCheck(string direction){
        /*
            Crystal Hit Check is checked when the laser is called upon in StartLaser, if either boolean is true sets string varible to direction
        */
        if (RedCrystal || BlueCrystal) {
                switch (direction){
                    case "SEDirection":
                        CrystalHit = "SEDirection";
                    break;
                    case "SWDirection":
                        CrystalHit = "SWDirection";
                    break;
                    case "NEDirection":
                        CrystalHit = "NEDirection";
                    break;
                    case "NWDirection":
                        CrystalHit = "NWDirection";
                    break;
                    case "NDirection":
                        CrystalHit = "NDirection";
                    break;
                    case "SDirection":
                        CrystalHit = "SDirection";
                    break;
                }
                return false;
        }
        return true;
    }
    public bool ShieldCheck(string direction){
        /*
            Shield Check checks the laser coming in and if the shield booleans are on returns so the laser doesn't pass through.
        */
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
        /*
            Shields On Script is called when the End Turn button is clicked in ConfirmButton.cs
            Script checks if string CrystalHit variable which was set in CrystalHitCheck
            It then checks if the oncoming laser color is the opposite player to prevent friendly fire.
            If opposite player, the tile will turn the walls where the laser is, and one clockwise and counter clockwise to the players color
            and turns on the booleans for the shields
        */
        if (RedCrystal) {
                switch (CrystalHit){
                    case "SEDirection":
                        if (paths[2].GetComponent<Renderer>().sharedMaterial == Player2Color) {
                        SouthEastShield = true;
                        SouthShield = true;
                        NorthEastShield = true;
                        SouthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                        CrystalRenderer.sprite = RedHitCrystalSprite;
                        }
                    break;
                    case "SWDirection":
                        if (paths[4].GetComponent<Renderer>().sharedMaterial == Player2Color) {
                        SouthWestShield = true;
                        SouthShield = true;
                        NorthWestShield = true;
                        SouthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                        CrystalRenderer.sprite = RedHitCrystalSprite;
                        }
                    break;
                    case "NEDirection":
                        if (paths[1].GetComponent<Renderer>().sharedMaterial == Player2Color) {
                        NorthEastShield = true;
                        NorthShield = true;
                        SouthEastShield = true;
                        NorthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                        CrystalRenderer.sprite = RedHitCrystalSprite;
                        }
                    break;
                    case "NWDirection":
                        if (paths[5].GetComponent<Renderer>().sharedMaterial == Player2Color) {
                        NorthWestShield = true;
                        NorthShield = true;
                        SouthWestShield = true;
                        NorthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                        CrystalRenderer.sprite = RedHitCrystalSprite;
                        }
                    break;
                    case "NDirection":
                        if (paths[0].GetComponent<Renderer>().sharedMaterial == Player2Color) {
                        NorthShield = true;
                        NorthEastShield = true;
                        NorthWestShield = true;
                        NorthWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                        NorthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                        CrystalRenderer.sprite = RedHitCrystalSprite;
                        }
                    break;
                    case "SDirection":
                        if (paths[3].GetComponent<Renderer>().sharedMaterial == Player2Color) {
                        SouthShield = true;
                        SouthWestShield = true;
                        SouthEastShield = true;
                        SouthWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthWestWall.GetComponent<Renderer>().material = RedShieldColor;
                        SouthEastWall.GetComponent<Renderer>().material = RedShieldColor;
                        CrystalRenderer.sprite = RedHitCrystalSprite;
                        }
                    break;
            }
        }
        if (BlueCrystal){
                switch (CrystalHit){
                    case "SEDirection":
                        if (paths[2].GetComponent<Renderer>().sharedMaterial == Player1Color) {
                        SouthEastShield = true;
                        SouthShield = true;
                        NorthEastShield = true;
                        SouthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                        }
                    break;
                    case "SWDirection":
                        if (paths[4].GetComponent<Renderer>().sharedMaterial == Player1Color) {
                        SouthWestShield = true;
                        SouthShield = true;
                        NorthWestShield = true;
                        SouthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                        }
                    break;
                    case "NEDirection":
                        if (paths[1].GetComponent<Renderer>().sharedMaterial == Player1Color) {
                        NorthEastShield = true;
                        NorthShield = true;
                        SouthEastShield = true;
                        NorthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                        }
                    break;
                    case "NWDirection":
                        if (paths[5].GetComponent<Renderer>().sharedMaterial == Player1Color) {
                        NorthWestShield = true;
                        NorthShield = true;
                        SouthWestShield = true;
                        NorthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                        }
                    break;
                    case "NDirection":
                        if (paths[0].GetComponent<Renderer>().sharedMaterial == Player1Color) {
                        NorthShield = true;
                        NorthEastShield = true;
                        NorthWestShield = true;
                        NorthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        NorthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                        }
                    break;
                    case "SDirection":
                        if (paths[3].GetComponent<Renderer>().sharedMaterial == Player1Color) {
                        SouthShield = true;
                        SouthWestShield = true;
                        SouthEastShield = true;
                        SouthWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthWestWall.GetComponent<Renderer>().material = BlueShieldColor;
                        SouthEastWall.GetComponent<Renderer>().material = BlueShieldColor;
                        CrystalRenderer.sprite = BlueHitCrystalSprite;
                        }
                    break;
            }
        }
    }


}