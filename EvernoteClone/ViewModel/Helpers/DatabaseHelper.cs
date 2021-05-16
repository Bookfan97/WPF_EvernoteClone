using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EvernoteClone.Model;
using Newtonsoft.Json;
using SQLite;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");
        private static string dbPath = SecretsHelper.GetFirebaseDBPath();

        //Insert list into table
        public static async Task<bool> Insert<T>(T item)
        {
            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync($"{dbPath}{item.GetType().Name.ToLower()}.json", content);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static async Task<bool> Update<T>(T item) where T : HasID
        {
            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var result = await client.PatchAsync($"{dbPath}{item.GetType().Name.ToLower()}/{item.ID}.json", content);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static async Task<bool> Delete<T>(T item) where T : HasID
        {
            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"{dbPath}{item.GetType().Name.ToLower()}/{item.ID}.json");
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Read list from table
        public static async Task<List<T>> Read<T>() where T : HasID
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{dbPath}{typeof(T).Name.ToLower()}.json");
                var jsonResult = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    var objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);
                    List<T> list = new List<T>();
                    if (objects != null)
                    {
                        foreach (var o in objects)
                        {
                            o.Value.ID = o.Key;
                            list.Add(o.Value);
                        }
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}