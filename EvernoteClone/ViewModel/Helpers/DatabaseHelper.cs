using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static readonly string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        //Insert list into table
        public static bool Insert<T>(T item)
        {
            bool result = false;

            //Create connection to database and setup table
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rows = connection.Insert(item);
                if (rows > 0)
                {
                    result = true;
                }
            };

            return result;
        }

        //Read list from table
        public static List<T> Read<T>() where T : new()
        {
            List<T> items;

            //Create connection to database and setup table
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                items = connection.Table<T>().ToList();
            };

            return items;
        }
    }
}