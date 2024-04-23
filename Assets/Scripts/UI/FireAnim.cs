using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireAnim : MonoBehaviour
{
    [SerializeField]
    private ImageAnimation animCtrl;
    [SerializeField]
    private Image _img;
    [SerializeField]
    private Sprite _sprite;

    internal void StartFire()
    {
        if (animCtrl) animCtrl.StartAnimation();
    }

    internal void StopFire()
    {
        if (animCtrl) animCtrl.StopAnimation();
        if (_img) _img.sprite = _sprite;
    }
}
