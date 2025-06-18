using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace KriptoFinal.Controllers
{
    public class GenerateKeyController : Controller
    {
        public IActionResult Index()
        {
            // RSA anahtar çifti üret
            using var rsa = RSA.Create(2048);

            var publicKey = rsa.ExportRSAPublicKey();
            var privateKey = rsa.ExportRSAPrivateKey();

            // Base64 ile stringe çevir
            string publicKeyString = Convert.ToBase64String(publicKey);
            string privateKeyString = Convert.ToBase64String(privateKey);

            ViewBag.PublicKey = publicKeyString;
            ViewBag.PrivateKey = privateKeyString;

            return View();
        }
    }
}
