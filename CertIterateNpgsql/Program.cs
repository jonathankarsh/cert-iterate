using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CertIterateNpgsql
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var storelocation in (StoreLocation[])Enum.GetValues(typeof(StoreLocation)))
            {
                foreach (var storename in (StoreName[])Enum.GetValues(typeof(StoreName)))
                {
                    var store = new X509Store(storename, storelocation);
                    try
                    {
                        store.Open(OpenFlags.OpenExistingOnly);
                    }
                    catch (CryptographicException)
                    {
                        // ignore
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed enumeration store: {0}, {1}: {2}", storename, storelocation, e);
                    }
                    try
                    {
                        Console.WriteLine("Found {0} certificates in store: {1}, {2}", store.Certificates.Count, storename, storelocation);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed printing store details: {0}, {1}: {2}", storename, storelocation, e);
                    }
                }
            }
        }
    }
}
