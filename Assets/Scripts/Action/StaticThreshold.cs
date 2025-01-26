using UnityEngine;

public static class StaticThreshold
{
    public enum Levels { low, medium, high };
    public static float lowActionCostThreshold = 7500;
    public static float midActionCostThreshold = 12500;
    public static float highActionCostThreshold = 15000;
    public static float lowInvestDanger = 4;
    public static float midInvestDanger = 8;
    public static float highInvestDanger = 11;
}
