using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Common
{
    internal class PhraseAnalyzer
    {
        private DFAState _prestate;			//DFA的前一个状态
        private char[] _chArray = null;		//句子变量的字符串形式
        private string _sentence = null;		//句子变量
        private PhraseStorage _ps = null;
        private bool _succeed = true;

        public PhraseAnalyzer(string sentence, ref PhraseStorage ps)
        {
            //清除前一次的词法分析结果
            _ps = ps;
            _ps.ClearResult();
            //保存句子
            _sentence = sentence;
            _succeed = true;
            //小写化句子中的所有字母
            _chArray = sentence.ToLower().ToCharArray();
            if (Analyze() == false)	//出错
                _ps.AddPhraseResult("Error", PhraseTypeEnum.unknown);
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Succeed
        {
            get
            {
                return _succeed;
            }
        }
        /// <summary>
        /// 保存词
        /// </summary>
        /// <param name="startpos">开始位置</param>
        /// <param name="endpos">结束位置</param>
        private void SavePhrase(int startpos, int endpos)
        {
            string temp = null;
            //处理'@'
            if (startpos == endpos && _chArray[startpos] == '@')
            {
                _ps.AddPhraseResult("-", PhraseTypeEnum.less);
                return;
            }
            //处理其他词元
            if (endpos >= 0 && startpos >= 0 && endpos >= startpos)
            {
                //trim()，以防止在startpos到endpos头尾出现空格
                temp = _sentence.Substring(startpos, endpos - startpos + 1).Trim().ToLower();
                _ps.AddPhraseResult(temp, PhraseAnalyzer.StrToType(temp));
            }
        }
        /// <summary>
        /// 转换字符串为所对应的词类
        /// </summary>
        /// <param name="str">词字符串</param>
        /// <returns></returns>
        public static PhraseTypeEnum StrToType(string str)
        {
            switch (str)
            {
                case ">": return PhraseTypeEnum.greater;
                case "<": return PhraseTypeEnum.less;
                case "=": return PhraseTypeEnum.equal;

                //case "sin": return PhraseTypeEnum.sin;
                //case "ln": return PhraseTypeEnum.ln;
                //case "lg": return PhraseTypeEnum.lg;
                //case "log": return PhraseTypeEnum.log;
                //case "cbrt": return PhraseTypeEnum.cbrt;
                case "end": return PhraseTypeEnum.end;
                case "abs": return PhraseTypeEnum.abs;
                case "else": return PhraseTypeEnum.else_;
                case "when": return PhraseTypeEnum.when;
                case "case": return PhraseTypeEnum.case_;
                case "then": return PhraseTypeEnum.then;
                //case "tg": return PhraseTypeEnum.tg;
                //case "atg":return PhraseTypeEnum.atg;
                case "+": return PhraseTypeEnum.plus;
                case "-": return PhraseTypeEnum.minus;
                case "*": return PhraseTypeEnum.mutiple;
                case "/": return PhraseTypeEnum.divide;
                //case "%":return PhraseTypeEnum.mod;
                case "(": return PhraseTypeEnum.leftbracket;
                case ")": return PhraseTypeEnum.rightbracket;
                case "#": return PhraseTypeEnum.sharp;
                //case "ans":return PhraseTypeEnum.ans;
                //case "sto":return PhraseTypeEnum.sto;
                //case "clr":return PhraseTypeEnum.clr;
                //case "ax": return PhraseTypeEnum.ax;
                //case "bx": return PhraseTypeEnum.bx;
                //case "cx": return PhraseTypeEnum.cx;
                //case "dx": return PhraseTypeEnum.dx;
                //case "ex": return PhraseTypeEnum.ex;
                //case "fx": return PhraseTypeEnum.fx;
                //case "e": return PhraseTypeEnum.e;
                //case "pi": return PhraseTypeEnum.pi;
                default: return PhraseTypeEnum.number;
            }
        }
        /// <summary>
        /// 字符串类型检查
        /// </summary>
        /// <param name="startpos">字符串开始位置</param>
        /// <param name="endpos">字符串结束位置</param>
        /// <returns>字符串是否匹配规定范围内容的类型</returns>
        private bool CheckString(int startpos, int endpos)
        {
            //trim()，以防止在startpos到endpos头尾出现空格
            string temp = this._sentence.Substring(startpos, endpos - startpos + 1).Trim().ToLower();
            int len = temp.Length;	//这里的temp.length不一定等于endpos-startpos+1
            if (len == 1)
            {
                //switch (temp)
                //{
                //    case "e":
                //        return true;
                //}
            }
            else if (len == 2)
            {
                //switch (temp)
                //{
                //    case "ln":
                //    case "lg":
                //    case "tg":
                //    case "ax":
                //    case "bx":
                //    case "cx":
                //    case "dx":
                //    case "ex":
                //    case "fx":
                //    case "pi":
                //        return true;
                //}
            }
            else if (len == 3)
            {
                switch (temp)
                {
                    case "end":
                    case "abs":
                        //case "sin":
                        //case "atg":
                        //case "ans":
                        //case "clr":
                        //case "sto":
                        //case "log":
                        return true;
                }
            }
            else if (len == 4)
            {
                switch (temp)
                {
                    case "case":
                    case "when":
                    case "then":
                    case "else":
                        //case "cbrt":
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 词法分析
        /// </summary>
        /// <returns>是否成功</returns>
        private bool Analyze()
        {
            int i = 0;
            int startpos = 0, endpos = 0;
            //设置初态
            _prestate = DFAState.S0;
            while (i < _chArray.Length)
            {
                //未知态处理，出错返回
                if (_prestate == DFAState.SX)
                {
                    _succeed = false;
                    return false;
                }

                if (Char.IsLetter(_chArray[i]))
                {
                    //字母
                    if (_prestate == DFAState.S0)
                    {
                        //初态变字母串
                        _prestate = DFAState.S3;
                    }
                    else if (_prestate == DFAState.S1)
                    {
                        endpos = i - 1;	//保存结束位置

                        ////检查字母串的匹配类型
                        //if (CheckString(startpos, endpos) == true)
                        //{
                        //    SavePhrase(startpos, endpos);
                        //    startpos = i;
                        //}
                        _prestate = DFAState.S3;
                    }
                    else if (_prestate == DFAState.S2)
                    {
                        endpos = i - 1;	//保存结束位置

                        //检查字母串的匹配类型
                        if (CheckString(startpos, endpos) == true)
                        {
                            SavePhrase(startpos, endpos);
                            startpos = i;
                        }
                        _prestate = DFAState.S3;
                    }
                    else if (_prestate == DFAState.S17)
                    {
                        endpos = i - 1;	//保存结束位置

                        //检查字母串的匹配类型
                        if (CheckString(startpos, endpos) == true)
                        {
                            SavePhrase(startpos, endpos);
                            startpos = i;
                        }
                        _prestate = DFAState.S3;
                    }
                    else if (_prestate != DFAState.S3)
                    {
                        //处理前一个词
                        endpos = i - 1;	//保存结束位置
                        SavePhrase(startpos, endpos);
                        //非字母串转换为字母串
                        _prestate = DFAState.S3;
                        //保存开始位置
                        startpos = i;
                    }
                    else	//之前的状态为字母串DFAState.S3
                    {

                        endpos = i - 1;	//保存结束位置

                        //检查字母串的匹配类型
                        if (CheckString(startpos, endpos) == true)
                        {
                            SavePhrase(startpos, endpos);
                            startpos = i;
                        }
                    }
                }
                else if (Char.IsDigit(_chArray[i]))
                {
                    //数字
                    if (_prestate == DFAState.S0)
                    {
                        //初态
                        _prestate = DFAState.S1;
                        startpos = i;	//保存开始位置
                    }
                    else if (_prestate == DFAState.S1)
                    {
                        //整数串，状态不变
                    }
                    else if (_prestate == DFAState.S2)	//浮点数串
                        _prestate = DFAState.S2;
                    else if (_prestate == DFAState.S3)	//字母
                    {
                        _prestate = DFAState.S2;
                    }
                    else if (_prestate == DFAState.S17)	//_
                    {
                        _prestate = DFAState.S2;
                    }
                    else
                    {
                        //处理前一个词
                        endpos = i - 1;	//保存结束位置
                        //字符串类型检查
                        if (_prestate == DFAState.S3 && CheckString(startpos, endpos) == false)
                            return false;	//如果前一个状态为字符串态，且字符串匹配失败，则退出
                        else
                            SavePhrase(startpos, endpos);

                        //从非数字串转换为整数串
                        _prestate = DFAState.S1;
                        //保存开始位置
                        startpos = i;
                    }
                }
                else if (_chArray[i] == '.')
                {
                    //小数点
                    if (_prestate == DFAState.S1 || _prestate == DFAState.S0)
                        _prestate = DFAState.S2;	//由整数串或初态变为浮点数串
                    else
                    {	//未知态
                        // 需要讨论: 是否保存前一个词
                        //_prestate = DFAState.SX;
                    }
                }
                else if (_chArray[i] == '_')
                {
                    _prestate = DFAState.S17;
                }
                else if (Char.IsWhiteSpace(_chArray[i]))
                {
                    //空格，跳过
                    //处理前一个词
                    endpos = i - 1;
                    //字符串类型检查
                    //if (_prestate == DFAState.S3 && CheckString(startpos, endpos) == false)
                    //    return false;	//如果前一个状态为字符串态，且字符串匹配失败，则退出
                    //else
                    SavePhrase(startpos, endpos);
                    startpos = i + 1;
                    _prestate = DFAState.S0;
                }
                //else if (_chArray[i] == '+' || _chArray[i] == '-' || _chArray[i] == '*' || _chArray[i] == '/' || _chArray[i] == '^' || _chArray[i] == '%' || _chArray[i] == '(' || _chArray[i] == ')' || _chArray[i] == '!' || _chArray[i] == '#' || _chArray[i] == '@' || _chArray[i] == '=')
                else if (_chArray[i] == '+' || _chArray[i] == '-' || _chArray[i] == '*' || _chArray[i] == '/' || _chArray[i] == '(' || _chArray[i] == ')' || _chArray[i] == '#' || _chArray[i] == '=' || _chArray[i] == '>' || _chArray[i] == '<')
                {
                    if (_prestate != DFAState.S0)
                    {
                        //处理前一个词
                        endpos = i - 1;
                        //字符串类型检查
                        //if (_prestate == DFAState.S3 && CheckString(startpos, endpos) == false)
                        //    return false;	//如果前一个状态为字符串态，且字符串匹配失败，则退出
                        //else
                        SavePhrase(startpos, endpos);
                    }
                    if (_chArray[i] == '-')
                    {
                        if (_ps.PhraseTypeResult.Length > 0)
                        {
                            PhraseTypeEnum prept = _ps.PhraseTypeResult[_ps.PhraseTypeResult.Length - 1];
                            if (prept != PhraseTypeEnum.ax && prept != PhraseTypeEnum.bx && prept != PhraseTypeEnum.cx && prept != PhraseTypeEnum.dx && prept != PhraseTypeEnum.ex && prept != PhraseTypeEnum.fx && prept != PhraseTypeEnum.clr && prept != PhraseTypeEnum.sto && prept != PhraseTypeEnum.rightbracket && prept != PhraseTypeEnum.number)
                            {
                                _chArray[i] = '@';
                            }
                        }
                        else
                        {
                            _chArray[i] = '@';
                        }
                    }
                    if (_chArray[i] == '+')
                        _prestate = DFAState.S4;
                    else if (_chArray[i] == '-')
                    {
                        _prestate = DFAState.S5;
                    }
                    else if (_chArray[i] == '*')
                        _prestate = DFAState.S6;
                    else if (_chArray[i] == '/')
                        _prestate = DFAState.S7;
                    else if (_chArray[i] == '=')     //not support so far
                        _prestate = DFAState.S11;
                    else if (_chArray[i] == '>')     //not support so far
                        _prestate = DFAState.S11;
                    else if (_chArray[i] == '<')     //not support so far
                        _prestate = DFAState.S11;
                    else if (_chArray[i] == '%')
                        _prestate = DFAState.S8;
                    else if (_chArray[i] == '^')
                        _prestate = DFAState.S10;
                    else if (_chArray[i] == '(')
                        _prestate = DFAState.S12;
                    else if (_chArray[i] == ')')
                        _prestate = DFAState.S13;
                    else if (_chArray[i] == '!')
                        _prestate = DFAState.S9;
                    else if (_chArray[i] == '#')
                        _prestate = DFAState.S14;
                    else if (_chArray[i] == '@')
                        _prestate = DFAState.S15;

                    //保存开始位置
                    startpos = i;
                }
                else
                {
                    //未知字符，进入未知态
                    //_prestate=DFAState.SX;
                    _prestate = DFAState.S3;
                }
                i++;
            }
            return true;
        }
    }
}
