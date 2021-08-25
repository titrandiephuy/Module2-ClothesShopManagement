using System;
using System.Collections.Generic;

namespace ClothesShop
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Account
    {
        public string usernames { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}