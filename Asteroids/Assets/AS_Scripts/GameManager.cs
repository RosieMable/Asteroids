using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Prefabs
    [SerializeField]
    private GameObject BulletPrefab; //Link in IDE

    [SerializeField]
    private GameObject ShipPrefab; //Link in IDE

    [SerializeField]
    private GameObject[] RockPrefabs; //Link in IDE
    #endregion



    #region Singleton
    public static GameManager sGM; //Allows access to singleton
                                   //Being static means you can acess without knowing instance
    private void Awake()            //Runs before Start
    {
        if (sGM == null)            //Has it been set up before?
        {
            sGM = this;             //No, its the first Time creation of Game Manager, so store our instance
            DontDestroyOnLoad(gameObject); //Persist, now it will survive scene reloads
        } else if (sGM != this) //If we get called again, then destroy new version and keep old one
        {
            Destroy(gameObject); //Kill any subsequent one
        }
    }
    #endregion


    private void Start()
    {
        InitialiseGame();
    }

    #region InitialiseGame
    void InitialiseGame()
    {
        CreatePlayerShip();
        for(int tRockIndex = 0; tRockIndex < 5; tRockIndex++)
        {
            CreateRock(0);
        }
    }
    #endregion

    #region Ship Creation
    public static void CreatePlayerShip()
    {
        Debug.Assert(sGM.ShipPrefab != null, "ShipPrefab prefab not linked in IDE");
        Instantiate(sGM.ShipPrefab, Vector3.zero, Quaternion.identity);
    }

    #endregion



    #region Rock Control

    public static void CreateRock(int vIndex)
    {
        Debug.Assert(sGM.RockPrefabs != null, "RockPrefabs not linked in IDE");
        if (vIndex < sGM.RockPrefabs.Length)
        {
            Instantiate(sGM.RockPrefabs[vIndex], RandomScreenPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogFormat("RockPrefab INdex {0} out of range", vIndex);
        }
        }
    #endregion


    #region CreateBullet
    public static void CreateBullet(Vector3 vPosition, Vector3 vDirection)
    {
        Debug.Assert(sGM.BulletPrefab != null, "BulletPrefab not linked in IDE");
        Bullet tBullet = Instantiate(sGM.BulletPrefab, vPosition, Quaternion.identity).GetComponent<Bullet>();
        Debug.Assert(tBullet != null, "Bullet Script Missing");
        tBullet.FireBullet(vPosition, vDirection);
    }
    #endregion

    #region Utilities
    public static Vector3 RandomScreenPosition
    {
        get
        {
            float tHeight = Camera.main.orthographicSize; //Figure out what Camera can see
            float tWidth = Camera.main.aspect * tHeight; //Use aspect ration to work out Width
            return new Vector3(Random.Range(-tWidth, tWidth), Random.Range(-tHeight, tHeight), 0.0f);
        }
    }
    #endregion
}
   







