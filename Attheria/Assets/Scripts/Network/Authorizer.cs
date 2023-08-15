using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Steamworks;
using UnityEngine;
using MainMenu;

namespace Network
{
    public class Authorizer : NetworkAuthenticator
    {
        readonly HashSet<NetworkConnection> connectionsPendingDisconnect = new HashSet<NetworkConnection>();

        #region Server

        public override void OnStartServer()
        {
            NetworkServer.RegisterHandler<AuthRequestMessage>(OnAuthRequestMessage, false);
            NetworkServer.RegisterHandler<AuthPasswordRequestMessage>(OnAuthPasswordRequestMessage, false);
        }

        public override void OnStopServer()
        {
            NetworkServer.UnregisterHandler<AuthRequestMessage>();
            NetworkServer.UnregisterHandler<AuthPasswordRequestMessage>();
        }

        public override void OnServerAuthenticate(NetworkConnectionToClient conn)
        {
            // do nothing...wait for AuthRequestMessage from client
        }

        public void OnAuthRequestMessage(NetworkConnectionToClient conn, AuthRequestMessage msg)
        {
            if (connectionsPendingDisconnect.Contains(conn)) return;

            if (!checkId(msg.SteamId)) //Check if id is not 0
            {
                connectionsPendingDisconnect.Add(conn);
                AuthResponseMessage mess = new()
                {
                    Code = 10,
                    Message = "Steam error"
                };
                conn.Send(mess);
                conn.isAuthenticated = false;
                StartCoroutine(DelayedDisconnect(conn, 1f));
                return;
            }

            if (!checkVersion(msg.GameVersion)) //Check game version
            {
                connectionsPendingDisconnect.Add(conn);
                AuthResponseMessage mess = new()
                {
                    Code = 60,
                    Message = "Game version mismatch"
                };
                conn.Send(mess);
                conn.isAuthenticated = false;
                StartCoroutine(DelayedDisconnect(conn, 1f));
                return;
            }

            if (!checkDLC(msg.SteamId)) //Check user installed DLC
            {
                connectionsPendingDisconnect.Add(conn);
                AuthResponseMessage mess = new()
                {
                    Code = 70,
                    Message = "DLC not found"
                };
                conn.Send(mess);
                conn.isAuthenticated = false;
                StartCoroutine(DelayedDisconnect(conn, 1f));
                return;
            }

            if (!checkSlots(msg.SteamId)) //Check server slots
            {
                connectionsPendingDisconnect.Add(conn);
                AuthResponseMessage mess = new()
                {
                    Code = 20,
                    Message = "Server is full"
                };
                conn.Send(mess);
                conn.isAuthenticated = false;
                StartCoroutine(DelayedDisconnect(conn, 1f));
                return;
            }


            if (ServerManager.Instance == null) //Game is hosted through steam
            {
                AuthResponseMessage authMessage = new()
                {
                    Code = 1,
                    Message = "Accepted"
                };
                conn.Send(authMessage);
                conn.steamId = msg.SteamId;
                ServerAccept(conn);
                return;
            }

            if (checkBan(msg.SteamId)) //Check user ban
            {
                connectionsPendingDisconnect.Add(conn);
                AuthResponseMessage authMessage = new()
                {
                    Code = 30,
                    Message = "You have been banned from this server"
                };
                conn.Send(authMessage);
                conn.isAuthenticated = false;
                StartCoroutine(DelayedDisconnect(conn, 1f));
                return;
            }

            if (!checkWhitelist(msg.SteamId)) //Check user whitelist
            {
                connectionsPendingDisconnect.Add(conn);
                AuthResponseMessage authMessage = new()
                {
                    Code = 40,
                    Message = "You are not whitelisted on this server"
                };
                conn.Send(authMessage);
                conn.isAuthenticated = false;
                StartCoroutine(DelayedDisconnect(conn, 1f));
                return;
            }

            AuthPasswordResponseMessage message = new();
            if (isServerPasswordProtected()) conn.Send(message); //Send client password request
            else
            {
                AuthResponseMessage authMessage = new()
                {
                    Code = 1,
                    Message = "Accepted"
                };
                conn.Send(authMessage);
                conn.steamId = msg.SteamId;
                Debug.Log($"User with steam id: {msg.SteamId} has successfully connected to the server!");
                ServerAccept(conn);
            }
        }

        public void OnAuthPasswordRequestMessage(NetworkConnectionToClient conn, AuthPasswordRequestMessage msg)
        {
            if (connectionsPendingDisconnect.Contains(conn)) return;
            if (checkPassword(msg.Password))
            {
                Debug.Log($"User with steam id: {msg.SteamId} has successfully connected to the server!");
                AuthResponseMessage message = new()
                {
                    Code = 1,
                    Message = "Accepted"
                };
                conn.Send(message);
                ServerAccept(conn);
            }
            else
            {
                connectionsPendingDisconnect.Add(conn);
                AuthResponseMessage message = new()
                {
                    Code = 50,
                    Message = "Wrong password"
                };
                conn.Send(message);
                conn.isAuthenticated = false;
                StartCoroutine(DelayedDisconnect(conn, 1f));
            }
        }

        bool checkId(ulong id)
        {
            return id != 0;
        }

        bool checkSlots(ulong id)
        {
            if (NetworkServer.connections.Count >= NetworkServer.maxConnections) return false; //Server is at max connections
            if (GameConfigManager.Instance.VIPIds.Contains(id) && NetworkServer.connections.Count >= GameConfigManager.Instance.ServerSettings.MaxPlayersVIP) return false; //Server full for VIP players
            if (GameConfigManager.Instance.VIPIds.Contains(id) && NetworkServer.connections.Count <= GameConfigManager.Instance.ServerSettings.MaxPlayersVIP) return true; //Server is full for non VIP players
            if (!GameConfigManager.Instance.VIPIds.Contains(id) && NetworkServer.connections.Count >= GameConfigManager.Instance.ServerSettings.MaxPlayers) return false; //Server full for normal players
            return true; //Server is empty
        }

        bool checkBan(ulong id) => GameConfigManager.Instance.BannedIds.Contains(id);

        bool checkWhitelist(ulong id) => GameConfigManager.Instance.ServerSettings.Whitelist && GameConfigManager.Instance.WhitelistedIds.Contains(id);

        bool isServerPasswordProtected() => GameConfigManager.Instance.ServerSettings.Password != string.Empty;
        bool checkPassword(string password) => GameConfigManager.Instance.ServerSettings.Password == password;

        bool checkVersion(string version) => GameManager.Instance.GameVersion == version;

        bool checkDLC(ulong id) => true;

        IEnumerator DelayedDisconnect(NetworkConnectionToClient conn, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            ServerReject(conn);

            yield return null;

            connectionsPendingDisconnect.Remove(conn);
        }

        #endregion


        #region Client

        public override void OnStartClient()
        {
            NetworkClient.RegisterHandler<AuthResponseMessage>(OnAuthResponseMessage, false);
            NetworkClient.RegisterHandler<AuthPasswordResponseMessage>(OnAuthPasswordResponseMessage, false);
        }

        public override void OnStopClient()
        {
            NetworkClient.UnregisterHandler<AuthResponseMessage>();
            NetworkClient.UnregisterHandler<AuthPasswordResponseMessage>();
        }

        public override void OnClientAuthenticate()
        {
            if (MainMenuUIManager.Instance.DevInp)
            {
                AuthRequestMessage msg = new()
                {
                    SteamId = ulong.Parse(MainMenuUIManager.Instance.DevInp.text),
                    GameVersion = GameManager.Instance.GameVersion
                };

                NetworkClient.Send(msg);
                return;
            }
            
            
            if (!SteamManager.Initialized) return;

            AuthRequestMessage message = new()
            {
                SteamId = (ulong)SteamUser.GetSteamID(),
                GameVersion = GameManager.Instance.GameVersion
            };

            NetworkClient.Send(message);
        }

        public void OnAuthResponseMessage(AuthResponseMessage msg)
        {
            switch (msg.Code)
            {
                case 0:
                    MainMenuUIManager.Instance.ResponseMessageInstance.SetActive(true);
                    MainMenuUIManager.Instance.ResponseMessage.Message.text = msg.Message;
                    MainMenuUIManager.Instance.ResponseMessage.Code.text = msg.Code.ToString();
                    ClientReject();
                    break;
                case 1:
                    ClientAccept();
                    break;
                default:
                    MainMenuUIManager.Instance.ResponseMessageInstance.SetActive(true);
                    MainMenuUIManager.Instance.ResponseMessage.Message.text = msg.Message;
                    MainMenuUIManager.Instance.ResponseMessage.Code.text = msg.Code.ToString();
                    ClientReject();
                    break;
            }
        }

        public void OnAuthPasswordResponseMessage(AuthPasswordResponseMessage msg)
        {
            MainMenuUIManager.Instance.ServerPasswordInstance.SetActive(true);
            MainMenuUIManager.Instance.ServerPassword.OnPassword += handlePasswordMessage;
        }

        void handlePasswordMessage(string password)
        {
            if (!SteamManager.Initialized) return;
            MainMenuUIManager.Instance.ServerPassword.OnPassword -= handlePasswordMessage;
            AuthPasswordRequestMessage message = new()
            {
                Password = password,
                SteamId = (ulong)SteamUser.GetSteamID()
            };

            NetworkClient.Send(message);
        }

        #endregion


        #region Messages

        public struct AuthRequestMessage : NetworkMessage
        {
            public ulong SteamId;
            public string GameVersion;
        }

        public struct AuthPasswordRequestMessage : NetworkMessage
        {
            public ulong SteamId;
            public string Password;
        }

        public struct AuthPasswordResponseMessage : NetworkMessage
        {
        }

        public struct AuthResponseMessage : NetworkMessage
        {
            public byte Code;
            public string Message;
        }

        #endregion
    }
}