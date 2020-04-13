using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Project.Models;

namespace Project.Services
{
    public class IdGenerator
    {
        private static TraderModel[] traders = {
            new TraderModel(Twnty('d'), "David", "Pineda"),
            new TraderModel(Twnty('b'), "Ben", "Lokos"),
            new TraderModel(Twnty('c'), "Cole", "Serfass"),
            new TraderModel(Twnty('w'), "Will", "Rinne")
        };

        public static TraderModel DefaultTrader;
        public static void SetRandomUser()
        {
            DefaultTrader = traders[new Random().Next(0,traders.Length)];
        }

        private static string Twnty(char c)
        {
            string tmp = "";
            for (int i = 0; i < 20; i++)
                tmp += c;
            return tmp;
        }

        private static readonly RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

        public string GenerateUniqueID(int length)
        {
            // We chose an encoding that fits 6 bits into every character,
            // so we can fit length*6 bits in total.
            // Each byte is 8 bits, so...
            int sufficientBufferSizeInBytes = (length * 6 + 7) / 8;

            var buffer = new byte[sufficientBufferSizeInBytes];
            random.GetBytes(buffer);
            return Convert.ToBase64String(buffer).Substring(0, length);
        }
    }
}
