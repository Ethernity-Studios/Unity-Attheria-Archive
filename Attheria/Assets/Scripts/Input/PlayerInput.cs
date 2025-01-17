//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/Scripts/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""13654693-96fe-4bf3-ab02-68217a53be53"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""c419ce50-8553-42e6-a785-4c3d8b9447ce"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""06d3bd94-4b66-48fb-8fd3-712f84dd87d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""9e8f163e-289a-4980-8817-a0831a88ef13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""eb88de8d-acf3-48af-8f62-b0d05e9c4cbc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crawl"",
                    ""type"": ""Button"",
                    ""id"": ""7aea66b8-2a19-4807-a0b3-fa17a674665f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7c5afaf7-4b0d-4ea9-97e2-c2d51665d2bb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""559b39b1-1d1e-457e-a1bd-62732163fedf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9d093ddc-9006-4b79-92c6-d2f0693f1983"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d51ddbaf-0a91-4d40-bfe6-d26a5fd0abd9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8198955d-3351-45f4-9992-b1572c12b441"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f30a9abc-633f-41f6-95eb-660dd2a605ec"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Hold(duration=1.401298E-45,pressPoint=1.401298E-45)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87569929-e5a1-4067-96db-6feafc144ebd"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a2c2617-2ec8-4d02-a679-dcdc24c79740"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1343422-d2f0-4db1-9608-a9fbcedbbb52"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crawl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""3f1a018e-cccf-4d22-8f5a-386147395072"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""3f0f0275-1720-42c5-a9d0-66f232ded2d7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ToggleView"",
                    ""type"": ""Value"",
                    ""id"": ""71b51cde-c69e-4e6d-8adf-c7ae1f160125"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ToggleMode"",
                    ""type"": ""Button"",
                    ""id"": ""59a1e6e9-8acb-4970-ada5-6c0dd0c43eae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a2b6ecf3-b476-4276-b8f5-10c6bb3a0163"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af929a9e-9c16-43ab-8624-927ada04f9cd"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""d8090d64-ea33-43d5-8c7c-a365977218ab"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-1,max=1)"",
                    ""groups"": """",
                    ""action"": ""ToggleView"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""818cbc85-280a-44fe-8cc6-e29184ca78a6"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleView"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""31f6ff63-c115-4bf1-b076-ea41e1dfd88e"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleView"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Console"",
            ""id"": ""cf63e2a1-7e85-4966-a9b6-baa7f6eb866a"",
            ""actions"": [
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""95ea0078-50b0-4eb3-9471-d3c1fcc2ecb2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6e8078e7-9dcb-4da0-8074-2d6927366db3"",
                    ""path"": ""<Keyboard>/f9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MainMenu"",
            ""id"": ""1ec66fe5-9996-4ca0-985d-750c5c042e93"",
            ""actions"": [
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""b122704a-e74d-42da-acc1-adcccdbef791"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""25a29ef0-7d0e-4e7a-9749-95561b20c6c2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""192e29ee-626a-4b84-b033-d3c72eee1f70"",
            ""actions"": [
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""1fde1fa7-55cc-49b5-ba8e-a3aa6923c09d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3583822d-fb59-4fae-bcb4-8e6a8158b98a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fb61178-bb42-4405-a292-226e43358cc6"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CharacterCreator"",
            ""id"": ""7de01c52-dc1d-4350-9950-a284afc3e20e"",
            ""actions"": [
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9b389ea1-9822-41c9-8a1b-25653e975025"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""e5c5e494-24d5-4758-ac14-144d215234fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""b129c752-f152-4028-861e-0e19cfdb63b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ba2d488b-832c-4adf-916d-7da8b3fb303f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""454455c9-69b4-40fb-a3b8-69978edf0229"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad36877d-8613-43ad-850f-beaf715d708a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8649904-921c-4112-bd43-ade641712568"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0fc2232-f0e6-4bd9-90df-18252b7309ed"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e722450b-1df9-4f6d-b297-f97c4b7cc987"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMovement_Sprint = m_PlayerMovement.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerMovement_Crouch = m_PlayerMovement.FindAction("Crouch", throwIfNotFound: true);
        m_PlayerMovement_Crawl = m_PlayerMovement.FindAction("Crawl", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Look = m_Camera.FindAction("Look", throwIfNotFound: true);
        m_Camera_ToggleView = m_Camera.FindAction("ToggleView", throwIfNotFound: true);
        m_Camera_ToggleMode = m_Camera.FindAction("ToggleMode", throwIfNotFound: true);
        // Console
        m_Console = asset.FindActionMap("Console", throwIfNotFound: true);
        m_Console_Toggle = m_Console.FindAction("Toggle", throwIfNotFound: true);
        // MainMenu
        m_MainMenu = asset.FindActionMap("MainMenu", throwIfNotFound: true);
        m_MainMenu_Toggle = m_MainMenu.FindAction("Toggle", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Toggle = m_Menu.FindAction("Toggle", throwIfNotFound: true);
        // CharacterCreator
        m_CharacterCreator = asset.FindActionMap("CharacterCreator", throwIfNotFound: true);
        m_CharacterCreator_Point = m_CharacterCreator.FindAction("Point", throwIfNotFound: true);
        m_CharacterCreator_Rotate = m_CharacterCreator.FindAction("Rotate", throwIfNotFound: true);
        m_CharacterCreator_Drag = m_CharacterCreator.FindAction("Drag", throwIfNotFound: true);
        m_CharacterCreator_Zoom = m_CharacterCreator.FindAction("Zoom", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private List<IPlayerMovementActions> m_PlayerMovementActionsCallbackInterfaces = new List<IPlayerMovementActions>();
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Jump;
    private readonly InputAction m_PlayerMovement_Sprint;
    private readonly InputAction m_PlayerMovement_Crouch;
    private readonly InputAction m_PlayerMovement_Crawl;
    public struct PlayerMovementActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerMovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
        public InputAction @Sprint => m_Wrapper.m_PlayerMovement_Sprint;
        public InputAction @Crouch => m_Wrapper.m_PlayerMovement_Crouch;
        public InputAction @Crawl => m_Wrapper.m_PlayerMovement_Crawl;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @Crawl.started += instance.OnCrawl;
            @Crawl.performed += instance.OnCrawl;
            @Crawl.canceled += instance.OnCrawl;
        }

        private void UnregisterCallbacks(IPlayerMovementActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @Crawl.started -= instance.OnCrawl;
            @Crawl.performed -= instance.OnCrawl;
            @Crawl.canceled -= instance.OnCrawl;
        }

        public void RemoveCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private List<ICameraActions> m_CameraActionsCallbackInterfaces = new List<ICameraActions>();
    private readonly InputAction m_Camera_Look;
    private readonly InputAction m_Camera_ToggleView;
    private readonly InputAction m_Camera_ToggleMode;
    public struct CameraActions
    {
        private @PlayerInput m_Wrapper;
        public CameraActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Camera_Look;
        public InputAction @ToggleView => m_Wrapper.m_Camera_ToggleView;
        public InputAction @ToggleMode => m_Wrapper.m_Camera_ToggleMode;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void AddCallbacks(ICameraActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @ToggleView.started += instance.OnToggleView;
            @ToggleView.performed += instance.OnToggleView;
            @ToggleView.canceled += instance.OnToggleView;
            @ToggleMode.started += instance.OnToggleMode;
            @ToggleMode.performed += instance.OnToggleMode;
            @ToggleMode.canceled += instance.OnToggleMode;
        }

        private void UnregisterCallbacks(ICameraActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @ToggleView.started -= instance.OnToggleView;
            @ToggleView.performed -= instance.OnToggleView;
            @ToggleView.canceled -= instance.OnToggleView;
            @ToggleMode.started -= instance.OnToggleMode;
            @ToggleMode.performed -= instance.OnToggleMode;
            @ToggleMode.canceled -= instance.OnToggleMode;
        }

        public void RemoveCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Console
    private readonly InputActionMap m_Console;
    private List<IConsoleActions> m_ConsoleActionsCallbackInterfaces = new List<IConsoleActions>();
    private readonly InputAction m_Console_Toggle;
    public struct ConsoleActions
    {
        private @PlayerInput m_Wrapper;
        public ConsoleActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Toggle => m_Wrapper.m_Console_Toggle;
        public InputActionMap Get() { return m_Wrapper.m_Console; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ConsoleActions set) { return set.Get(); }
        public void AddCallbacks(IConsoleActions instance)
        {
            if (instance == null || m_Wrapper.m_ConsoleActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ConsoleActionsCallbackInterfaces.Add(instance);
            @Toggle.started += instance.OnToggle;
            @Toggle.performed += instance.OnToggle;
            @Toggle.canceled += instance.OnToggle;
        }

        private void UnregisterCallbacks(IConsoleActions instance)
        {
            @Toggle.started -= instance.OnToggle;
            @Toggle.performed -= instance.OnToggle;
            @Toggle.canceled -= instance.OnToggle;
        }

        public void RemoveCallbacks(IConsoleActions instance)
        {
            if (m_Wrapper.m_ConsoleActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IConsoleActions instance)
        {
            foreach (var item in m_Wrapper.m_ConsoleActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ConsoleActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ConsoleActions @Console => new ConsoleActions(this);

    // MainMenu
    private readonly InputActionMap m_MainMenu;
    private List<IMainMenuActions> m_MainMenuActionsCallbackInterfaces = new List<IMainMenuActions>();
    private readonly InputAction m_MainMenu_Toggle;
    public struct MainMenuActions
    {
        private @PlayerInput m_Wrapper;
        public MainMenuActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Toggle => m_Wrapper.m_MainMenu_Toggle;
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void AddCallbacks(IMainMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MainMenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainMenuActionsCallbackInterfaces.Add(instance);
            @Toggle.started += instance.OnToggle;
            @Toggle.performed += instance.OnToggle;
            @Toggle.canceled += instance.OnToggle;
        }

        private void UnregisterCallbacks(IMainMenuActions instance)
        {
            @Toggle.started -= instance.OnToggle;
            @Toggle.performed -= instance.OnToggle;
            @Toggle.canceled -= instance.OnToggle;
        }

        public void RemoveCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MainMenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainMenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainMenuActions @MainMenu => new MainMenuActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private List<IMenuActions> m_MenuActionsCallbackInterfaces = new List<IMenuActions>();
    private readonly InputAction m_Menu_Toggle;
    public struct MenuActions
    {
        private @PlayerInput m_Wrapper;
        public MenuActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Toggle => m_Wrapper.m_Menu_Toggle;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void AddCallbacks(IMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MenuActionsCallbackInterfaces.Add(instance);
            @Toggle.started += instance.OnToggle;
            @Toggle.performed += instance.OnToggle;
            @Toggle.canceled += instance.OnToggle;
        }

        private void UnregisterCallbacks(IMenuActions instance)
        {
            @Toggle.started -= instance.OnToggle;
            @Toggle.performed -= instance.OnToggle;
            @Toggle.canceled -= instance.OnToggle;
        }

        public void RemoveCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // CharacterCreator
    private readonly InputActionMap m_CharacterCreator;
    private List<ICharacterCreatorActions> m_CharacterCreatorActionsCallbackInterfaces = new List<ICharacterCreatorActions>();
    private readonly InputAction m_CharacterCreator_Point;
    private readonly InputAction m_CharacterCreator_Rotate;
    private readonly InputAction m_CharacterCreator_Drag;
    private readonly InputAction m_CharacterCreator_Zoom;
    public struct CharacterCreatorActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterCreatorActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Point => m_Wrapper.m_CharacterCreator_Point;
        public InputAction @Rotate => m_Wrapper.m_CharacterCreator_Rotate;
        public InputAction @Drag => m_Wrapper.m_CharacterCreator_Drag;
        public InputAction @Zoom => m_Wrapper.m_CharacterCreator_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_CharacterCreator; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterCreatorActions set) { return set.Get(); }
        public void AddCallbacks(ICharacterCreatorActions instance)
        {
            if (instance == null || m_Wrapper.m_CharacterCreatorActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CharacterCreatorActionsCallbackInterfaces.Add(instance);
            @Point.started += instance.OnPoint;
            @Point.performed += instance.OnPoint;
            @Point.canceled += instance.OnPoint;
            @Rotate.started += instance.OnRotate;
            @Rotate.performed += instance.OnRotate;
            @Rotate.canceled += instance.OnRotate;
            @Drag.started += instance.OnDrag;
            @Drag.performed += instance.OnDrag;
            @Drag.canceled += instance.OnDrag;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
        }

        private void UnregisterCallbacks(ICharacterCreatorActions instance)
        {
            @Point.started -= instance.OnPoint;
            @Point.performed -= instance.OnPoint;
            @Point.canceled -= instance.OnPoint;
            @Rotate.started -= instance.OnRotate;
            @Rotate.performed -= instance.OnRotate;
            @Rotate.canceled -= instance.OnRotate;
            @Drag.started -= instance.OnDrag;
            @Drag.performed -= instance.OnDrag;
            @Drag.canceled -= instance.OnDrag;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
        }

        public void RemoveCallbacks(ICharacterCreatorActions instance)
        {
            if (m_Wrapper.m_CharacterCreatorActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICharacterCreatorActions instance)
        {
            foreach (var item in m_Wrapper.m_CharacterCreatorActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CharacterCreatorActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CharacterCreatorActions @CharacterCreator => new CharacterCreatorActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnCrawl(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnToggleView(InputAction.CallbackContext context);
        void OnToggleMode(InputAction.CallbackContext context);
    }
    public interface IConsoleActions
    {
        void OnToggle(InputAction.CallbackContext context);
    }
    public interface IMainMenuActions
    {
        void OnToggle(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnToggle(InputAction.CallbackContext context);
    }
    public interface ICharacterCreatorActions
    {
        void OnPoint(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnDrag(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
}
