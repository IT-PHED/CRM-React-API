using System.Security.Cryptography;
using System.Text;
using Application.Abstractions.Authentication;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Authentication;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5350:Do Not Use Weak Cryptographic Algorithms", Justification = "Legacy system compatibility required")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5358:Do Not Use Unsafe Cipher Modes", Justification = "Legacy system compatibility required")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "S5547:Cryptography: Cipher algorithms should be robust", Justification = "Legacy system compatibility required")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5351: Encrypt uses a broken cryptographic algorithm MD5", Justification = "Legacy system compatibility required")]
internal sealed class Tripartite(IConfiguration configuration) : ITripartite
{

    public string Encrypt(string password)
    {
        string hashKey = configuration["Jwt:HashKey"]!;
        byte[] toEncryptArray = Encoding.UTF8.GetBytes(password);

        // Compute MD5 hash of the key
        byte[] keyArray = MD5.HashData(Encoding.UTF8.GetBytes(hashKey));

        using var desProvider = TripleDES.Create();

        desProvider.Key = keyArray;

        // Suppress the third-party S5547 warning here
        desProvider.Mode = CipherMode.ECB;
        desProvider.Padding = PaddingMode.PKCS7;

        // Encrypt the data
        ICryptoTransform encryptor = desProvider.CreateEncryptor();
        byte[] encryptedBytes = encryptor.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        // Base64 encode the encrypted data
        string base64EncodedData = Convert.ToBase64String(encryptedBytes);

        return base64EncodedData;
    }
}
