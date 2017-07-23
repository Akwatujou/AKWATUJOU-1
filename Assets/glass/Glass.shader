Shader "Custom/Glass"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BottomV("Bottom V", Float) = 0.0
		_TopV ("Top V", Float) = 1.0
	}
	SubShader
	{
		Cull Back
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha

		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4 _Colors[8];
			int _ColorCount;
			float _BottomV;
			float _TopV;

			fixed4 frag (v2f i) : SV_Target
			{
				int index = int((i.uv.y - _BottomV) / (_TopV - _BottomV) * _ColorCount);
				float4 mask = tex2D(_MainTex, i.uv);
				fixed4 col = lerp(fixed4(0.,0.,0.,1.), _Colors[index], mask.r);
				col.a *= mask.a;
				return col;
			}
			ENDCG
		}
	}
}
