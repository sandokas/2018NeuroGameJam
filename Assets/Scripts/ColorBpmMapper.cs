using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class ColorBpmMapper
    {
        public static Color GetColor(float bpm)
        {
            Color red = Color.red;
            Color magenta = Color.magenta;
            Color blue = Color.blue;
            Color cyan = Color.cyan;
            Color green = Color.green;
            Color yellow = Color.yellow;
            Color returnColor;
            if (bpm < 30f)
            {
                returnColor = Color.black;
            }
            if (bpm >= 30f && bpm < 55f)
            {
                returnColor = Color.Lerp(red, yellow, ((bpm - 30f) / 25f));
            }
            else if (bpm >= 55f && bpm < 80f)
            {
                returnColor = Color.Lerp(yellow, green, ((bpm - 55f) / 25f));
            }
            else if (bpm >= 80f && bpm < 110f)
            {
                returnColor = Color.Lerp(green, cyan, ((bpm - 80f) / 30f));
            }
            else if (bpm >= 110 && bpm < 145f)
            {
                returnColor = Color.Lerp(cyan, blue, ((bpm - 100f) / 35f));
            }
            else if (bpm >= 145f && bpm < 190f)
            {
                returnColor = Color.Lerp(blue, magenta, ((bpm - 135f) / 45f));
            }
            else if (bpm >= 190f && bpm < 250f)
            {
                returnColor = Color.Lerp(magenta, red, ((bpm - 170f) / 60f));
            }
            else if (bpm >= 250f)
            {
                returnColor = Color.white;
            }
            else
            {
                returnColor = Color.black;
            }
            return returnColor;

        }
        public static Color GetColorDeprecated(float bpm)
        {
            Color red = Color.red;
            Color magenta = Color.magenta;
            Color blue = Color.blue;
            Color turqoise = new Color(0, 255, 255);
            Color green = Color.green;
            Color yellow = Color.yellow;
            Color returnColor;
            if (bpm >= 30 && bpm < 50)
            {
                returnColor = Color.Lerp(red, magenta, ((bpm - 30) / 20));
            }
            else if (bpm >= 50 && bpm < 75)
            {
                returnColor = Color.Lerp(magenta, blue, ((bpm - 50) / 25));
            }
            else if (bpm >= 75 && bpm < 100)
            {
                returnColor = Color.Lerp(blue, turqoise, ((bpm - 75) / 25));
            }
            else if (bpm >= 100 && bpm < 130)
            {
                returnColor = Color.Lerp(turqoise, green, ((bpm - 100) / 30));
            }
            else if (bpm >= 130 && bpm < 170)
            {
                returnColor = Color.Lerp(green, yellow, ((bpm - 135) / 40));
            }
            else if (bpm >= 170 && bpm < 250)
            {
                returnColor = Color.Lerp(yellow, red, ((bpm - 170) / 80));
            }
            else
            {
                returnColor = red;
            }
            return returnColor;
        }
    }
}
