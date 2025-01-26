using UnityEngine;

public static class StaticThreshold
{
    public enum Levels { low, medium, high };
    public static float lowActionCostThreshold = 20;
    public static float midActionCostThreshold = 50;
    public static float highActionCostThreshold = 70;
    public static float lowInvestDanger = 20;
    public static float midInvestDanger = 40;
    public static float highInvestDanger = 60;
}
