Shader "RotateUV" {
        Properties 
        {
                _Color ("Main Color", Color) = (1,1,1,1)
                _MainTex ("Base (RGB)", 2D) = "white" {}
        }
        SubShader 
        {
                Tags { "RenderType"="Opaque" "Queue"="Transparent+1"}
                ZWrite Off
                Blend One One
                 
                Pass {
                        Lighting Off
        
                        SetTexture [_MainTex] 
                        {
                                constantColor [_Color]
                                matrix [_Rotation]
                                combine texture * constant
                        }
                }
        } 
        FallBack "Diffuse"
}
