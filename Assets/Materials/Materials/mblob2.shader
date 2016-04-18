// converted to Unity3D - mgear - http://unitycoder.com/blog

Shader "mShaders/mblob2" 
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
	}
    SubShader 
    {
//        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque"}
//        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        //Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
        LOD 200

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
        
		//Blend One One // transparency
        Pass
        {
        
        Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			Blend SrcAlpha OneMinusSrcAlpha
        
CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest 
#include "UnityCG.cginc"
sampler2D _MainTex;
			sampler2D _ClipTex;
			float4 _ClipRange0 = float4(0.0, 0.0, 1.0, 1.0);


struct v2f {
    float4 pos : POSITION;
    float4 color : COLOR0;
//    float4 fragPos : COLOR1;
	float4 uv : TEXCOORD0;
};

v2f vert (appdata_base v)
{
    v2f o;
    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
//    o.fragPos = o.pos;
	o.uv = float4( v.texcoord.xy, 0, 0 );
	o.color = float4 (1.0, 1.0, 1.0, 1);

    return o;
}


//http://danim.tv/blog/archives/unity3d-shaders/
half4 frag (v2f i) : COLOR
{
	half4 cc = tex2D(_MainTex, i.uv);// * i.color;
    float a = cc.a;
	float animtime = _Time.x*10.0;
      //the centre point for each blob
    float2 move1;
    move1.x = cos(animtime)*0.4;
    move1.y = sin(animtime*1.5)*0.4;
    float2 move2;
    move2.x = cos(animtime*2.0)*0.4;
    move2.y = sin(animtime*3.0)*0.4;
    //screen coordinates
//    float2 p = -1.0 + 2.0 * i.fragPos.xy / float2 (7,7);
    float2 p = i.uv.xy-float2(0.5,0.5);
	//float4(i.uv.y,0,0,1)
//    float2 p = float2(1,1);
    //radius for each blob
    float r1 =(dot(p-move1,p-move1))*8.0;
    float r2 =(dot(p+move2,p+move2))*16.0;
    //sum the meatballs
    float metaball =(1.0/r1+1.0/r2);
    //alter the cut-off power
//    float col =  pow(metaball,8.0) * a * 0.001 ;
    float col =  metaball * a * 0.1 ;
    //set the output color
    half4 colr = cc * a;
    if(r1>0.1&&r2>0.1){colr +=col;}
    		else{
    
    //colr +=col * 0.1;
    float2 w = i.uv.xy;
   	float t = _Time.x;
   	float s = 2.2+1.0*sin(t);
    float v =  0.4+0.4*cos(t*0.9);
   	float2 uv = (0.2 + 0.05 * sin(t*1.1)) * w  + 0.2 * float2(s,v);
    //float2 uv = float2(u,v);
    
    for (int i=0; i<11; ++i){float z = 0.81-0.1*uv.y;
        uv = abs(uv) / dot(uv,uv) - float2(z,z);}
    
	colr += float4(uv*uv, uv.y-uv.x, 1.0)*a;

    } 
    return colr;// + half4(col,col,col,1);
//    return half4(col,col,col,1);
}
ENDCG
        }
    } 
    FallBack "VertexLit"
}