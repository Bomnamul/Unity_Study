void GetLightingInformation_float(out float3 Direction, out float3 Color, out float Attenuation) // _float: return type
{
    #ifdef SHADERGRAPH_PREVIEW
        Direction = float3(-0.5, 0.5, -0.5);
        Color = float3(1, 1, 1);
        Attenuation = 0.4;
    #else
        Light light = GetMainLight();
        Direction = light.direction;
        Attenuation = light.distanceAttenuation;
        Color = light.color;
    #endif
}

// https://alexanderameye.github.io/toonshading.html
// https://forum.unity.com/threads/the-current-render-pipeline-is-not-compatible-with-this-master-node.660742/
// https://blogs.unity3d.com/2019/07/31/custom-lighting-in-shader-graph-expanding-your-graphs-in-2019/