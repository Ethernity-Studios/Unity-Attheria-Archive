using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public static CharacterCreator Instance;

    [SerializeField] private GameObject CharacterCreation;
    [SerializeField] private GameObject ViewController;
    
    Character createdCharacter;

    [Header("Input")]
    [SerializeField] private TMP_InputField NameInput;

    private void Start()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    /// <summary>
    /// Opens character creator
    /// </summary>
    public void OpenCharacterCreator()
    {
        CharacterCreation.SetActive(true);
        ViewController.SetActive(true);
    }
    
    /// <summary>
    /// Closes character creator
    /// </summary>
    public void CloseCharacterCreator()
    {
        CharacterCreation.SetActive(false);
        ViewController.SetActive(false);
    }

    /// <summary>
    /// Creates new character with selected fields
    /// </summary>
    public void SaveCharacter()
    {
        string name = NameInput.text == string.Empty ? "Player" : NameInput.text;
        Character character = new()
        {
            Name = name
        };
        
        CharacterCreation.SetActive(false);
        ViewController.SetActive(false);
        
        CharacterRequest message = new()
        {
            Character = character
        };

        NetworkClient.Send(message);
    }

    private void OnEnable()
    {
        NetworkClient.RegisterHandler<CharacterResponse>(CharacterCreationResponse);
    }
    private void OnDisable()
    {
        NetworkClient.UnregisterHandler<CharacterResponse>();
    }

    void CharacterCreationResponse(CharacterResponse msg)
    {
        CloseCharacterCreator();

        PlayerSpawner.Instance.OpenSpawner(msg.UnlockedZones);
    }

    #region Messages

    public struct CharacterRequest : NetworkMessage
    {
        public Character Character;
    }
    
    public struct CharacterResponse : NetworkMessage
    {
        public List<int> UnlockedZones;
    }

    #endregion
}