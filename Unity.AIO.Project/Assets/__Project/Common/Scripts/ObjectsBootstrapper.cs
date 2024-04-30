using UnityEngine;

namespace Project.Common
{
	public static class ObjectsBootstrapper
	{
		private struct BootstrapObject
		{
			public string ResourcesPath;
			public bool DontDestroyOnLoad;

			public BootstrapObject(string resourcesPath, bool dontDestroyOnLoad)
			{
				ResourcesPath = resourcesPath;
				DontDestroyOnLoad = dontDestroyOnLoad;
			}
		}

		private static readonly BootstrapObject[] BootstrapObjects =
		{
			new BootstrapObject("GlobalServices", true)
		};

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Bootstrap()
		{
			foreach (var bootstrapObject in BootstrapObjects)
			{
				var asset = Resources.Load(bootstrapObject.ResourcesPath);
				if (!asset)
				{
					Debug.LogError("Could load persistent behaviours object");
					return;
				}
				var persistentBehaviours = Object.Instantiate(asset);
				persistentBehaviours.name = bootstrapObject.ResourcesPath;
				if (bootstrapObject.DontDestroyOnLoad)
					Object.DontDestroyOnLoad(persistentBehaviours);
			}
		}
	}
}