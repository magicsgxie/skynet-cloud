using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Common
{
    public static class FormulaUtils
    {
        public static PhraseStorage SplitFormula(this string formula)
        {
            PhraseStorage ps = new PhraseStorage();
            PhraseAnalyzer pa = new PhraseAnalyzer(formula + "#=", ref ps);
            if (pa.Succeed == false)
            {
                throw new Exception(string.Format("公式“{0}”错误", formula)); ;
            }
            SemanticAnalyzer sa = new SemanticAnalyzer(ref ps);
            if (sa.Check() == false)
            {
                throw new Exception(string.Format("公式“{0}”有误，{1}", formula, sa.ErrorTip)); ;
            }

            return ps;
        }

        /// <summary>
        /// 将A/B/C拆分成(A/B)/C 后处理除号成DECODE函数
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="phraseTypeList"></param>
        /// <param name="ifZero"></param>
        /// <returns></returns>
        public static string PhraseStorageDecode(PhraseStorage ps, List<PhraseTypeEnum> phraseTypeList, decimal ifZero)
        {
            StringCollection psCopy = new StringCollection();
            foreach (string sCopy in ps.PhraseResult)
            {
                psCopy.Add(sCopy);
            }
            List<PhraseTypeEnum> phraseList = new List<PhraseTypeEnum>(ps.PhraseTypeResult);

            for (int i = 0; i < phraseList.Count; i++)
            {
                if (phraseList[i] == PhraseTypeEnum.divide)
                {
                    int tmp = 0;
                    int n = i - 1;
                    for (; n > 0; n--)
                    {
                        if (phraseList[n] == PhraseTypeEnum.rightbracket)
                        {
                            tmp++;
                        }
                        else if (phraseList[n] == PhraseTypeEnum.leftbracket)
                        {
                            tmp--;
                            if (tmp == 0)
                                break;
                        }
                        else if (phraseList[n] == PhraseTypeEnum.number)
                        {
                            if (tmp == 0)
                                break;
                        }
                        else
                        {
                            if (tmp == 0)
                            {
                                n = n + 1;
                                break;
                            }
                        }
                    }
                    tmp = 0;
                    int k = i + 1;
                    for (; k < phraseList.Count; k++)
                    {
                        if (phraseList[k] == PhraseTypeEnum.rightbracket)
                        {
                            tmp--;
                            if (tmp == 0)
                                break;
                        }
                        else if (phraseList[k] == PhraseTypeEnum.leftbracket)
                        {
                            tmp++;
                        }
                        else if (phraseList[k] == PhraseTypeEnum.number)
                        {
                            if (tmp == 0)
                            {
                                k = k + 1;
                                break;
                            }
                        }
                        else
                        {
                            if (tmp == 0)
                                break;
                        }

                    }
                    if (k == psCopy.Count)
                    {
                        psCopy.Add(")");
                        phraseList.Add(PhraseTypeEnum.rightbracket);
                    }
                    else
                    {
                        psCopy.Insert(k, ")");
                        phraseList.Insert(k, PhraseTypeEnum.rightbracket);
                    }
                    psCopy.Insert(n, "(");
                    phraseList.Insert(n, PhraseTypeEnum.leftbracket);
                    i = k + 1;

                }
            }


            return Formula2Decode(psCopy, phraseList, 0, psCopy.Count, ifZero);
        }

        /// <summary>
        /// 将A/B处理成DECODE(B,0,0,A/B)方式
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="phraseTypeList"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="ifZero"></param>
        /// <returns></returns>
        private static string Formula2Decode(StringCollection sc, List<PhraseTypeEnum> phraseTypeList, int startIndex, int endIndex, decimal ifZero)
        {
            int i = startIndex;
            int posCount = 0;
            int startIndexTmp = 0;
            List<FormulaInfoHelp> formulaInfoList = new List<FormulaInfoHelp>();
            for (; i < endIndex; i++)
            {
                if (phraseTypeList[i] == PhraseTypeEnum.leftbracket)
                {
                    if (posCount == 0)
                        startIndexTmp = i;
                    posCount++;
                }
                else if (phraseTypeList[i] == PhraseTypeEnum.rightbracket)
                {
                    posCount--;
                    if (posCount == 0)
                    {
                        string s = "(" + Formula2Decode(sc, phraseTypeList, startIndexTmp + 1, i, ifZero) + ")";
                        formulaInfoList.Add(new FormulaInfoHelp { Phrase = PhraseTypeEnum.number, PhraseResult = s });
                    }
                }
                else if (phraseTypeList[i] == PhraseTypeEnum.sharp)
                {
                }
                else
                {
                    if (posCount == 0)
                    {
                        formulaInfoList.Add(new FormulaInfoHelp { Phrase = phraseTypeList[i], PhraseResult = sc[i].ToUpper() });
                    }
                }
            }

            i = 0;
            StringBuilder sb = new StringBuilder();
            int n = 0;
            int sIndex = 0;
            int k = formulaInfoList.Count;
            bool hasDivide = false;
            for (; i < formulaInfoList.Count; i++)
            {
                if (formulaInfoList[i].Phrase == PhraseTypeEnum.divide)
                {
                    hasDivide = true;
                    int tmp = 0;
                    n = i - 1;
                    for (; n >= 0; n--)
                    {
                        if ((formulaInfoList[n].Phrase == PhraseTypeEnum.leftbracket))
                        {
                            tmp++;
                        }
                        if ((formulaInfoList[n].Phrase == PhraseTypeEnum.rightbracket))
                        {
                            tmp--;
                        }
                        if (!(formulaInfoList[n].Phrase == PhraseTypeEnum.mutiple))
                        {
                            if (tmp == 0)
                            {
                                break;
                            }
                        }
                    }

                    k = i + 1;
                    tmp = 0;
                    for (; k < formulaInfoList.Count; k++)
                    {
                        if ((formulaInfoList[n].Phrase == PhraseTypeEnum.leftbracket))
                        {
                            tmp++;
                        }
                        if ((formulaInfoList[n].Phrase == PhraseTypeEnum.rightbracket))
                        {
                            tmp--;
                        }
                        if (!(formulaInfoList[k].Phrase == PhraseTypeEnum.mutiple))
                        {
                            if (tmp == 0)
                            {
                                break;
                            }
                        }
                    }
                }

                if (hasDivide)
                {
                    StringBuilder sb1 = new StringBuilder();
                    for (int i1 = i + 1; i1 <= k; i1++)
                    {
                        sb1.Append(" " + formulaInfoList[i1].PhraseResult + " ");
                    }
                    double d = 0;
                    if (!double.TryParse(sb1.ToString(), out d))
                    {

                        for (; sIndex < n; sIndex++)
                        {
                            sb.Append(" " + formulaInfoList[sIndex].PhraseResult + " ");
                        }
                        sb.Append("decode(");
                        for (int i1 = i + 1; i1 <= k; i1++)
                        {
                            sb.Append(" " + formulaInfoList[i1].PhraseResult + " ");
                        }
                        sb.Append(",0,");
                        sb.Append(ifZero);
                        sb.Append(",");
                        for (int i1 = sIndex; i1 <= k; i1++)
                        {
                            sb.Append(" " + formulaInfoList[i1].PhraseResult + " ");
                        }
                        sb.Append(")");

                        sIndex = k + 1;
                        i = k;
                        hasDivide = false;

                    }
                    else
                    {
                        for (; sIndex < k + 1; sIndex++)
                        {
                            sb.Append(" " + formulaInfoList[sIndex].PhraseResult + " ");
                        }
                        sIndex = k + 1;
                        i = k;
                        hasDivide = false;

                    }
                }
            }

            for (; sIndex < formulaInfoList.Count; sIndex++)
            {
                sb.Append(" " + formulaInfoList[sIndex].PhraseResult + " ");
            }

            return sb.ToString();
        }
    }

    internal class FormulaInfoHelp
    {
        public PhraseTypeEnum Phrase
        {
            get;
            set;
        }

        public string PhraseResult
        {
            get;
            set;
        }
    }
}
