using System;
using TriTailorShop.Data;
using UnityEngine;

namespace TriTailorShop
{
    public class GameManager : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod()]
        static void Init()
        {
            ItemUtilities.Reset();
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}
