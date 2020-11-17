using Bogus;
using NerdStore.WebApp.MVC;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace NerdStore.WebApp.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationWebTestFixtureCollection))]
    public class IntegrationWebTestFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupWebTests>> { }

    [CollectionDefinition(nameof(IntegrationApiTestFixtureCollection))]
    public class IntegrationApiTestFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public string AntiForgeryFieldName = "__RequestVerificationToken";

        public readonly LojaAppFactory<TStartup> Factory;
        public HttpClient Client;

        public string UsuarioEmail;
        public string UsuarioSenha;

        public IntegrationTestsFixture()
        {
            Factory = new LojaAppFactory<TStartup>();
            Client = Factory.CreateClient();
        }

        public void GerarUserSenha()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email().ToLower();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public string ObterAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch = Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type= ""hidden"" value=""([^'']+)"" \/\> ");

            if (requestVerificationTokenMatch.Success)
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;

            throw new ArgumentException($"Anti forgery token {AntiForgeryFieldName} não encontrado no html", nameof(htmlBody));
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
