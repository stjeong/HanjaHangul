using System.Diagnostics;
using System.Text;
using System.Unicode;

namespace HanConv
{
    public static class Hanja2Hangul
    {
        public static List<string> MakeSplittedHanjaList(this string str)
        {
            List<string> list = new List<string>();

            if (string.IsNullOrEmpty(str))
            {
                return list;
            }

            str = str.Normalize(NormalizationForm.FormC);

            bool previous = str[0].IsHanja();
            List<char> word = new List<char>();

            word.Add(str[0]);

            foreach (char ch in str[1..])
            {
                bool current = ch.IsHanja();

                if (current != previous)
                {
                    list.Add(new string(word.ToArray()));
                    word.Clear();
                }

                word.Add(ch);
                previous = current;
            }

            if (word.Count > 0)
            {
                list.Add(new string(word.ToArray()));
            }

            return list;
        }

        public static string HanjaToHangulDueum(this string str)
        {
            str = str.Normalize(NormalizationForm.FormC);

            List<string> splitted = MakeSplittedHanjaList(str);
            StringBuilder sb = new StringBuilder();

            foreach (string word in splitted)
            {
                if (word.IsHanja())
                {
                    sb.Append(MakeDueum(HanjaToHangulSimple(word)));
                }
                else
                {
                    sb.Append(word);
                }
            }

            return sb.ToString();
        }

        private static string MakeDueum(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            char[] chArray = str.ToArray();
            chArray[0] = make_dueum_a_char(chArray[0]);
            if (chArray.Length == 1)
            {
                return $"{chArray[0]}";
            }

            char previous = chArray[0];
            for (int i = 1; i < chArray.Length; i++)
            {
                char ch = chArray[i];

                if (ch == '렬' && is_vowel_or_nieun(previous))
                {
                    chArray[i] = '열';
                }
                else if (ch == '률' && is_vowel_or_nieun(previous))
                {
                    chArray[i] = '율';
                }

                previous = ch;
            }

            return new string(chArray);
        }

        private static bool is_vowel_or_nieun(char ch)
        {
            if (ch.IsHangul() == false)
            {
                return false;
            }

            int n = ((int)ch - 0xAC00) % 28;
            if (n == 0 || n == 4)
            {
                return true;
            }

            return false;
        }

        private static char make_dueum_a_char(char ch)
        {
            if (ch.IsHangul() == false)
            {
                return ch;
            }

            if (HanConv.HanDict.Dueum.ContainsKey(ch))
            {
                return HanConv.HanDict.Dueum[ch];
            }

            return ch;
        }

        private static string HanjaToHangulSimple(string str)
        {
            char[] chArray = new char[str.Length];

            for (int i = 0; i < chArray.Length; i ++)
            {
                chArray[i] = HanConv.HanDict.Hanja[str[i]];
            }

            return new string(chArray);
        }

        public static bool IsHangul(this char ch)
        {
            return UnicodeInfo.GetName(ch).IndexOf("HANGUL") != -1;
        }

        public static bool IsHangul(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            return str.Length == len_hangul(str);
        }

        private static int len_hangul(string str)
        {
            int count = 0;

            foreach (char c in str)
            {
                if (c.IsHangul())
                {
                    count++;
                }
            }

            return count;
        }

        public static bool IsHanja(this char ch)
        {
            return UnicodeInfo.GetName(ch) switch
            {
                String txt when txt.Length >= 3 && txt[0..3] == "CJK" => true,
                String txt when txt.Length >= 6 && txt[0..6] == "KANGXI" => true,
                _ => false,
            };
        }

        public static bool IsHanja(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            return str.Length == len_hanja(str);
        }

        private static int len_hanja(string str)
        {
            int count = 0;

            foreach (char c in str)
            {
                if (c.IsHanja())
                {
                    count++;
                }
            }

            return count;
        }
    }
}
