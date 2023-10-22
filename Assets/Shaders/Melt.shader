Shader "Custom/Melting" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Melting ("Melting", Range(0, 1)) = 0.5
        _BurnSpeed ("Burn Speed", Range(0, 1)) = 0.5
    }
    SubShader {
        Tags { "Queue" = "Transparent" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Melting;
            float _BurnSpeed;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.a *= smoothstep(_Melting - _BurnSpeed, _Melting, col.r);
                col.rgb *= col.a;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
