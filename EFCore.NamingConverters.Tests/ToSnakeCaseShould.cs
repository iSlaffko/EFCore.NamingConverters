using NUnit.Framework;

namespace EFCore.NamingConverters.Tests
{
    public class ToSnakeCaseShould
    {
        [TestCase("Test", ExpectedResult = "test")]
        [TestCase("TestTest", ExpectedResult = "test_test")]
        [TestCase("TestTestTest", ExpectedResult = "test_test_test")]
        [TestCase("Test_Test", ExpectedResult = "test_test")]
        [TestCase("Test Test", ExpectedResult = "test test")]
        [TestCase("Test__Test", ExpectedResult = "test__test")]
        [TestCase("_Test_test", ExpectedResult = "_test_test")]
        [TestCase("", ExpectedResult = "")]
        [TestCase("T", ExpectedResult = "t")]
        [TestCase("test", ExpectedResult = "test")]
        [TestCase("@Te$t", ExpectedResult = "@te$t")]
        [TestCase("12T3st", ExpectedResult = "12_t3st")]
        [TestCase("TEST", ExpectedResult = "test")]
        [TestCase("TestMYTest", ExpectedResult = "test_mytest")]
        public string ConvertToSnakeCase(string str) => str.ToSnakeCase();
    }
}
