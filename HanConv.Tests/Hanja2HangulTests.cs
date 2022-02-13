using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HanConv.Tests
{
    [TestClass()]
    public class Hanja2HangulTests
    {
        [TestMethod()]
        public void MakeSplittedHanjaListTest()
        {
            string input = "家那多羅락락락多羅aaa";
            string[] expected = new string[]
            {
                "家那多羅", "락락락", "多羅", "aaa"
            };

            CollectionAssert.AreEqual(input.MakeSplittedHanjaList(), expected);
        }

        [TestMethod()]
        [DataRow("家那多羅락락락多羅aaa", "가나다라락락락다라aaa")]
        [DataRow("歷史 圓周率 確率 女子", "역사 원주율 확률 여자")]
        [DataRow("申砬", "신립")]
        [DataRow("羅列", "나열")]
        [DataRow("雲量", "운량")]
        [DataRow("羅州", "나주")]
        [DataRow("靈巖", "영암")]
        [DataRow("男女", "남녀")]
        [DataRow("讀者欄", "독자란")]
        [DataRow("隱匿", "은닉")]
        [DataRow("李成桂", "이성계")]
        public void HanjaToHangulDueumTest(string input, string expected)
        {
            Assert.AreEqual(input.HanjaToHangulDueum(), expected);
        }

        [TestMethod()]
        [DataRow("新女性", "신여성", "신녀성")]
        [DataRow("國際聯合", "국제연합", "국제련합")]
        [DataRow("空念佛", "공염불", "공념불")]
        [DataRow("會計年度", "회계연도", "회계년도")]
        [DataRow("許蘭雪軒", "허난설헌", "허란설헌")]
        [DataRow("失樂園", "실낙원", "실락원")]
        [DataRow("銃榴彈", "총유탄", "총류탄")]
        public void HanjaToHangulDueumTest_ToImprove(string input, string myExpected, string result)
        {
            Assert.AreEqual(input.HanjaToHangulDueum(), result);
            Assert.AreNotEqual(input.HanjaToHangulDueum(), myExpected);
        }
    }
}