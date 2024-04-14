namespace Settings
{
    using System;
    using UnityEngine;

    public class GameSettings
    {
        public static bool adminIsAavailable { get; set; } = false;
        public static float SimulationSpeed { get; set; } = 1;
        public static bool isPaused { get; set; } = false;
        public static float globalVolume { get; } = -10f;
        public static float musicVolume { get; } = -10f;
        public static float effectsVolume { get; } = -10f;
        public static int qualityLevel { get; } = 4;
        public static Vector2Int[] screenResolutions { get; } ={

            new Vector2Int(800, 600),
            new Vector2Int(960, 540),
            new Vector2Int(1024, 576),
            new Vector2Int(1280, 720),
            new Vector2Int(1920, 1080),
            new Vector2Int(2560, 1440)
        };
        public static int[] Framerates { get; } = { 240, 120, 60, 30 };
    }
    public class PlayerSettings
    {
        public static bool takeDamage { get; set; } = true;
        public static bool noclipEnabled { get; set; } = false;
        public static float currentHealth { get; set; }
        public static float Sensivity { get; set; } = 2;
        public static float characterSpeed { get; set; } = 55000;
        public static float characterStrength { get; set; } = 300;
        public static float jumpForce { get; set; } = 500;
        public static float maxHealth { get; set; } = 100;
        public static float maxTemp { get; set; } = 50;
        public static float minTemp { get; set; } = 0;
    }
}

