using UnityEngine;
using System.Collections;
using Unity.WebRTC;

/// <summary>
/// A persistent singleton that manages the main WebRTC update coroutine.
/// This ensures the coroutine runs for the entire application lifetime.
/// </summary>
public class WebRTCManager : MonoBehaviour
{
	// The static instance of the singleton
	private static WebRTCManager _instance;

	// Public property to access the instance from anywhere
	public static WebRTCManager Instance {
		get {
			// If the instance doesn't exist, create it dynamically
			if (_instance == null)
			{
				// Create a new GameObject and add our manager component to it
				GameObject managerObject = new GameObject("WebRTCManager");
				_instance = managerObject.AddComponent<WebRTCManager>();
			}
			return _instance;
		}
	}

	private bool isCoroutineRunning = false;

	// This method is called when the script instance is being loaded
	private void Awake()
	{
		// --- Singleton Pattern Implementation ---
		// If an instance already exists and it's not this one, destroy this new one.
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		// This is the first instance, so assign it and make it persistent.
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	/// <summary>
	/// Starts the WebRTC processing loop if it is not already running.
	/// </summary>
	public void StartWebRTCLoop()
	{
		if (!isCoroutineRunning)
		{
			
			StartCoroutine(WebRTC.Update());
			isCoroutineRunning = true;
		}
	}

	
}