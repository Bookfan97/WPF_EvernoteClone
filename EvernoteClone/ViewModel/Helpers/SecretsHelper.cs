using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace EvernoteClone.ViewModel.Helpers
{
    public class SecretsHelper
    {
        public static JsonDocument appSettings = JsonDocument.Parse(File.ReadAllText("../../../secrets.json"), new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });

        public static string GetAzureRegion()
        {
            return appSettings.RootElement.GetProperty("Azure").GetProperty("Region").GetString();
        }

        public static string GetAzureServiceApiKey()
        {
            return appSettings.RootElement.GetProperty("Azure").GetProperty("ServiceApiKey").GetString();
        }

        public static string GetAzureContainerURL()
        {
            return appSettings.RootElement.GetProperty("Azure").GetProperty("ContainerURL").GetString();
        }

        public static string GetAzureContainerName()
        {
            return appSettings.RootElement.GetProperty("Azure").GetProperty("ContainerName").GetString();
        }

        public static string GetAzureConnectionString()
        {
            return appSettings.RootElement.GetProperty("Azure").GetProperty("ConnectionString").GetString();
        }

        public static string GetFirebaseWebApiKey()
        {
            return appSettings.RootElement.GetProperty("Firebase").GetProperty("WebApiKey").GetString();
        }

        public static string GetFirebaseDBPath()
        {
            return appSettings.RootElement.GetProperty("Firebase").GetProperty("dbPath").GetString();
        }
    }
}