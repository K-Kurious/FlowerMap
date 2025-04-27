using CustomMapLib;
using MelonLoader;
using UnityEngine;
using BuildInfo = FlowerMap.BuildInfo;

[assembly: MelonInfo(typeof(FlowerMap.Flower), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonGame(null, null)]

namespace FlowerMap
{
    public static class BuildInfo
    {
        public const string Name = "Flower Map";
        public const string Description = "The first test map";
        public const string Author = "SisterPankake";
        public const string Company = null;
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }

    public class Flower : Map
    {
        public override void OnLateInitializeMelon() => Initialize(BuildInfo.Name, BuildInfo.Version, BuildInfo.Author);

        GameObject arena = new GameObject();
        public override void OnMapCreation()
        {
            Il2CppAssetBundle bundle = LoadBundle("FlowerMap.Resources.flower");
            MelonLogger.Msg($"Loaded Bundle, yay");
            arena = GameObject.Instantiate(bundle.LoadAsset<GameObject>("MapParent"));
            MelonLogger.Msg($"Loaded Map Asset");
            arena.transform.SetParent(mapParent.transform);
            MelonLogger.Msg($"Set Parent Transform");

            HostPedestal.SetFirstSequence(arena.transform.FindChild("Positions").transform.GetChild(0).position);
            MelonLogger.Msg($"Set Host First Sequence");
            ClientPedestal.SetFirstSequence(arena.transform.FindChild("Positions").transform.GetChild(0).position);
            MelonLogger.Msg($"Set Client First Sequence");

        }
        public Il2CppAssetBundle LoadBundle(string path)
        {
            using (System.IO.Stream bundleStream = MelonAssembly.Assembly.GetManifestResourceStream(path))
            {
                byte[] bundleBytes = new byte[bundleStream.Length];
                bundleStream.Read(bundleBytes, 0, bundleBytes.Length);
                return Il2CppAssetBundleManager.LoadFromMemory(bundleBytes);
            }
        }
    }
}
