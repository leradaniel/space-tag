// ReCalcCubeTexture
using UnityEngine;

[ExecuteInEditMode]
public class ReCalcCubeTexture : MonoBehaviour
{
	private Vector3 _currentScale;

	private void Start()
	{
		Calculate();
	}

	private void Update()
	{
		Calculate();
	}

	public void Calculate()
	{
		if (!(_currentScale == base.transform.localScale) && !CheckForDefaultSize())
		{
			_currentScale = base.transform.localScale;
			Mesh mesh = GetMesh();
			mesh.uv = SetupUvMap(mesh.uv);
			mesh.name = "Cube Instance";
			if (GetComponent<Renderer>().sharedMaterial.mainTexture.wrapMode != 0)
			{
				GetComponent<Renderer>().sharedMaterial.mainTexture.wrapMode = TextureWrapMode.Repeat;
			}
		}
	}

	private Mesh GetMesh()
	{
		return GetComponent<MeshFilter>().mesh;
	}

	private Vector2[] SetupUvMap(Vector2[] meshUVs)
	{
		float x = _currentScale.x;
		float z = _currentScale.z;
		float y = _currentScale.y;
		ref Vector2 reference = ref meshUVs[2];
		reference = new Vector2(0f, y);
		ref Vector2 reference2 = ref meshUVs[3];
		reference2 = new Vector2(x, y);
		ref Vector2 reference3 = ref meshUVs[0];
		reference3 = new Vector2(0f, 0f);
		ref Vector2 reference4 = ref meshUVs[1];
		reference4 = new Vector2(x, 0f);
		ref Vector2 reference5 = ref meshUVs[7];
		reference5 = new Vector2(0f, 0f);
		ref Vector2 reference6 = ref meshUVs[6];
		reference6 = new Vector2(x, 0f);
		ref Vector2 reference7 = ref meshUVs[11];
		reference7 = new Vector2(0f, y);
		ref Vector2 reference8 = ref meshUVs[10];
		reference8 = new Vector2(x, y);
		ref Vector2 reference9 = ref meshUVs[19];
		reference9 = new Vector2(z, 0f);
		ref Vector2 reference10 = ref meshUVs[17];
		reference10 = new Vector2(0f, y);
		ref Vector2 reference11 = ref meshUVs[16];
		reference11 = new Vector2(0f, 0f);
		ref Vector2 reference12 = ref meshUVs[18];
		reference12 = new Vector2(z, y);
		ref Vector2 reference13 = ref meshUVs[23];
		reference13 = new Vector2(z, 0f);
		ref Vector2 reference14 = ref meshUVs[21];
		reference14 = new Vector2(0f, y);
		ref Vector2 reference15 = ref meshUVs[20];
		reference15 = new Vector2(0f, 0f);
		ref Vector2 reference16 = ref meshUVs[22];
		reference16 = new Vector2(z, y);
		ref Vector2 reference17 = ref meshUVs[4];
		reference17 = new Vector2(x, 0f);
		ref Vector2 reference18 = ref meshUVs[5];
		reference18 = new Vector2(0f, 0f);
		ref Vector2 reference19 = ref meshUVs[8];
		reference19 = new Vector2(x, z);
		ref Vector2 reference20 = ref meshUVs[9];
		reference20 = new Vector2(0f, z);
		ref Vector2 reference21 = ref meshUVs[13];
		reference21 = new Vector2(x, 0f);
		ref Vector2 reference22 = ref meshUVs[14];
		reference22 = new Vector2(0f, 0f);
		ref Vector2 reference23 = ref meshUVs[12];
		reference23 = new Vector2(x, z);
		ref Vector2 reference24 = ref meshUVs[15];
		reference24 = new Vector2(0f, z);
		return meshUVs;
	}

	private bool CheckForDefaultSize()
	{
		if (_currentScale != Vector3.one)
		{
			return false;
		}
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Object.DestroyImmediate(GetComponent<MeshFilter>());
		base.gameObject.AddComponent<MeshFilter>();
		GetComponent<MeshFilter>().sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
		Object.DestroyImmediate(gameObject);
		return true;
	}
}
