Shader "Alex Shaders/Flashlight1" {
    Properties {
       // _DropFreq ("Drop Frequency", Range(0, 50.0)) = 0.3
       // _DistClampMin ("Distance Minimum Clamp Value", Range(0, 0.3)) = 0.1
       // _DistClampMax ("Distance Maximum Clamp Value", Range(0.3, 1.0)) = 0.3
        
    }
	SubShader {
		Pass {
		
		
			ZWrite Off // don't write to depth buffer 
            // in order not to occlude other objects
			
			Blend SrcAlpha OneMinusSrcAlpha // use alpha blending
		
		
			CGPROGRAM 
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
#pragma exclude_renderers gles
			#pragma fragment frag
			
			struct vertexInput{
				float4 vertex : POSITION;
			};
			
			struct vertexOutput{
				float4 pos : POSITION;
				float4 texcoord : TEXCOORD;
			};
			
			vertexOutput myVert(vertexInput input)
			{
				vertexOutput output;
				
				output.pos = float4(input.vertex.xy, 0.0, 1.0);
				output.texcoord = float4((float2( output.pos.x, - output.pos.y ) + float2( 1.0 ) ) / float2( 2.0 ), 0.0, 0.0);
			
				return output;
			}
			
		//	float _DropFreq;
		//	float _DistClampMin;
		//	float _DistClampMax;
			
			float4 frag(vertexOutput input) : COLOR 
			{
			
		//	  	float dist = distance(input.texcoord, float2(0.5,0.5));
		//	  	dist = clamp(dist, _DistClampMin, _DistClampMax);
   		//		float waterDrop = (sin(dist*_DropFreq));
				
		//		return mix(float4(0.0,0.0,0.0,1.0), float4(0.0,0.0,0.0,0.0), waterDrop); 
				return float4(0.0,0.0,0.0,1.0);
			}
		
			ENDCG  
		}
		
		
	}
}