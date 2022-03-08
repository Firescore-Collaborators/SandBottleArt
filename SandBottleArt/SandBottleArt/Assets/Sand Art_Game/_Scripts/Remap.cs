using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Remap 
{
    public static float remap(float val, float in1, float in2, float out1, float out2,
        bool in1Clamped, bool in2Clamped, bool out1Clamped, bool out2Clamped)
    {
        if (in1Clamped == true && val < in1) val = in1;
        if (in2Clamped == true && val > in2) val = in2;
 
        float result = out1 + (val - in1) * (out2 - out1) / (in2 - in1);
 
        if (out1Clamped == true && result < out1) result = out1;
        if (out2Clamped == true && result > out2) result = out2;
 
        return result;
    }
}
