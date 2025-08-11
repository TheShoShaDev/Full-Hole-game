Shader "Custom/New Hole Shader"
{
   Properties
   {
	   [IntRange] _StensilID ("Stensil ID", Range(0, 225)) = 0
   }
   
   SubShader
   {
	   Tags 
	   { 
		    "RenderType" = "Opaque" 
			"Queue" = "Geometry-1"
			"RenderPipeLine" = "UniversalPipeLine"
	   }

	   Pass
	   {
		   Blend Zero One
		   ZWrite Off

		   Stencil
		   {
			   Ref [_StensilID]
			   Comp Always
			   Pass Replace
		   }
	   }
   }

}
