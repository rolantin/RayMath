Shader "Unlit/fens"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_fk("fk",color)=(1,1,1,1)

			_scale("scale", Range( 0 , 3)) = 1
		_power("power", Range( 0 , 3)) = 1
		_bias("bias", Range( 0 , 3)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 nor:NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float3 normal:TEXCOORD2;
			    float3 pos :TEXCOORD3 ;
			};

			sampler2D _MainTex;
			float4 _fk;
			float4 _MainTex_ST;
				uniform float _bias;
		uniform float _scale;
		uniform float _power;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos=mul(unity_ObjectToWorld,v.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = UnityObjectToWorldNormal(v.nor);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

              
            float3 viewpos =normalize (_WorldSpaceCameraPos.xyz - i.pos);
            

			float fresnelNDotV1 = dot( normalize( i.normal ), viewpos );
			float fresnelNode1 = ( _bias + _fk * pow( 1.0 - fresnelNDotV1, _power));

				float dsf=2;



				float3 ss=normalize(_WorldSpaceCameraPos);

				float3 sa=normalize(i.pos);

				float3 nn = normalize(dot( normalize(i.normal) ,sa- ss));

				//float3 nn1 = dsf+(1−dsf) * pow(1-nn,5);
				float final =_fk+ (1-_fk) * pow(1-nn,5);




			 return float4(fresnelNode1,fresnelNode1,fresnelNode1,fresnelNode1);

			
		
			}
			ENDCG
		}
	}
}
