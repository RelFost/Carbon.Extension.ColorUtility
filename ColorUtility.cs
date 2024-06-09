using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Newtonsoft.Json;

namespace Carbon.Extensions
{
    public static class ColorUtility
    {
        private static Dictionary<string, ColorData> _namedColors;

        private static Dictionary<string, ColorData> NamedColors
        {
            get
            {
                if (_namedColors == null)
                {
                    LoadColorsFromFile();
                }
                return _namedColors;
            }
        }

        private static void LoadColorsFromFile()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "carbon", "extensions", "data", $"{nameof(ColorUtility)}", "colors.json");
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                _namedColors = JsonConvert.DeserializeObject<Dictionary<string, ColorData>>(json);
            }
            else
            {
                throw new FileNotFoundException($"Colors file not found at: {path}");
            }
        }

        public static void UpdateColors()
        {
            LoadColorsFromFile();
        }

        public static string HexToRGBA(string hex = "#FFFFFFFF")
        {
            if (string.IsNullOrEmpty(hex))
            {
                hex = "#FFFFFFFF";
            }

            var str = hex.Trim('#');

            if (str.Length == 6)
            {
                str += "FF";
            }

            if (str.Length != 8)
            {
                throw new InvalidOperationException($"Cannot convert a wrong format: {hex}");
            }

            try
            {
                var r = byte.Parse(str.Substring(0, 2), NumberStyles.HexNumber);
                var g = byte.Parse(str.Substring(2, 2), NumberStyles.HexNumber);
                var b = byte.Parse(str.Substring(4, 2), NumberStyles.HexNumber);
                var a = byte.Parse(str.Substring(6, 2), NumberStyles.HexNumber);

                // Color color = new Color32(r, g, b, a);

                return $"{r:F2} {g:F2} {b:F2} {a:F2}";
            }
            catch (FormatException e)
            {
                throw new InvalidOperationException($"Invalid hex format: {hex}", e);
            }
        }

        public static string GetHexColorByName(string colorName)
        {
            if (NamedColors.TryGetValue(colorName, out var colorData))
            {
                var color = colorData.Hex + "FF";
                return color;
            }
            throw new ArgumentException($"Unknown color name: {colorName}", nameof(colorName));
        }

        public static string GetHexColorByName(string colorName, float transparent)
        {
            if (NamedColors.TryGetValue(colorName, out var colorData))
            {
                var color = HexToRGBA(colorData.Hex + ((int)(transparent * 255)).ToString("X2"));
                return color;
            }
            throw new ArgumentException($"Unknown color name: {colorName}", nameof(colorName));
        }

        public static string GetHexColorByName(string colorName, string transparent)
        {
            if (NamedColors.TryGetValue(colorName, out var colorData))
            {
                var color = colorData.Hex + transparent;
                return color;
            }
            throw new ArgumentException($"Unknown color name: {colorName}", nameof(colorName));
        }

        public static string GetRGBAColorByName(string colorName, float transparent = 1f)
        {
            if (NamedColors.TryGetValue(colorName, out var colorData))
            {
                var rgbaValues = colorData.RGB.Split(' ');
                if (rgbaValues.Length == 3)
                {
                    var r = int.Parse(rgbaValues[0]);
                    var g = int.Parse(rgbaValues[1]);
                    var b = int.Parse(rgbaValues[2]);
                    var a = transparent * 255;

                    return $"{r:F2} {g:F2} {b:F2} {a:F2}";
                }
            }
            throw new ArgumentException($"Unknown color name: {colorName}", nameof(colorName));
        }

        public static string GetRandomRGBAColor(float transparent = 1)
        {
            System.Random random = new System.Random();
            return $"{random.Next(0, 256) / 255f} {random.Next(0, 256) / 255f} {random.Next(0, 256) / 255f} {transparent}";
        }

        public static string GetRandomHEXColor(float transparent = 1)
        {
            byte[] colorBytes = new byte[3];
            new System.Random().NextBytes(colorBytes);

            string hexColor = BitConverter.ToString(colorBytes).Replace("-", "");
            string hexTransparent = ((int)(transparent * 255)).ToString("X2"); // Преобразуем значение прозрачности в HEX
            return "#" + hexColor + hexTransparent;
        }

        public static string GetRandomHEXColor(string transparent = "FF")
        {
            byte[] colorBytes = new byte[3];
            new System.Random().NextBytes(colorBytes);

            string hexColor = BitConverter.ToString(colorBytes).Replace("-", "");
            return "#" + hexColor + transparent;
        }

        private class ColorData
        {
            public string Hex { get; set; }
            public string RGB { get; set; }
        }
    }
}
