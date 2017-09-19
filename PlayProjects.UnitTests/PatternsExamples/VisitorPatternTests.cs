using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayProjects.UnitTests
{

    [TestClass]
    public class VisitorPatternTests
    {

        [TestInitialize()]
        public void MyTestInitialize()
        {

        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }

    public interface IVisitor
    {
        void Visit(Book book);
        //void Visit(CD cd);
        //void Visit(DVD dvd);
    }

    public class Book : IVisitable
    {
        public double Price { get; set; }
        public double Weight { get; set; }

        void IVisitable.Accept(IVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }

}
