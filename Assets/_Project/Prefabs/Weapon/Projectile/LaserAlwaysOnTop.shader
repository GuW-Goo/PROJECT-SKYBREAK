
Shader "Custom/LaserAlwaysOnTop"
{
    Properties
    {
        _Color ("Color", Color) = (1, 0, 0, 1)
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ZTest Always
            Cull Off

            Color [_Color]
        }
    }
}
