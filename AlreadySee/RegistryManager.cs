using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Drawing;

namespace AlreadySee
{
    static class RegistryManager
    {
        public static List<Color> CustomPalleteColorList = new List<Color>();
        public static Color NowColor = Color.Black;
        public static float ImageSimilarity = 0.5f;
        public static float ColorSimilarity = 50.0f;
        public static string FilePath = "";
        public static float PenWidth = 20.0f;
        public static bool SearchSubFolder = false;

        public static void LoadRegistry()
        {
            try
            {
                var subkey = OpenSubKey("AlreadySee");

                for (int i = 0; i < 10; i++)
                    CustomPalleteColorList.Add(StringToColor((string)subkey.GetValue($"CustomPallete{i + 1}"), Color.White));
                NowColor = StringToColor((string)subkey.GetValue(nameof(NowColor)), Color.Black);
                ImageSimilarity = null == subkey.GetValue(nameof(ImageSimilarity)) ? ImageSimilarity : float.Parse((string)subkey.GetValue(nameof(ImageSimilarity)));
                ColorSimilarity = null == subkey.GetValue(nameof(ColorSimilarity)) ? ColorSimilarity : float.Parse((string)subkey.GetValue(nameof(ColorSimilarity)));
                FilePath = (string)subkey.GetValue(nameof(FilePath)) ?? FilePath;
                PenWidth = null == subkey.GetValue(nameof(PenWidth)) ? PenWidth : float.Parse((string)subkey.GetValue(nameof(PenWidth)));
                SearchSubFolder = null == subkey.GetValue(nameof(ImageSimilarity)) ? false : StringToBool((string)subkey.GetValue(nameof(SearchSubFolder)));
            }
            catch (Exception e)
            {
                
            }
        }

        public static void SaveRegistry(List<Color> CustomPalleteColorList, Color NowColor, float ImageSimilarity, float ColorSimilarity, string FilePath, float PenWidth, bool SearchSubFolder)
        {
            try
            {
                var subkey = OpenSubKey("AlreadySee");

                for (int i = 0; i < 10; i++)
                    subkey.SetValue($"CustomPallete{i + 1}", ColorToString(CustomPalleteColorList[i]));
                subkey.SetValue(nameof(NowColor), ColorToString(NowColor));
                subkey.SetValue(nameof(ImageSimilarity), ImageSimilarity.ToString());
                subkey.SetValue(nameof(ColorSimilarity), ColorSimilarity.ToString());
                subkey.SetValue(nameof(FilePath), FilePath);
                subkey.SetValue(nameof(PenWidth), PenWidth.ToString());
                subkey.SetValue(nameof(SearchSubFolder), BoolToString(SearchSubFolder));
            }
            catch
            {

            }
        }

        private static RegistryKey OpenSubKey(string name)
        {
            var rkey = Registry.CurrentUser.OpenSubKey(name, true);
            return rkey ?? Registry.CurrentUser.CreateSubKey(name);
        }

        private static object GetValue(RegistryKey subkey, string name)
        {
            return subkey.GetValue(name);
        }

        private static string ColorToString(Color color)
        {
            return $"{color.R},{color.G},{color.B}";
        }


        private static Color StringToColor(string color, Color defaultColor)
        {
            try
            {
                var split = color.Split(',');
                return Color.FromArgb(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
            }
            catch
            {
                return defaultColor;
            }
        }

        private static string BoolToString(bool value)
        {
            return value ? "TRUE" : "FALSE";
        }

        private static bool StringToBool(string value)
        {
            return value == "TRUE";
        }
    }
}
