using UnityEngine;

public static class LayerUtil
{
    public static int AsMask(int layer)
    {
        return 1 << layer;
    }

    public static bool IsOverlapping(LayerMask mask_a, LayerMask mask_b)
    {
        return (mask_a & mask_b) != 0;
    }

    public static bool IsInMask(int layer, LayerMask mask)
    {
        return IsOverlapping(AsMask(layer), mask);
    }
}
