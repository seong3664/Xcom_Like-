Shader "Custom/MyOpaqueTransparentShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)   // 기본 색상
        _MainTex ("Main Texture", 2D) = "white" {}      // 텍스처
        // _AlphaCutoff ("Alpha Cutoff", Range(0,1)) = 0.3 // AlphaClip Threshold
        _Transparency ("Transparency", Range(0,1)) = 1  // 투명도 조절
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" } // Transparent Queue 설정
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            // 알파 블렌딩 설정
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On // ZWrite를 On으로 설정하여 깊이 정보를 기록
            ZTest LEqual // 깊이 테스트 설정
            Cull Back // Back culling을 사용하여 앞면만 그리도록 설정

            // 셰이더 코드
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // 입력, 출력 구조체 정의
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // Vertex Shader
            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                return OUT;
            }

            // 셰이더 속성
            float4 _BaseColor;
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            // float _AlphaCutoff;
            float _Transparency;  // 투명도 값 추가

            // Fragment Shader
          half4 frag (Varyings IN) : SV_Target
    {
        // 텍스처 색상 가져오기
     float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
     texColor *= _BaseColor; // 텍스처 색상과 기본 색상 결합
        
     // 투명도를 적용하여 알파값 조정
     texColor.a *= _Transparency; // BaseColor의 알파값에 투명도 적용

      // Alpha Cutoff: 알파 값이 임계값 이하일 경우 투명 처리
      // clip(texColor.a - _AlphaCutoff);  // 이 부분에서 알파가 0 이하로 내려가면 해당 픽셀이 보이지 않게 됨
    
       return texColor;
    }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
