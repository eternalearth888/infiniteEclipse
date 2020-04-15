using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Digger
{
    [ExecuteInEditMode]
    public class MicroSplatSync : MonoBehaviour
    {
#if __MICROSPLAT_DIGGER__
        public void OnEnable()
        {
            var digger = GetComponent<DiggerSystem>();
            var microSplat = digger.Terrain.GetComponent<MicroSplatTerrain>();
            if (!microSplat) {
                Debug.LogError($"Could not find MicroSplatTerrain on terrain {digger.Terrain.name}");
                return;
            }

            Sync(null);
            microSplat.OnMaterialSync += Sync;
#if UNITY_EDITOR
            EditorSceneManager.sceneSaved += OnSceneSaved;
#endif
        }

        public void OnDisable()
        {
            var digger = GetComponent<DiggerSystem>();
            var microSplat = digger.Terrain.GetComponent<MicroSplatTerrain>();
            if (!microSplat) {
                Debug.LogError($"Could not find MicroSplatTerrain on terrain {digger.Terrain.name}");
                return;
            }

            microSplat.OnMaterialSync -= Sync;
#if UNITY_EDITOR
            EditorSceneManager.sceneSaved -= OnSceneSaved;
#endif
        }

        private void OnSceneSaved(Scene scene)
        {
            if (this) {
                Sync(null);
            }
        }

        private void Sync(Material m)
        {
            // Check itself hasn't been destroyed
            if (!this)
                return;
            
            var digger = GetComponent<DiggerSystem>();

            if (!digger || digger.Materials == null || digger.Materials.Length == 0 || !digger.Materials[0]) {
                Debug.LogWarning($"[Digger] Could not sync MicroSplat material");
                return;
            }

            var microSplat = digger.Terrain.GetComponent<MicroSplatTerrain>();
            if (!microSplat) {
                Debug.LogError($"Could not find MicroSplatTerrain on terrain {digger.Terrain.name}");
                return;
            }
            
            if (!microSplat.matInstance){
                Debug.Log($"[Digger] Skipping sync of MicroSplat material");
                return;
            }

            var material = digger.Materials[0];
            material.CopyPropertiesFromMaterial(microSplat.matInstance);

#if UNITY_EDITOR
            if (!Application.isPlaying) {
                var matPath = Path.Combine(digger.BasePathData, $"diggerMicroSplat.mat");
                material = EditorUtils.CreateOrReplaceAsset(material, matPath);
                AssetDatabase.ImportAsset(matPath, ImportAssetOptions.ForceUpdate);
            }
#endif

            digger.Materials[0] = material;
        }
#endif
    }
}