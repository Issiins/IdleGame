using System;
using Unity.U2D.Physics;
using UnityEngine;

namespace UI
{
    public class Shop : MonoBehaviour
    {
        public void ChangeShopMenu() => gameObject.SetActive(!gameObject.activeSelf);
    }
}