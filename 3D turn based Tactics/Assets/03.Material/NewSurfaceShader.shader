Shader "Custom/MyOpaqueTransparentShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)   // �⺻ ����
        _MainTex ("Main Texture", 2D) = "white" {}      // �ؽ�ó
        // _AlphaCutoff ("Alpha Cutoff", Range(0,1)) = 0.3 // AlphaClip Threshold
        _Transparency ("Transparency", Range(0,1)) = 1  // ���� ����
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" } // Transparent Queue ����
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            // ���� ���� ����
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On // ZWrite�� On���� �����Ͽ� ���� ������ ���
            ZTest LEqual // ���� �׽�Ʈ ����
            Cull Back // Back culling�� ����Ͽ� �ո鸸 �׸����� ����

            // ���̴� �ڵ�
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // �Է�, ��� ����ü ����
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

            // ���̴� �Ӽ�
            float4 _BaseColor;
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            // float _AlphaCutoff;
            float _Transparency;  // ���� �� �߰�

            // Fragment Shader
          half4 frag (Varyings IN) : SV_Target
    {
        // �ؽ�ó ���� ��������
     float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
     texColor *= _BaseColor; // �ؽ�ó ����� �⺻ ���� ����
        
     // ������ �����Ͽ� ���İ� ����
     texColor.a *= _Transparency; // BaseColor�� ���İ��� ���� ����

      // Alpha Cutoff: ���� ���� �Ӱ谪 ������ ��� ���� ó��
      // clip(texColor.a - _AlphaCutoff);  // �� �κп��� ���İ� 0 ���Ϸ� �������� �ش� �ȼ��� ������ �ʰ� ��
    
       return texColor;
    }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
