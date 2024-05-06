using System;
namespace eMKParty.BackOffice.Support.Application.Interfaces
{
	public interface IAesOperation
	{
        string EncryptString(string key, string plainText);
        string DecryptString(string key, string cipherText);
        string Base64Encode(string plainText);
        string Base64Decode(string base64EncodedData);
    }
}