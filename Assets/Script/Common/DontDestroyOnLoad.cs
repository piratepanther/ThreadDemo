using UnityEngine;
using System.Collections;

namespace Common
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
	
	    // Use this for initialization
	    void Awake()
	    {
	        DontDestroyOnLoad(gameObject);
	    }
	
	    
	}
}
