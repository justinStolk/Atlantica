using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform backpack;
    [SerializeField] private GameObject gameplayCam;


    // Start is called before the first frame update
    void Start()
    {
        EventSystem.SubscribeEvent(EventSystem.EventType.ON_GAME_START, StartGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveGameData()
    {
        SaveSystem.SaveData(new GameData(player.position, player.eulerAngles, backpack.position, backpack.eulerAngles));
    }
    
    public void LoadGameData()
    {
        //This one will probably be removed, as loading data while the game isn't started yet won't be possible through the Game Manager.
        //However, for debugging purposes, this is temporarily placed here.

        GameData loadedData = LoadSystem.LoadData();
        player.position = loadedData.playerPosition;
        player.rotation = Quaternion.Euler(loadedData.playerRotation);

        backpack.position = loadedData.backpackPosition;
        backpack.rotation = Quaternion.Euler(loadedData.backpackRotation);

    }

    public void StartGame()
    {
        gameplayCam.SetActive(true);
        EventSystem.UnsubscribeEvent(EventSystem.EventType.ON_GAME_START, StartGame);
    }

}
