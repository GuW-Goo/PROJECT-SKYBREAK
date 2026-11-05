using UnityEditor;
using UnityEngine;


namespace GameSeed
{

    [CustomEditor(typeof(ToonVFXPack))]
    public class CustomInspectorButtons : Editor
    {
        // Define constants for URLs
        public static string DiscordUrl = "https://discord.gg/sbjZXg2YJ9";
        public static string TutorialUrl = "https://www.youtube.com/@gameseedassets/videos";
        public static string DocumentationPath = "";
        public static string Email = "mailto:gameseedassets@gmail.com";
        public static string AssetReviewUrl = "https://u3d.as/3YyW";

        [SerializeField] private Texture headerSprite;

        public override void OnInspectorGUI()
        {
            // Add a space at the top
            GUILayout.Space(5);

            GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel);
            headerStyle.alignment = TextAnchor.MiddleCenter;

            GUILayout.Label(headerSprite, headerStyle);
            GUILayout.Space(5);

            // ======= Buttons at the Top =======
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Documentation", GUILayout.Height(25)))
            {
                Application.OpenURL(DocumentationPath);
            }

            if (GUILayout.Button("Tutorial", GUILayout.Height(25)))
            {
                Application.OpenURL(TutorialUrl);
            }

            if (GUILayout.Button("Join Discord", GUILayout.Height(25)))
            {
                Application.OpenURL(DiscordUrl);
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            // ======= Rate This Asset =======
            if (GUILayout.Button("Rate This Asset", GUILayout.Height(30)))
            {
                Application.OpenURL(AssetReviewUrl);
            }

            GUILayout.Space(10);

            // Draw the default Inspector (after buttons)
            DrawDefaultInspector();
        }
    }
}