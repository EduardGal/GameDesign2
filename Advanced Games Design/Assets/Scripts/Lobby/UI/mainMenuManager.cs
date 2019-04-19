using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuManager : MonoBehaviour {

    public GameObject createGame, joinGame, lobbyView2, mainMenu, currentRoom;

	public void OnCreate()
    {
        createGame.SetActive(true);
        lobbyView2.SetActive(false);
        joinGame.transform.SetAsFirstSibling();
        
        // mainMenu.SetActive(false);
    }

    public void OnJoin()
    {
        createGame.SetActive(false);
        lobbyView2.SetActive(false);
        joinGame.transform.SetAsLastSibling();
    }

    public void OnBackFromCreate()
    {

        createGame.SetActive(false);
        lobbyView2.SetActive(true);
        joinGame.transform.SetAsFirstSibling();
    }

    public void OnBackFromJoin()
    {
        lobbyView2.SetActive(true);
        joinGame.transform.SetAsFirstSibling();
    }

    public void OnClickHome()
    {
        joinGame.transform.SetAsFirstSibling();
        createGame.SetActive(false);
        lobbyView2.SetActive(true);

    }

    public void OnClickExit()
    {
        
    }

}
