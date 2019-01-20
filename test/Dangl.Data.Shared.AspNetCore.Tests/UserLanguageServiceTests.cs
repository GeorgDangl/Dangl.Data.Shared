using System.Collections.Generic;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests
{
    public class UserLanguageServiceTests
    {
        private string _acceptLanguageHeader;
        private string _deserializedHeaderLanguage;
        private List<string> _availableLanguages = new List<string> { "en", "de", "fr" };

        private void DeserializeHeader()
        {
            var userLanguageService = new UserLanguageService(null, ".cookie_name", _availableLanguages);
            _deserializedHeaderLanguage = userLanguageService.GetAcceptLanguageFromHeaderOrNull(_acceptLanguageHeader);
        }

        [Fact]
        public void CorrectlyDeserializesAcceptLanguageHeader_01()
        {
            _acceptLanguageHeader = "fr-CH, fr;q=0.9, en;q=0.8, de;q=0.7, *;q=0.5";
            DeserializeHeader();
            Assert.Equal("fr", _deserializedHeaderLanguage);
        }

        [Fact]
        public void CorrectlyDeserializesAcceptLanguageHeader_02()
        {
            _acceptLanguageHeader = "de-CH";
            DeserializeHeader();
            Assert.Equal("de", _deserializedHeaderLanguage);
        }

        [Fact]
        public void CorrectlyDeserializesAcceptLanguageHeader_03()
        {
            _acceptLanguageHeader = "de";
            DeserializeHeader();
            Assert.Equal("de", _deserializedHeaderLanguage);
        }

        [Fact]
        public void CorrectlyDeserializesAcceptLanguageHeader_04()
        {
            _acceptLanguageHeader = "fr;q=0.3, en;q=0.8, de;q=0.7, *;q=0.5";
            DeserializeHeader();
            Assert.Equal("en", _deserializedHeaderLanguage);
        }

        [Fact]
        public void CorrectlyDeserializesAcceptLanguageHeader_05()
        {
            _availableLanguages = new List<string> { "fr" };
            _acceptLanguageHeader = "fr;q=0.3, en;q=0.8, de;q=0.7, *;q=0.5";
            DeserializeHeader();
            Assert.Equal("fr", _deserializedHeaderLanguage);
        }

        [Fact]
        public void CorrectlyDeserializesAcceptLanguageHeader_06()
        {
            _availableLanguages = new List<string> { "pl", "de" };
            _acceptLanguageHeader = "fr;q=0.3, en;q=0.8, de;q=0.7, *;q=0.5";
            DeserializeHeader();
            Assert.Equal("de", _deserializedHeaderLanguage);
        }

        [Fact]
        public void CorrectlyDeserializesAcceptLanguageHeader_07()
        {
            _acceptLanguageHeader = "Total garbage here! Doesn't work!";
            DeserializeHeader();
            Assert.Null(_deserializedHeaderLanguage);
        }

        [Fact]
        public void CorrectlyDeserializesAcceptLanguageHeader_08()
        {
            DeserializeHeader();
            Assert.Null(_deserializedHeaderLanguage);
        }
    }
}
