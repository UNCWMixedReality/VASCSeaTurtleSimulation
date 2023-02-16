using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq.Expressions;
using System;

namespace Altimit.UI
{
    public partial class AUI
    {
        public static Material Colored
        {
            get
            {
                return GetMaterial("Colored");
            }
        }
        public static Material Dark
        {
            get
            {
                return GetMaterial("Dark");
            }
        }
        public static Material None
        {
            get
            {
                return GetMaterial("None");
            }
        }
        public static Material Red
        {
            get
            {
                return GetMaterial("Red");
            }
        }
        public static Material Blue
        {
            get
            {
                return GetMaterial("Blue");
            }
        }
        public static Material Default
        {
            get
            {
                return GetMaterial("Default");
            }
        }
        public static float Light
        {
            get
            {
                return .66f;
            }
        }
        /*
        public static Material Light
        {
            get
            {
                return GetMaterial("Light");
            }
        }*/
        public static Material DimLight
        {
            get
            {
                return GetMaterial("Dim Light");
            }
        }

        public static Sprite InputFieldBackground
        {
            get
            {
                return GetSprite("InputFieldBackground");
            }
        }
        public static TMP_FontAsset Font = GetFont("Roboto/Roboto-Medium SDF");

        public const int TinySize = 40;
        public const int SmallSize = 70;
        public const int MediumSize = 100;
        public const int LargeSize = 130;

        public const int LargeSpace = 40;
        public const int MediumSpace = 20;
        public const int SmallSpace = 15;
        public const int TinySpace = 5;
    }
}