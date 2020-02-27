using Newtonsoft.Json.Linq;
using GLTF.Extensions;
using GLTF.Math;

namespace GLTF.Schema
{
	public class KHR_mesh_quantizationExtensionFactory : ExtensionFactory
	{
		public const string EXTENSION_NAME = "KHR_mesh_quantization";

		public const string TRANSLATION = "translation";
		public const string SCALE = "scale";

		public KHR_mesh_quantizationExtensionFactory()
		{
			ExtensionName = EXTENSION_NAME;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			Vector3 translation = KHR_mesh_quantizationExtension.TRANSLATION_DEFAULT;
			Vector3 scale = KHR_mesh_quantizationExtension.SCALE_DEFAULT;

			if (extensionToken != null)
			{
				JToken translationToken = extensionToken.Value[TRANSLATION];
				translation = translationToken != null ? translationToken.DeserializeAsVector3() : translation;

				JToken scaleToken = extensionToken.Value[SCALE];
				scale = scaleToken != null ? scaleToken.DeserializeAsVector3() : scale;
			}
			
			return new KHR_mesh_quantizationExtension(translation, scale);
		}
	}
}
