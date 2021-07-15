using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    #region Field Declarations

    private static Vector3 _bounds;
    private static float _spriteBorder = .9f;
    public static float left { get { return -_bounds.x + _spriteBorder; } }
    public static float right { get { return _bounds.x - _spriteBorder; } }
    public static float top { get { return _bounds.y - _spriteBorder; } }
    public static float bottom { get { return -_bounds.y + _spriteBorder; } }

    #endregion
    private void Start()
    {
        _bounds = new Vector3(11.07f, 11.07f);
    }
}
