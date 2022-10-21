using UnityEngine;
using System.Runtime.InteropServices; 

public class JSPsychBackend : MonoBehaviour
{

    private float waitTime = 2.0f;
	private float timer = 0.0f;

    // Import javascript functions from 
    // Assets/Plugins/WebGL/JSPsychUnityWebGL.jslib
    //
#if UNITY_WEBGL == true && UNITY_EDITOR == false
	
	[DllImport("__Internal")]
	private static extern void PublishEventJSPsych(string event_json);

	[DllImport("__Internal")]
	private static extern void PublishEventJSPsychUnityReady();

#endif

	// Signal jsPsych that we have started.
	private void Start()
	{
#if UNITY_WEBGL == true && UNITY_EDITOR == false
			PublishEventJSPsychUnityReady();
#endif
	}

	void Update()
    {
		timer += Time.deltaTime;

        if (timer > waitTime)
        {
            SendMessage("{\"channel\": \"log\", \"payload\": {\"time\":\"" + Time.time + "\"}}");
            timer = timer - waitTime;
        }
    }

	new public static void SendMessage(string event_json)
	{
#if UNITY_WEBGL == true && UNITY_EDITOR == false
		PublishEventJSPsych (event_json);
#endif
	}

	// gets called when message is sent from jsPsych
	public void OnMessage(string event_json)
	{
        Debug.Log("Message from jsPsych: " + event_json);
	}
}
