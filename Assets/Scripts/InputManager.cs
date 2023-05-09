using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{

    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion
    private PlayerControls playerControls;


    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        playerControls = new PlayerControls();

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Touch.PrimaryContact.started += ctx => TouchPrimaryContactStarted(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => TouchPrimaryContactCanceled(ctx);
    }

    private void TouchPrimaryContactCanceled(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke(Utils.ScreenToWorld(Camera.main, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
    }

    private void TouchPrimaryContactStarted(InputAction.CallbackContext ctx)
    {
        OnStartTouch?.Invoke(Utils.ScreenToWorld(Camera.main, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.startTime);

    }

    public Vector2 GetPrimaryTouchPosition()
    {
        return Utils.ScreenToWorld(Camera.main, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

}
