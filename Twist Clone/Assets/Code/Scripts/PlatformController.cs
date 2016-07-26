using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
	[SerializeField] private PlatformGenerator m_Generator = new PlatformGenerator();
	[SerializeField] private PlatformPool m_Pool = new PlatformPool();

	void Awake ()
	{
		m_Generator.GeneratePool (m_Pool);
	}

	void Start ()
	{
		
	}

	IEnumerator SpawnPlatform ()
	{
		
	}
}