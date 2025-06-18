using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace KriptoFinal.Controllers
{
    public class RSAOperationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateKeys()
        {
            using var rsa = RSA.Create(2048);

            var publicKey = rsa.ExportRSAPublicKey();
            var privateKey = rsa.ExportRSAPrivateKey();

            string publicKeyString = Convert.ToBase64String(publicKey);
            string privateKeyString = Convert.ToBase64String(privateKey);

            return Json(new { publicKey = publicKeyString, privateKey = privateKeyString });
        }

        [HttpPost]
        public IActionResult Encrypt(string publicKeyBase64, string plainText)
        {
            if (string.IsNullOrWhiteSpace(publicKeyBase64) || string.IsNullOrWhiteSpace(plainText))
            {
                return BadRequest("Public key ve şifrelenecek metin gereklidir.");
            }

            try
            {
                byte[] publicKeyBytes = Convert.FromBase64String(publicKeyBase64);

                using var rsa = RSA.Create();
                rsa.ImportRSAPublicKey(publicKeyBytes, out _);

                var data = Encoding.UTF8.GetBytes(plainText);
                var encryptedBytes = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);

                string encryptedText = Convert.ToBase64String(encryptedBytes);
                return Json(new { encryptedText = encryptedText });
            }
            catch
            {
                return BadRequest("Şifreleme işlemi başarısız oldu.");
            }
        }
    }
} 