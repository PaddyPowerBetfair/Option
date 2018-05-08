
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Option.Tests.Unit
{
    [TestClass]
    public class OptionExTests
    {
        [TestMethod]
        public void OptionExToNullable()
        {
            var counter = Option.Some(new Counter()).ToNullable();

            Assert.IsTrue(counter.HasValue);
            Assert.AreEqual(0, counter.Value.Item);
            Assert.AreEqual(0, counter.Value.Count);
        }

        [TestMethod]
        public void OptionExToNullableWithItems()
        {
            var counter = Option.Some(new Counter{Count = 1, Item = 2}).ToNullable();

            Assert.IsTrue(counter.HasValue);
            Assert.AreEqual(2, counter.Value.Item);
            Assert.AreEqual(1, counter.Value.Count);
        }

        [TestMethod]
        public void OptionExSelectSome()
        {
            var option = Option.Some("test")
                .Select(item => item.Length);

            Assert.IsTrue(option.HasValue);
            Assert.AreEqual(4, option.Value);
        }

        [TestMethod]
        public void OptionExSelectNone()
        {
            var option = Option.None<string>()
                .Select(item => item.Length);

            Assert.IsTrue(option.IsNone);
        }
        
        // TODO FINISH TESTS FOR THIS

        //[TestMethod]
        //public void OptionExSelectionMany()
        //{

        //}
    }

    public struct Counter
    {
        public int Item;
        public int Count;
    }

}
