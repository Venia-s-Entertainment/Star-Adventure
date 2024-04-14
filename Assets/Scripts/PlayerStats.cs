using System.Collections.Generic;

public class PlayerStats
{
    public static CelestialInfo celestial;

    public static float Temperature;

    public static bool wearingAstroSuit = false;
    public static bool onShip = false;
    public static bool isPiloting = false;
    public static bool canBreath = true;
    public static bool underWater = false;
    public static bool inWater = false;
}
public class Inventory
{
    public static Item[] itemList = new Item[3];
    public static uint[] itemsAmount = new uint[3];
}


