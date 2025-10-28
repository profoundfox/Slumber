sampler2D inputTexture : register(s0);

float2 texelSize; 
float blurAmount = 1.0;

float4 PixelShaderFunction(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 color = float4(0,0,0,0);


    for(int x=-1; x<=1; x++)
    {
        for(int y=-1; y<=1; y++)
        {
            color += tex2D(inputTexture, texCoord + float2(x, y) * texelSize * blurAmount);
        }
    }

    color /= 9.0;
    return color;
}

technique SimpleBlur
{
    pass P0
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
