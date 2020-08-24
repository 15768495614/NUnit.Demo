using NUnit.Demo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace NUnit.Demo.Tests
{
    /* 生命周期
     ctor
    one-time setup

    setup
    test1
    tear down
    
    setup
    test2
    tear down

    one-time tear down
    dispose
     */

    /// <summary>
    /// 标记测试的容器
    /// </summary>
    [TestFixture(Author = "梁灿林", Category = "culc", Description = "计算")]
    public class OperateTests
    {
        [Test(Author = "梁灿林", Description = "加法", ExpectedResult = 3)]
        public int AddTest()
        {
            var result = Operate.Add(1, 2);
            Assert.That(result, Is.GreaterThan(2));
            Assert.That(result, Is.EqualTo(3));
            return result;
        }

        [Test()]
        public void ToMonthsTest()
        {
            var o = new Operate(2);
            var result = o.ToMonths();
            Assert.That(result, Is.EqualTo(12), "期望值不相等");//定义消息
        }

        private List<Operate> list1;

        /// <summary>
        /// 每次测试运行前都会运行此方法
        /// </summary>
        [SetUp]
        public void Setup()
        {
            list1 = new List<Operate> {
                 new Operate(1),
               new Operate(2),
               new Operate(3),
               new Operate(3),
            };
        }

        /// <summary>
        /// 每次测试执行后运行
        /// </summary>
        [TearDown]
        public void TearDwon()
        {
            //list1 dispose
        }

        /// <summary>
        /// 
        /// </summary>
        [OneTimeSetUp]//第一次运行
        //[Sequential]//How to combine test data
        public void OneTimeSetUp()
        {
            //simulate long setup init time for this list of products
            //we assume that this list will not be modified by any tests
            // as this will potentially break other tests(i.e. break test isolation)
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //run after last test in this test class (fixture) executes
            //e.g. disposing of shared expensive setup performed in OneTimeSetup
            // products.Dispose(); e.g. if products implemented IDisposable
        }

        [Test()]
        [Ignore("忽略，不再测试")]
        public void InitOperateException()
        {
            //Assert.That(() => new Operate(-1), Throws.TypeOf<ArgumentOutOfRangeException>().With.Property("Message").EqualTo("years值无效\r\n参数名: years"));
            //Assert.That(() => new Operate(-1), Throws.TypeOf<ArgumentOutOfRangeException>().With.Message.EqualTo("years值无效\r\n参数名: years"));         
            Assert.That(() => new Operate(-1), Throws.TypeOf<ArgumentOutOfRangeException>().With.Property("ParamName").EqualTo("years"));
            Assert.That(() => new Operate(-1), Throws.TypeOf<ArgumentOutOfRangeException>().With.Matches<ArgumentOutOfRangeException>(x => x.ParamName == "years"));

        }

        [Test]
        public void ObjEquals()
        {
            var x = new List<string> { "a", "b" };
            var y = x;
            var z = new List<string> { "a", "b" };

            Assert.That(x, Is.SameAs(y));//引用相等
            Assert.That(x, Is.Not.SameAs(z));
        }

        [Test]
        public void ValueEquals()
        {
            var o1 = new Operate(1);
            var o2 = new Operate(1);
            Assert.That(o1, Is.EqualTo(o2));//值相等
        }

        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;
            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
            Assert.That(a, Is.Not.EqualTo(0.33).Within(0.003));
        }

        [Test]
        public void MulitValueTest()
        {
            var list = new List<Operate> {
               new Operate(1),
               new Operate(1),
               new Operate(1),
            };

            //Assert.That(list, Has.Exactly(3).Property("Years").EqualTo(1).And.Property("aaa"), "不是3条数据");
            Assert.That(list, Has.Exactly(3).Matches<Operate>(x => x.Years == 1));//lambda
        }

        [Test]
        public void MulitValueTest1()
        {
            Assert.That(list1, Is.Unique, "有重复数据");
        }

        [Test]
        public void MulitValueTest2()
        {
            var list = new List<Operate> {
               new Operate(1),
               new Operate(2),
               new Operate(3),
               new Operate(3),
            };
            var a = new Operate(3);
            Assert.That(list, Does.Not.Contain(a), "没有包含");
        }

        [Test(Author = "梁灿林", Description = "加法")]
        [TestCase(1, 1, Author = "梁灿林", ExpectedResult = 2)]
        [TestCase(2, 1, Author = "梁灿林", ExpectedResult = 3)]
        [TestCase(4, 1, Author = "梁灿林", ExpectedResult = 5)]
        public int AddTest1(int a, int b)
        {
            var result = Operate.Add(a, b);
            return result;
        }

        [Test(Author = "梁灿林", Description = "加法")]
        [TestCaseSource(typeof(OperateTests), "TestCases")]
        public int AddTest2(int a, int b)
        {
            var result = Operate.Add(a, b);
            return result;
        }




        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(1, 2).Returns(3);
                yield return new TestCaseData(1, 3).Returns(4);
                yield return new TestCaseData(1, 4).Returns(5);
                yield return new TestCaseData(1, 5).Returns(6);
            }
        }

        [Test(Author = "梁灿林", Description = "")]

        public void AddTest3([Values(1, 2)] int a, [Values(2, 1)] int b)
        {
            var result = Operate.Add(a, b);
            Assert.That(result, Is.LessThanOrEqualTo(4));
        }
    }


}