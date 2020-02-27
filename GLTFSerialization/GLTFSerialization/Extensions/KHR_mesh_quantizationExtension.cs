using System;
using GLTF.Math;
using GLTF.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GLTF.Schema
{
	public class KHR_mesh_quantizationExtension : IExtension
	{
		/// <summary>
		/// Dequantization translation.
		/// </summary>
		public Vector3 Translation;
		public static readonly Vector3 TRANSLATION_DEFAULT = new Vector3(0.0f, 0.0f, 0.0f);

		/// <summary>
		/// Dequantization scale.
		/// </summary>
		public Vector3 Scale = SCALE_DEFAULT;
		public static readonly Vector3 SCALE_DEFAULT = new Vector3(1.0f, 1.0f, 1.0f);


		public KHR_mesh_quantizationExtension(Vector3 translation, Vector3 scale)
		{
			Translation = translation;
			Scale = scale;
		}

		public IExtension Clone(GLTFRoot gltfRoot)
		{
			return new KHR_mesh_quantizationExtension(Translation, Scale);
		}

		public JProperty Serialize()
		{
			JObject ext = new JObject();

			if (Translation != TRANSLATION_DEFAULT)
			{
				ext.Add(new JProperty(
					KHR_mesh_quantizationExtensionFactory.TRANSLATION,
					new JArray(Translation.X, Translation.Y, Translation.Z)
				));
			}

			if (Scale != SCALE_DEFAULT)
			{
				ext.Add(new JProperty(
					KHR_mesh_quantizationExtensionFactory.SCALE,
						new JArray(Scale.X, Scale.Y, Scale.Z)
					)
				);
			}

			return new JProperty(KHR_mesh_quantizationExtensionFactory.EXTENSION_NAME, ext);
		}
	}
}
