float4 TintColor;
Texture ColorMap;
sampler ColorMapSampler = sampler_state
{
	texture = <ColorMap>;
};

float4 TintShader(float4 color : COLOR0, float2 texCoords : TEXCOORD0) : COLOR0
{
	//Get the texture data.
	float4 colorMap = tex2D(ColorMapSampler, texCoords);

	//Only tint if the color is below white.
	//if ((colorMap.r + colorMap.g + colorMap.b) * 255 < 730) { return colorMap * TintColor; }
	
	//Return the base color.
	return colorMap * TintColor;
}

technique ColorTint
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 TintShader();
    }
}
