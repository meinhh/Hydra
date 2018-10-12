using System;
namespace Hydra
{
    public interface ISecretSettings 
    {
        string MapCredantials { get; set; }

        string DbPassword { get; set; }
    }

    public class SecretSettings : ISecretSettings
    {
        public SecretSettings(string mapCredantials, string dbPassword)
        {
            DbPassword = dbPassword;
            MapCredantials = mapCredantials;
        }

        public string MapCredantials { get; set; }

        public string DbPassword { get; set; }
    }
}
