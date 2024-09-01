﻿using Newtonsoft.Json;

namespace Epro.Models
{
    public class Cart
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int Quantity { get; set; }

        public int Price { get; set; }
    }

    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            string serializedValue = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedValue);
        }
        public static T GetObject<T>(this ISession session, string key)
        {
            string serializedValue = session.GetString(key);
            if (serializedValue != null)
            {
                return JsonConvert.DeserializeObject<T>(serializedValue);
            }
            return default(T);
        }
    }

}
