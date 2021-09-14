using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;

struct UserAttributes
{
    public int level;
}

struct AppAttributes
{

}

public class ConfigFetcher : MonoBehaviour
{
    const string Environment_Production  = "802b5a44-318d-431f-b934-059584bb0617";
    const string Environment_Development = "6dfc70b2-ffa5-4643-8f13-41d82932f4ef";

    [SerializeField] bool IsDevelopment = true;
    [SerializeField] int Level = 1;

    // Start is called before the first frame update
    void Start()
    {
        // link to the apply handler
        ConfigManager.FetchCompleted += ApplyRemoteSettings;

        // request the settings
        FetchSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FetchSettings()
    {
        // set the environment
        if (IsDevelopment)
            ConfigManager.SetEnvironmentID(Environment_Development);
        else
            ConfigManager.SetEnvironmentID(Environment_Production);

        // setup the user and app attributes
        var userAttrib = new UserAttributes();
        userAttrib.level = Level;
        var appAttrib = new AppAttributes();

        // request the configuration
        ConfigManager.FetchConfigs<UserAttributes, AppAttributes>(userAttrib, appAttrib);
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        // did we get back no response (ie. use defaults) or cached data?
        if (configResponse.requestOrigin == ConfigOrigin.Default ||
            configResponse.requestOrigin == ConfigOrigin.Cached)
        {
            Debug.Log("No change. Defaults or cached.");
        } // did we receive new remote data?
        else if (configResponse.requestOrigin == ConfigOrigin.Remote)
        {
            var verboseLogs = ConfigManager.appConfig.GetBool("verbose_logs");
            Debug.Log(verboseLogs);
        }
    }
}
