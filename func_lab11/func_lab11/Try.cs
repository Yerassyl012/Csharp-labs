using System;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace func_lab11.Chapter11
{
    public static class TryTests
    {
        static Exceptional<Uri> Boilerplate_CreateUri(string uri)
        {
            try { return new Uri(uri); }
            catch (Exception ex) { return ex; }
        }


        static Try<Uri> CreateUri(string uri) => () => new Uri(uri);

        static Try<JObject> Parse(string s) => () => JObject.Parse(s);

        static Try<Uri> ExtractUri(string json) =>
           from jObj in Parse(json)
           let uriStr = (string)jObj["Uri"]
           from uri in Try(() => new Uri(uriStr))
           select uri;

        [TestCase(@"{'Uri': 'http://github.com'}", "Ok")]
        [TestCase("{'Uri': 'rubbish'}", "Invalid URI")]
        [TestCase("{}", "Value cannot be null")]
        [TestCase("blah!", "Unexpected character encountered")]
        public static void SuccessfulTry(string json, string expected)
           => Assert.IsTrue(
              ExtractUri(json)
                 .Run()
                 .Match(
                    ex => ex.Message,
                    _ => "Ok")
                 .StartsWith(expected));
    }
}