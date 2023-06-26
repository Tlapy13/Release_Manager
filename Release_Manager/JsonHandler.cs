using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Configuration;
using Serilog;
using Logging;
using System.Runtime.CompilerServices;

namespace Release_Manager
{
    public class JsonHandler
    {
        public SolutionsConfig SolutionsConfig { get; set; }
        public List<SolutionsConfig> SolutionsConfigs { get; set; } = new List<SolutionsConfig>();

        private readonly string _configurationPath = 
            Path.GetFullPath(
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                .AppSettings.Settings["solutions_config_path"]
                .Value);

        private readonly ILogger _logger = new SerilogClass().logger;

        public bool DeserializeConfigFile()
        {
            bool isDeserialized = false;
            try
            {
                using (FileStream openStream = File.OpenRead(_configurationPath))
                {
                    SolutionsConfigs = JsonSerializer.Deserialize<List<SolutionsConfig>>(openStream);
                    if (SolutionsConfigs.Count > 0)
                    {
                        _logger.Debug($"These solutions were loaded successfully:");
                        SolutionsConfigs.ForEach(solution => _logger.Debug($"{solution}"));
                        isDeserialized = true;
                        return isDeserialized;
                    }
                    else
                        _logger.Debug($"JSON configuration file exists but it does not contain any solution.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while deserializing the JSON configuration file. Message:\n{ex.InnerException.Message}");
                isDeserialized = false;
            }

            return isDeserialized;
            
        }

        public bool SerializeConfigFile()
        {
            bool isSerialized = false;
            try
            {
                using (FileStream createStream = File.Create(_configurationPath))
                {
                    JsonSerializer.Serialize(createStream, SolutionsConfigs);
                    createStream.Dispose(); //TODO: Re-think implementation of Dispose(), because DisposeAsync() was not introduced later, i.e. since .NET Core 3.1 and later
                    createStream.Close();
                };
                if (SolutionsConfigs.Count > 0)
                    _logger.Debug($"JSON serialization ran successfully. In total {SolutionsConfigs.Count} item(s) was/were serialized.");
                else
                    _logger.Debug("List contained no solution. No item was serialized.");
                isSerialized = true;
                
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while serializing the JSON configuration file. Message:\n{ex.InnerException.Message}");
                isSerialized = false;
            }


            return isSerialized;
        }
    }

   
}
