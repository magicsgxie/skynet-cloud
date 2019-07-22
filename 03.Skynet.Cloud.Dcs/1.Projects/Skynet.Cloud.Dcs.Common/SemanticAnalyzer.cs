using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Common
{
    internal class SemanticAnalyzer
    {
        private Stack _optr;		//运算符栈
        private Stack _opnd;		//数符栈
        private Stack _op;			//符号栈（包括运算符和数符的栈）
        private PhraseStorage _ps = null;
        private PhraseTypeEnum _lastOpForError;	//可能引起错误的那个运算符

        public SemanticAnalyzer(ref PhraseStorage ps)
        {
            _optr = new Stack();
            _opnd = new Stack();
            _op = new Stack();
            _ps = ps;
        }

        string msg = string.Empty;
        public string ErrorTip
        {
            get
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    return msg;
                }
                //错误信息处理（UI错误提示优化）
                if (_lastOpForError == PhraseTypeEnum.unknown)
                    return "计算表达式出现未知错误";
                else if (_lastOpForError == PhraseTypeEnum.sharp)
                    return "计算表达式出错";
                else if (_lastOpForError == PhraseTypeEnum.plus)
                    return "在\'+\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.minus)
                    return "在\'-\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.mutiple)
                    return "在\'*\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.divide)
                    return "在\'/\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.rightbracket)
                    return "在\')\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.leftbracket)
                    return "在\'(\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.greater)
                    return "在\'^\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.equal)
                    return "在\'!\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.mod)
                    return "在\'%\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.less)
                    return "在\'@\'附近存在错误";
                else if (_lastOpForError == PhraseTypeEnum.number)
                    return "在某个数字附近存在错误";
                else
                    return "在\'" + _lastOpForError.ToString() + "\'附近可能存在错误";
            }
        }
        /// <summary>
        /// 虚拟运算（不进行真实的计算）
        /// </summary>
        /// <returns>是否有错误发生</returns>
        private bool FakeCalculate()
        {
            PhraseTypeEnum pt = (PhraseTypeEnum)_optr.Pop();
            OperandType oc = Operator.OperandCount(pt);	//栈顶运算符目数

            PhraseTypeEnum temp_pt;	//存储_op中pop出的一个符号

            switch (oc)
            {
                case OperandType.O0:	//0目运算符，不存在
                    _lastOpForError = pt;
                    return false;
                //_op.Pop();
                //break;
                case OperandType.O1:	//1目运算符
                    if (_opnd.Count >= 1)
                    {
                        _opnd.Pop();
                        _op.Pop();	//抛出数符
                    }
                    else
                    {	//没有足够的数符用于匹配运算符，出错
                        _lastOpForError = pt;
                        return false;
                    }
                    temp_pt = (PhraseTypeEnum)_op.Pop();
                    //抛出运算符，邻近符号检查
                    if (Operator.OperatorCmp((PhraseTypeEnum)_op.Peek(), temp_pt) == PriorityCmpType.Unknown)
                    {
                        _lastOpForError = pt;
                        return false;
                    }
                    _opnd.Push(PhraseTypeEnum.number);
                    _op.Push(PhraseTypeEnum.number);
                    break;
                case OperandType.O2:	//2目运算符
                    if (_opnd.Count >= 2)
                    {
                        _opnd.Pop();
                        _opnd.Pop();
                        _op.Pop();	//抛出数符
                    }
                    else
                    {
                        _lastOpForError = pt;
                        return false;
                    }
                    temp_pt = (PhraseTypeEnum)_op.Pop();
                    //抛出数符，邻近符号检查
                    if (Operator.OperatorCmp((PhraseTypeEnum)_op.Peek(), temp_pt) == PriorityCmpType.Unknown)
                    {
                        _lastOpForError = pt;
                        return false;
                    }
                    temp_pt = (PhraseTypeEnum)_op.Pop();
                    //抛出运算符，邻近符号检查
                    if (Operator.OperatorCmp((PhraseTypeEnum)_op.Peek(), temp_pt) == PriorityCmpType.Unknown)
                    {
                        _lastOpForError = pt;
                        return false;
                    }

                    _opnd.Push(PhraseTypeEnum.number);
                    _op.Push(PhraseTypeEnum.number);
                    break;
            }
            return true;
        }
        /// <summary>
        /// 检查文法
        /// </summary>
        /// <returns>是否正确</returns>
        public bool Check()
        {
            msg = string.Empty;
            _optr.Clear();
            _optr.Push(PhraseTypeEnum.sharp);	//将#作为栈操作结束标志
            _opnd.Clear();
            _op.Clear();
            _op.Push(PhraseTypeEnum.sharp);		//将#作为栈操作结束标志

            int i = 0;
            while (i < _ps.Length)
            {
                PhraseTypeEnum temp_pt = _ps.PhraseTypeResult[i];
                if (temp_pt == PhraseTypeEnum.case_)
                {
                    int n1 = -1, n2 = -1, n3 = -1, n4 = -1;
                    int k1 = -1, k2 = -1, k3 = -1, k4 = -1;
                    n1 = i + 1;
                    while (n1 < _ps.Length)
                    {
                        if (_ps.PhraseTypeResult[n1] == PhraseTypeEnum.when)
                        {
                            k1 = n1;
                            break;
                        }
                        n1++;
                    }

                    n2 = n1 + 1;
                    while (n2 < _ps.Length)
                    {
                        if (_ps.PhraseTypeResult[n2] == PhraseTypeEnum.then)
                        {
                            k2 = n2;
                            break;
                        }
                        n2++;
                    }

                    n3 = n2 + 1;
                    while (n3 < _ps.Length)
                    {
                        if (_ps.PhraseTypeResult[n3] == PhraseTypeEnum.else_)
                        {
                            k3 = n3;
                            break;
                        }
                        n3++;
                    }

                    n4 = n3 + 1;
                    while (n4 < _ps.Length)
                    {
                        if (_ps.PhraseTypeResult[n4] == PhraseTypeEnum.end)
                        {
                            k4 = n4;
                            break;
                        }
                        n4++;
                    }

                    if (k4 == -1)
                    {
                        msg = "case when then else end 语句不完整";
                        return false;
                    }

                    if ((k4 - k3) == 1)
                    {
                        msg = "case when then else end 语句有误，else和end之间不能为空";
                        return false;
                    }

                    if ((k3 - k2) == 1)
                    {
                        msg = "case when then else end 语句有误，then和else之间不能为空";
                        return false;
                    }

                    if ((k2 - k1) == 1)
                    {
                        msg = "case when then else end 语句有误，when和then之间不能为空";
                        return false;
                    }

                    i = k4;
                    temp_pt = PhraseTypeEnum.number;
                }

                //运算前算符相邻检查
                PriorityCmpType temp_pct = (PriorityCmpType)Operator.OperatorCmp((PhraseTypeEnum)_op.Peek(), temp_pt);
                if (temp_pct == PriorityCmpType.Unknown)
                {
                    _lastOpForError = temp_pt;
                    return false;
                }

                //假运算处理
                if (temp_pt == PhraseTypeEnum.number || temp_pt == PhraseTypeEnum.e || temp_pt == PhraseTypeEnum.pi || temp_pt == PhraseTypeEnum.ans
                    || temp_pt == PhraseTypeEnum.ax || temp_pt == PhraseTypeEnum.bx || temp_pt == PhraseTypeEnum.cx || temp_pt == PhraseTypeEnum.dx
                    || temp_pt == PhraseTypeEnum.ex || temp_pt == PhraseTypeEnum.fx)
                {	//是数
                    _opnd.Push(PhraseTypeEnum.number);
                    _op.Push(PhraseTypeEnum.number);
                }
                else	//是运算符
                {
                    //运算结束
                    if ((PhraseTypeEnum)_optr.Peek() == PhraseTypeEnum.sharp && temp_pt == PhraseTypeEnum.sharp)
                        break;

                    temp_pct = (PriorityCmpType)Operator.OperatorCmp2((PhraseTypeEnum)_optr.Peek(), temp_pt);
                    if (temp_pct == PriorityCmpType.Higher)
                    {
                        do
                        {
                            if (this.FakeCalculate() == false)	//虚拟运算
                                return false;
                        } while ((PriorityCmpType)Operator.OperatorCmp2((PhraseTypeEnum)_optr.Peek(), temp_pt) == PriorityCmpType.Higher);
                        //当相邻PhraseTypeEnum优先级相等时
                        if ((PriorityCmpType)Operator.OperatorCmp2((PhraseTypeEnum)_optr.Peek(), temp_pt) == PriorityCmpType.Equal)
                        {
                            _optr.Pop();	//抛出相等的prePhraseTypeEnum
                            //对类似于(number)的情况做处理
                            PhraseTypeEnum pt1 = (PhraseTypeEnum)_op.Pop();
                            _op.Pop();
                            _op.Push(pt1);
                        }
                        else
                        {
                            _optr.Push(temp_pt);
                            _op.Push(temp_pt);
                        }
                    }
                    else if (temp_pct == PriorityCmpType.Lower)
                    {
                        _optr.Push(temp_pt);
                        _op.Push(temp_pt);
                    }
                    else if (temp_pct == PriorityCmpType.Equal)
                    {
                        _optr.Pop();
                        PhraseTypeEnum pt1 = (PhraseTypeEnum)_op.Pop();
                        _op.Pop();
                        _op.Push(pt1);
                    }
                    else
                    {		//出现了不允许相邻的符号
                        _lastOpForError = (PhraseTypeEnum)_optr.Peek();
                        return false;
                    }
                }
                i++;
            }
            //数栈检查，如果并非只剩一个元素报错
            if (_opnd.Count != 1)
            {
                _lastOpForError = PhraseTypeEnum.unknown;
                return false;
            }
            return true;
        }
    }
}
