using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class Program
    {
        static void Main(string[] args)
        {
            new TestClass().SampleFunction();

            new TestClass2("Test").ExecutedMethod();
            new TestClass2("Test").SampleFunction("Munich");

            new PartialClass().ExecutedMethod_1();
            new PartialClass().ExecutedMethod_2();
            new PartialClass().SomeProperty = -10;

            new PartialClassWithAutoProperties().Property1 = "Test";
            new PartialClassWithAutoProperties().Property2 = "Test";

            new SomeClass().Property1 = "Test";

            new ClassWithExcludes().IncludedMethod();
            new ClassWithExcludes().ExcludedMethod();

            new GenericClass<SomeModel, IState>().Process(null);
            new GenericClass<SomeModel, IState>().PostProcess(null);

            new CodeContract_Target().Calculate(-1);

            new AbstractClass_SampleImpl1();
            new AbstractClass_SampleImpl2();

            CallAsyncMethod();

            try
            {
                new CodeContract_Target().Calculate(0);
            }
            catch (System.ArgumentException)
            {
            }
        }

        [TestMethod]
        public void CSharp_ExecuteTest1()
        {
            Main(null);
        }

        [TestMethod]
        public void CSharp_ExecuteTest2()
        {
            Main(null);
        }

        private static async void CallAsyncMethod()
        {
            var expected = new HttpResponseMessage();
            var handler = new AsyncClass() { InnerHandler = new EchoHandler(expected) };
            var invoker = new HttpMessageInvoker(handler, false);
            var actual = await invoker.SendAsync(new HttpRequestMessage(), new CancellationToken());
        }

        private class EchoHandler : DelegatingHandler
        {
            private HttpResponseMessage _response;

            public EchoHandler(HttpResponseMessage response)
            {
                this._response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(this._response);
            }
        }
    }
}
