using UnityEngine;
using UnityEditor;
using ProceduralPrimitives;

/*namespace ProceduralPrimitives
{*/
    

    /// <summary>
    /// DEPRECATED!
    /// 
    /// Please use the extension methods in MeshExtensions and GameObjectExtensions to programatically create primitives now.
    /// This is being left in as a reference for how things used to work, and to aid in backwards compatibility with old versions.
    /// </summary>
public class ProceduralPrimitivesMenu : EditorWindow
{
    ProceduralPrimitives.PrimitiveType primitiveType = ProceduralPrimitives.PrimitiveType.Box;

    float width = 1;
    float height = 1;
    float depth = 1;

    int widthDivisions = 10;
    int depthDivisions = 10;

    float radius = 0.5f;
    int stacks = 20;
    int slices = 20;

    float topRadius = 0.5f;
    float bottomRadius = 0.5f;
    float length = 1;

    [MenuItem("GameObject/Create Other/Procedural Primitive")]
    public static void ShowWindow()
    {
        GetWindow<ProceduralPrimitivesMenu>();
    }

    void OnGUI()
    {
        GUILayout.Label("Create Procedural Primitive", EditorStyles.boldLabel);

        primitiveType = (ProceduralPrimitives.PrimitiveType)EditorGUILayout.EnumPopup("Select shape: ", primitiveType);

        switch (primitiveType)
        {
            case ProceduralPrimitives.PrimitiveType.Box:
                width = EditorGUILayout.Slider("Width: ", width, 0.01f, 1000);
                height = EditorGUILayout.Slider("Height: ", height, 0.01f, 1000);
                depth = EditorGUILayout.Slider("Depth: ", depth, 0.01f, 1000);
                break;
            case ProceduralPrimitives.PrimitiveType.Cylinder:
                bottomRadius = EditorGUILayout.Slider("Bottom Radius: ", bottomRadius, 0.0f, 1000);
                topRadius = EditorGUILayout.Slider("Top Radius: ", topRadius, 0.0f, 1000);
                length = EditorGUILayout.Slider("Length: ", length, 0.01f, 1000);
                stacks = EditorGUILayout.IntSlider("Stacks: ", stacks, 1, 256);
                slices = EditorGUILayout.IntSlider("Slices: ", slices, 1, 256);
                break;
            case ProceduralPrimitives.PrimitiveType.Plane:
                width = EditorGUILayout.Slider("Width: ", width, 0.01f, 1000);
                depth = EditorGUILayout.Slider("Depth: ", depth, 0.01f, 1000);
                widthDivisions = EditorGUILayout.IntSlider("Width Divisions: ", widthDivisions, 1, 254);
                depthDivisions = EditorGUILayout.IntSlider("Depth Divisions: ", depthDivisions, 1, 254);
                break;
            case ProceduralPrimitives.PrimitiveType.Sphere:
                radius = EditorGUILayout.Slider("Radius: ", radius, 0.01f, 1000);
                stacks = EditorGUILayout.IntSlider("Stacks: ", stacks, 1, 256);
                slices = EditorGUILayout.IntSlider("Slices: ", slices, 1, 256);
                break;
        }

        if (GUILayout.Button("Create"))
        {
            switch (primitiveType)
            {
                case ProceduralPrimitives.PrimitiveType.Box:
                    Primitive.CreateBoxGameObject(width, height, depth);
                    break;
                case ProceduralPrimitives.PrimitiveType.Cylinder:
                    Primitive.CreateCylinderGameObject(bottomRadius, topRadius, length, slices, stacks);
                    break;
                case ProceduralPrimitives.PrimitiveType.Plane:
                    Primitive.CreatePlaneGameObject(width, depth, widthDivisions, depthDivisions);
                    break;
                case ProceduralPrimitives.PrimitiveType.Sphere:
                    Primitive.CreateSphereGameObject(radius, slices, stacks);
                    break;
            }
        }

		bool isNull = Camera.main.targetTexture == null;

		if (isNull)
		{
			RenderTexture rt = new RenderTexture(100, 100, 0);
			Camera.main.targetTexture = rt;
		}

		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = Camera.main.targetTexture;
		Camera.main.Render();

		if (Camera.main.targetTexture  == null)
		{
			Debug.Log("Camera is null");
			return;
		}

		Texture2D image = new Texture2D(Camera.main.targetTexture.width, Camera.main.targetTexture.height);
		image.ReadPixels(new Rect(0, 0, Camera.main.targetTexture.width, Camera.main.targetTexture.height), 0, 0);
		image.Apply();

		RenderTexture.active = currentRT;
		EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetRect(100, 100), image);

		if (isNull)
		{
			Camera.main.targetTexture = null;
		}
    }
}

//}
