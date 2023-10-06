//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/01.Scenes/InputSetting.inputactions
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

public partial class @InputSetting : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSetting()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSetting"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""dd446f40-3ecb-4151-9dcf-3f54682a4987"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cb99628a-3262-4c4c-a5b7-1a99e018d5cd"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""3D Vector"",
                    ""id"": ""0f6bb121-ed62-4bdf-a7d4-1f7880c6ec83"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""de078262-4d54-41f2-ab55-e1dd2685852e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a348451b-3830-419d-bb2d-9d9124867793"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""forward"",
                    ""id"": ""a006f085-9add-49aa-b6f8-84fe741e106a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""backward"",
                    ""id"": ""d2d45f59-7cd4-4003-9d8b-9b81d43a20b4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""BuildMenu"",
            ""id"": ""ed04abc9-03c6-4b81-9a90-1e6aa58e7bff"",
            ""actions"": [
                {
                    ""name"": ""ObjectSnap"",
                    ""type"": ""Button"",
                    ""id"": ""c4a3fb27-b5cd-4391-b491-76952616ee7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PointerMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""49aa872b-1025-4afb-b043-98417fa726e0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e7633393-6ee9-49ff-9211-ff667d1257fb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ObjectSnap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5d9b834-2833-4601-9843-ab60931bf9f9"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PointerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        // BuildMenu
        m_BuildMenu = asset.FindActionMap("BuildMenu", throwIfNotFound: true);
        m_BuildMenu_ObjectSnap = m_BuildMenu.FindAction("ObjectSnap", throwIfNotFound: true);
        m_BuildMenu_PointerMove = m_BuildMenu.FindAction("PointerMove", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    public struct PlayerActions
    {
        private @InputSetting m_Wrapper;
        public PlayerActions(@InputSetting wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // BuildMenu
    private readonly InputActionMap m_BuildMenu;
    private IBuildMenuActions m_BuildMenuActionsCallbackInterface;
    private readonly InputAction m_BuildMenu_ObjectSnap;
    private readonly InputAction m_BuildMenu_PointerMove;
    public struct BuildMenuActions
    {
        private @InputSetting m_Wrapper;
        public BuildMenuActions(@InputSetting wrapper) { m_Wrapper = wrapper; }
        public InputAction @ObjectSnap => m_Wrapper.m_BuildMenu_ObjectSnap;
        public InputAction @PointerMove => m_Wrapper.m_BuildMenu_PointerMove;
        public InputActionMap Get() { return m_Wrapper.m_BuildMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildMenuActions set) { return set.Get(); }
        public void SetCallbacks(IBuildMenuActions instance)
        {
            if (m_Wrapper.m_BuildMenuActionsCallbackInterface != null)
            {
                @ObjectSnap.started -= m_Wrapper.m_BuildMenuActionsCallbackInterface.OnObjectSnap;
                @ObjectSnap.performed -= m_Wrapper.m_BuildMenuActionsCallbackInterface.OnObjectSnap;
                @ObjectSnap.canceled -= m_Wrapper.m_BuildMenuActionsCallbackInterface.OnObjectSnap;
                @PointerMove.started -= m_Wrapper.m_BuildMenuActionsCallbackInterface.OnPointerMove;
                @PointerMove.performed -= m_Wrapper.m_BuildMenuActionsCallbackInterface.OnPointerMove;
                @PointerMove.canceled -= m_Wrapper.m_BuildMenuActionsCallbackInterface.OnPointerMove;
            }
            m_Wrapper.m_BuildMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ObjectSnap.started += instance.OnObjectSnap;
                @ObjectSnap.performed += instance.OnObjectSnap;
                @ObjectSnap.canceled += instance.OnObjectSnap;
                @PointerMove.started += instance.OnPointerMove;
                @PointerMove.performed += instance.OnPointerMove;
                @PointerMove.canceled += instance.OnPointerMove;
            }
        }
    }
    public BuildMenuActions @BuildMenu => new BuildMenuActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IBuildMenuActions
    {
        void OnObjectSnap(InputAction.CallbackContext context);
        void OnPointerMove(InputAction.CallbackContext context);
    }
}
