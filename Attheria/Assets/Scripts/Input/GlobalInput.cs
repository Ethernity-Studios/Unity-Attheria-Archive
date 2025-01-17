//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/Scripts/Input/GlobalInput.inputactions
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

public partial class @GlobalInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GlobalInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GlobalInput"",
    ""maps"": [
        {
            ""name"": ""DebugMenu"",
            ""id"": ""8f306d06-42c5-481b-8e37-35d808de1c35"",
            ""actions"": [
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""13d3fcb7-d8ef-4b31-8faf-3d7af552e22b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e81f1180-163e-4f64-bd2f-333a20a8c0b2"",
                    ""path"": ""<Keyboard>/f8"",
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
            ""name"": ""BugReportMenu"",
            ""id"": ""c520d334-5bdf-4205-b826-604a89e4560c"",
            ""actions"": [
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""ad1110f9-a781-4bae-8087-cab3c4da0a03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""992bd049-1373-4a12-b5eb-814d5f0edc40"",
                    ""path"": ""<Keyboard>/f7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DebugMenu
        m_DebugMenu = asset.FindActionMap("DebugMenu", throwIfNotFound: true);
        m_DebugMenu_Toggle = m_DebugMenu.FindAction("Toggle", throwIfNotFound: true);
        // BugReportMenu
        m_BugReportMenu = asset.FindActionMap("BugReportMenu", throwIfNotFound: true);
        m_BugReportMenu_Toggle = m_BugReportMenu.FindAction("Toggle", throwIfNotFound: true);
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

    // DebugMenu
    private readonly InputActionMap m_DebugMenu;
    private List<IDebugMenuActions> m_DebugMenuActionsCallbackInterfaces = new List<IDebugMenuActions>();
    private readonly InputAction m_DebugMenu_Toggle;
    public struct DebugMenuActions
    {
        private @GlobalInput m_Wrapper;
        public DebugMenuActions(@GlobalInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Toggle => m_Wrapper.m_DebugMenu_Toggle;
        public InputActionMap Get() { return m_Wrapper.m_DebugMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebugMenuActions set) { return set.Get(); }
        public void AddCallbacks(IDebugMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_DebugMenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DebugMenuActionsCallbackInterfaces.Add(instance);
            @Toggle.started += instance.OnToggle;
            @Toggle.performed += instance.OnToggle;
            @Toggle.canceled += instance.OnToggle;
        }

        private void UnregisterCallbacks(IDebugMenuActions instance)
        {
            @Toggle.started -= instance.OnToggle;
            @Toggle.performed -= instance.OnToggle;
            @Toggle.canceled -= instance.OnToggle;
        }

        public void RemoveCallbacks(IDebugMenuActions instance)
        {
            if (m_Wrapper.m_DebugMenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDebugMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_DebugMenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DebugMenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DebugMenuActions @DebugMenu => new DebugMenuActions(this);

    // BugReportMenu
    private readonly InputActionMap m_BugReportMenu;
    private List<IBugReportMenuActions> m_BugReportMenuActionsCallbackInterfaces = new List<IBugReportMenuActions>();
    private readonly InputAction m_BugReportMenu_Toggle;
    public struct BugReportMenuActions
    {
        private @GlobalInput m_Wrapper;
        public BugReportMenuActions(@GlobalInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Toggle => m_Wrapper.m_BugReportMenu_Toggle;
        public InputActionMap Get() { return m_Wrapper.m_BugReportMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BugReportMenuActions set) { return set.Get(); }
        public void AddCallbacks(IBugReportMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_BugReportMenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BugReportMenuActionsCallbackInterfaces.Add(instance);
            @Toggle.started += instance.OnToggle;
            @Toggle.performed += instance.OnToggle;
            @Toggle.canceled += instance.OnToggle;
        }

        private void UnregisterCallbacks(IBugReportMenuActions instance)
        {
            @Toggle.started -= instance.OnToggle;
            @Toggle.performed -= instance.OnToggle;
            @Toggle.canceled -= instance.OnToggle;
        }

        public void RemoveCallbacks(IBugReportMenuActions instance)
        {
            if (m_Wrapper.m_BugReportMenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBugReportMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_BugReportMenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BugReportMenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BugReportMenuActions @BugReportMenu => new BugReportMenuActions(this);
    public interface IDebugMenuActions
    {
        void OnToggle(InputAction.CallbackContext context);
    }
    public interface IBugReportMenuActions
    {
        void OnToggle(InputAction.CallbackContext context);
    }
}
