Shader "Alex Shaders/Flashlight" {
	Properties {
		_DropFreq ("Drop Frequency", Range(0, 50.0)) = 0.3
		_WaterSpeed ("Water Speed", Range(0, 50.0)) = 0.3
	}
   SubShader {
     // Tags { "Queue" = "Transparent" } 
         // draw after all opaque geometry has been drawn
      Pass {
			
			ZWrite Off // don't write to depth buffer 
            // in order not to occlude other objects
			
			Blend SrcAlpha OneMinusSrcAlpha // use alpha blending
			
			CGPROGRAM 
			
			#pragma vertex vert
			#pragma fragment frag
			
			struct vertexInput {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			
			struct vertexOutput {
				float4 pos : POSITION;
				float4 tex : TEXCOORD0;
			};
			
	         vertexOutput vert(vertexInput input) 
	         {
	            vertexOutput output;
	            
				output.pos = input.vertex;
				output.tex = input.texcoord;
	            
	            return output;
	         }
			
			float _DropFreq;
			float _DistClampMin;
			float _DistClampMax;
			
			float4 frag(vertexOutput input) : COLOR 
			{
			
			  	float dist = distance(input.tex, float2(0.5,0.5));
			  	dist = clamp(dist, _DistClampMin, _DistClampMax);
   				float waterDrop = (sin(dist*_DropFreq));
				
				float4 penis = float4(1.0,0.0,0.0,1.0);
				
				//return mix(float4(0.0,0.0,0.0,1.0), float4(0.0,0.0,0.0,0.0), waterDrop); 
				//return float4(0.0,0.0,0.0,1.0);
				//return waterDrop;
				return penis;
			}
		
			ENDCG  
		}
		
		
	}
}