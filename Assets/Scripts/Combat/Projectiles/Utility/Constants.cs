using UnityEngine;

namespace STIHOWE.Constants
/*https://www.anton.website/quick-tip-for-constants/*/
{
    // public static class Paths
    // {
    //     public static readonly string settings = Application.streamingAssetsPath + "/Settings.json";
    //     public static readonly string playerSettings = Application.streamingAssetsPath + "/Player.json";
    // }

    // public static class Tags
    // {
    //     public const string board = "Board";
    //     public const string metal = "Metal";
    //     public const string net = "Net";
    //     public const string stick = "Stick";
    // }
    public static class Layers
    {
        //public static readonly int player = LayerMask.NameToLayer("Player");
        //public static readonly int enemy = LayerMask.NameToLayer("Enemy");
        public static readonly int bullets = LayerMask.NameToLayer("Bullets");
    }
}