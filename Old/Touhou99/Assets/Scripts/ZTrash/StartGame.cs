//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;

//[System.Obsolete]
//public class StartGame : NetworkBehaviour
//{
//    private int numberOfPlayers;

//    [SerializeField] public GameObject arenaPrefab;
//    public static int arenaNumber = 1;

//    [SerializeField] private GameObject arena;

//    //[SerializeField]
//    //private GameObject StartGameButton;

//    [SerializeField] private int posX = 0;
//    [SerializeField] private int posY = 0;
//    void Start()
//    {
        
//    }

//    void Update()
//    {
        

//    }

//    public void SpawnArenas()
//    {
//        for (int i = 0; i < 5; i++)
//        {
//            for (int y = 0; y < 5; y++)
//            {

//                arena = Instantiate(arenaPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
//                arena.transform.name = "Arena" + arenaNumber;

//                arenaNumber += 1;
//                posX += 17;

//            }
//            posY += 20;
//            posX = 0;
//        }
//    }
//}
