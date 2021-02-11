using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Security.Cryptography;

namespace Controlador_Feel_For_Real
{
    class mobo
    {
        /// <summary>
        /// Get volume serial number of drive C
        /// </summary>
        /// <returns></returns>
        public static string RawId { get; set; }
        
        /// <summary>
        /// Get motherboard serial number
        /// </summary>
        /// <returns></returns>
        private static string GetMotherboardID()
        {

            try
            {
                ManagementObjectSearcher _mbs = new ManagementObjectSearcher("Select SerialNumber From Win32_BaseBoard");
                ManagementObjectCollection _mbsList = _mbs.Get();
                string _id = string.Empty;
                foreach (ManagementObject _mo in _mbsList)
                {
                    _id = _mo["SerialNumber"].ToString();
                    break;
                }

                return _id;
            }
            catch
            {
                return string.Empty;
            }

        }

        private static IEnumerable<string> SplitInParts(string input, int partLength)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (int i = 0; i < input.Length; i += partLength)
                yield return input.Substring(i, Math.Min(partLength, input.Length - i));
        }

        /// <summary>
        /// Combine CPU ID, Disk C Volume Serial Number and Motherboard Serial Number as device Id
        /// </summary>
        /// <returns></returns>
        public static string GenerateUID()
        {
            //Combine the IDs and get bytes
            string _id = string.Concat( GetMotherboardID());
            RawId = _id;
            byte[] _byteIds = Encoding.UTF8.GetBytes(_id);

            //Use MD5 to get the fixed length checksum of the ID string
            MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
            byte[] _checksum = _md5.ComputeHash(_byteIds);

            //Convert checksum into 4 ulong parts and use BASE36 to encode both
            string _part1Id = b36.Encode(BitConverter.ToUInt32(_checksum, 0));

            //Concat these 4 part into one string
            return string.Format("{0}", _part1Id);
        }
        

        public static byte[] GetUIDInBytes(string UID)
        {
            //Split 4 part Id into 4 ulong
            string[] _ids = UID.Split('-');

            if (_ids.Length != 4) throw new ArgumentException("Wrong UID");

            //Combine 4 part Id into one byte array
            byte[] _value = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(b36.Decode(_ids[0])), 0, _value, 0, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(b36.Decode(_ids[1])), 0, _value, 8, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(b36.Decode(_ids[2])), 0, _value, 16, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(b36.Decode(_ids[3])), 0, _value, 24, 8);

            return _value;
        }

        public static bool ValidateUIDFormat(string UID)
        {
            if (!string.IsNullOrWhiteSpace(UID))
            {
                string[] _ids = UID.Split('-');

                return (_ids.Length == 4);
            }
            else
            {
                return false;
            }

        }
    }
}
