﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;
using Steamworks;
using System.Diagnostics;

public class SteamLobby : MonoBehaviour
{
    [SerializeField] private GameObject button = null;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;
    protected CallResult<LobbyMatchList_t> m_LobbyMatchListCallResult;

    //protected LobbyMetaData[] m_Data;

    private const string HostAddressKey = "HostAddress";
    
    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();

        if (!SteamManager.Initialized) { return; }

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        m_LobbyMatchListCallResult = CallResult<LobbyMatchList_t>.Create(OnLobbyMatchList);

    }

    public void HostLobby()
    {
        //Deactivate the host button so they can't press it again
        button.SetActive(false);
        //Create a lobby only friends can join with a max number of players, from the network manager
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, networkManager.maxConnections);
    }

    public void JoinLobby()
    {
        UnityEngine.Debug.Log("Search for lobbies");
        for (int i = 1; i < networkManager.maxConnections; ++i)
        {
            SteamMatchmaking.AddRequestLobbyListFilterSlotsAvailable(i);
            m_LobbyMatchListCallResult.Set(SteamMatchmaking.RequestLobbyList());
        }
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK)
        {
            button.SetActive(true);
            return;
        }

        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) { return; }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();

        button.SetActive(false);
    }

    void OnLobbyMatchList(LobbyMatchList_t pCallback, bool bIOFailure)
    {
        
        UnityEngine.Debug.Log(pCallback.m_nLobbiesMatching);
        if (pCallback.m_nLobbiesMatching > 0)
        {
            //UnityEngine.Debug.Log(SteamMatchmaking.GetLobbyByIndex(0));
            SteamMatchmaking.JoinLobby(SteamMatchmaking.GetLobbyByIndex(0));
        }
    }
}
