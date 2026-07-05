using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace URPGlitch
{
    public class AnalogGlitchPass : ScriptableRenderPass
    {
        private const string k_AnalogPassName = "AnalogRenderPass";

        static readonly int ScanLineJitterID = Shader.PropertyToID("_ScanLineJitter");
        static readonly int VerticalJumpID = Shader.PropertyToID("_VerticalJump");
        static readonly int HorizontalShakeID = Shader.PropertyToID("_HorizontalShake");
        static readonly int ColorDriftID = Shader.PropertyToID("_ColorDrift");

        private Material analogGlitchMat;
        private float _verticalJumpTime;
        private static Vector4 scaleBias = new Vector4(1, 1, 0, 0);

        public AnalogGlitchPass(Shader shader)
        {
            if (shader != null) analogGlitchMat = CoreUtils.CreateEngineMaterial(shader);

            requiresIntermediateTexture = true;
        }

        private bool UpdateSettings()
        {
            if (analogGlitchMat == null) { Debug.LogError("update settings material null"); return false; }
            var volumeStack = VolumeManager.instance.stack;
            if (volumeStack == null) return false;

            var volume = volumeStack.GetComponent<AnalogGlitchVolume>();
            if (volume == null || !volume.IsActive) return false;

            var scanLineJitter = volume.scanLineJitter.value;
            var verticalJump = volume.verticalJump.value;
            var horizontalShake = volume.horizontalShake.value;
            var colorDrift = volume.colorDrift.value;

            _verticalJumpTime += Time.deltaTime * verticalJump * 11.3f;

            var slThresh = Mathf.Clamp01(1.0f - scanLineJitter * 1.2f);
            var slDisp = scanLineJitter <= AnalogGlitchVolume.ActiveEpsilon
                ? 0f
                : 0.002f + Mathf.Pow(scanLineJitter, 3) * 0.05f;
            analogGlitchMat.SetVector(ScanLineJitterID, new Vector2(slDisp, slThresh));

            var vj = new Vector2(verticalJump, _verticalJumpTime);
            analogGlitchMat.SetVector(VerticalJumpID, vj);
            analogGlitchMat.SetFloat(HorizontalShakeID, horizontalShake * 0.2f);

            var cd = new Vector2(colorDrift * 0.04f, Time.time * 606.11f);
            analogGlitchMat.SetVector(ColorDriftID, cd);
            return true;
        }

        public void Dispose()
        {
            CoreUtils.Destroy(analogGlitchMat);
        }

        #region Render graph code

        private class PassData
        {
            internal TextureHandle source;
            internal Material material;
        }
        private static void ExecutePass(PassData data, RasterGraphContext context, int pass)
        {
            Blitter.BlitTexture(context.cmd, data.source, scaleBias, data.material, pass);
        }
        public override void RecordRenderGraph(RenderGraph renderGraph,
        ContextContainer frameData)
        {
            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();

            var isPostProcessEnabled = frameData.Get<UniversalCameraData>().postProcessEnabled;
            var isSceneViewCamera = frameData.Get<UniversalCameraData>().isSceneViewCamera;
            if (!isPostProcessEnabled || isSceneViewCamera)
            {
                return;
            }

            if (resourceData.isActiveTargetBackBuffer)
            {
                Debug.LogError($"Skipping render pass. BlitAndSwapColorRendererFeature requires an intermediate ColorTexture, we can't use the BackBuffer as a texture input.");
                return;
            }

            if (!UpdateSettings()) return;

            var src = resourceData.activeColorTexture;
            var destinationDesc = renderGraph.GetTextureDesc(src);
            destinationDesc.name = $"CameraColor-{k_AnalogPassName}";
            destinationDesc.clearBuffer = false;

            TextureHandle dst = renderGraph.CreateTexture(destinationDesc);

            using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass(k_AnalogPassName, out PassData passData, profilingSampler))
            {
                passData.source = src;
                passData.material = analogGlitchMat;

                builder.UseTexture(src);
                builder.SetRenderAttachment(dst, 0);
                builder.SetRenderFunc((PassData data, RasterGraphContext context) => ExecutePass(data, context, 0));
            }

            resourceData.cameraColor = dst;
        }

        #endregion
    }
}
