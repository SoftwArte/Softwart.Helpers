using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Softwarte.Helpers
{
  [TestClass]
  public class CryptoTest
  {
    [TestMethod]
    public void EncryptAesTest()
    {
      var input = "This is my sample text for AES encryptiobn test.";
      var result = CryptoHelper.EncryptStrongAes(input, "aEyf7TSUad@94D#@y784");
    }
    [TestMethod]
    public void DecryptAesTest()
    {
      var input = "This is my sample text for AES encryptiobn test.";
      var encryptedInput = CryptoHelper.EncryptStrongAes(input, "aEyf7TSUad@94D#@y784");
      var result = CryptoHelper.DecryptStrongAes(encryptedInput, "aEyf7TSUad@94D#@y784");
      Assert.IsTrue(result == input);
    }
  }
}
