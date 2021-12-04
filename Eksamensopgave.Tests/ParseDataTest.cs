using Xunit;

namespace Eksamensopgave.Tests
{
    public class ParseDataTest
    {
        /*
           The name of your test should consist of three parts:
           * The name of the method being tested.
           * The scenario under which it's being tested.
           * The expected behavior when the scenario is invoked.
       */
        [Theory]
        [InlineData("<h2>\"name</h2>", "name")]
        [InlineData("<h1>\"name 1,5\"</h1>", "name 1,5")]
        [InlineData("\"<b>name      text<b>", "name      text")]
        [InlineData("<b>name 4<></b>\"", "name 4<>")]
        [InlineData("\"<h1><name</h1>\"", "<name")]
        [InlineData("\" name \"", " name ")]
        [InlineData("\"Budget kildevand incl.pant\"", "Budget kildevand incl.pant")]
        [InlineData("\"HARBOE - ½L Vand excl.pant\"", "HARBOE - ½L Vand excl.pant")]
        [InlineData("\"Island <blink><b>TILBUD!</blink> \"", "Island TILBUD! ")]
        [InlineData("\"Island <blink><b >TILBUD!</ blink> \"", "Island TILBUD! ")]
        [InlineData("\"Island <blink>< b >TILBUD!</ blink > \"", "Island TILBUD! ")]
        [InlineData("\"Island <blink>< b >\"TILBUD!\"</ blink > \"", "Island TILBUD! ")]

        public void ParseProduct_FormatNameCorrectly_ProductWithCorrectName(string name, string expectedName)
        {
            // Arrange
            string[] values = new string[4];
            values[0] = "1";
            values[1] = name;
            values[2] = "12,3";
            values[3] = "1";

            // Act
            Product actual = ParseData.ParseProduct(values);

            // Assert
            Assert.Equal(actual.Name, expectedName);
        }
    }
}
