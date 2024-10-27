Shader "Custom/Blur"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BlurSize ("Blur size", Range(0.0, 10.0)) = 0.0
    }
    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            SetTexture [_MainTex]
            {
                combine texture
                constantColor (1, 1, 1, 0.5)
                combine previous lerp (constant) previous
            }
            SetTexture [_MainTex]
            {
                combine previous * previous alpha * previous
            }
        }
    }
}
