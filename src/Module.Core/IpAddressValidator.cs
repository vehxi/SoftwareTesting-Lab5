using System;
using Lab.Interfaces;

namespace Lab.Implementations
{
    public class IpAddressValidator : IIpAddressValidator
    {
        public bool IsValidIpv4(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return false;

            // Strict whitespace check as suggested in Lab 1 report
            if (ipAddress.Contains(" "))
                return false;

            string[] parts = ipAddress.Split('.');
            if (parts.Length != 4)
                return false;

            foreach (string part in parts)
            {
                if (string.IsNullOrEmpty(part))
                    return false;

                if (!int.TryParse(part, out int octet))
                    return false;

                if (octet < 0 || octet > 255)
                    return false;

                // Leading zeros check
                if (part.Length > 1 && part.StartsWith("0"))
                    return false;
            }

            return true;
        }

        public string GetIpClass(string ipAddress)
        {
            if (!IsValidIpv4(ipAddress))
                throw new ArgumentException($"IP-адрес '{ipAddress}' не является валидным IPv4-адресом.", nameof(ipAddress));

            string[] parts = ipAddress.Split('.');
            int firstOctet = int.Parse(parts[0]);

            if (firstOctet >= 1 && firstOctet <= 126)
                return "A";
            else if (firstOctet >= 128 && firstOctet <= 191)
                return "B";
            else if (firstOctet >= 192 && firstOctet <= 223)
                return "C";
            else if (firstOctet >= 224 && firstOctet <= 239)
                return "D";
            else if (firstOctet >= 240 && firstOctet <= 255)
                return "E";
            else
                throw new ArgumentException($"Первый октет '{firstOctet}' не принадлежит ни одному классу IPv4 (A-E).", nameof(ipAddress));
        }
    }
}
