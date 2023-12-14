using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using RazorPagesTestSample.Data;

namespace RazorPagesTestSample.Tests.UnitTests
{
    public class MessageTests
    {
        private static Message CreateMessageWithTextLength(int length)
        {
            return new Message { Text = new string('a', length) };
        }

        public static IEnumerable<object[]> MessageLengths =>
            new List<object[]>
            {
            new object[] { 199, true },
            new object[] { 200, true },
            new object[] { 201, false },
            new object[] { 249, true },
            new object[] { 250, true },
            new object[] { 251, false },
            };

        [Theory]
        [MemberData(nameof(MessageLengths))]
        public void TestMessageTextLength(int length, bool isValid)
        {
            var message = CreateMessageWithTextLength(length);
            var validationContext = new ValidationContext(message);
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(message, validationContext, validationResults, true);

            Assert.Equal(isValid, actual);
        }
    }
}