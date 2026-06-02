using System;

namespace Lab.Interfaces
{
    public interface IIpAddressValidator
    {
        bool IsValidIpv4(string ipAddress);
        string GetIpClass(string ipAddress);
    }
}
